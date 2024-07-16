using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace MVPSA_V2022.Controllers
{
    public class LoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("api/Lote")]
        public IEnumerable<LoteCLS> listarLotes()
        {
            string nombretemp;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                Prioridad oPriorodad = new Prioridad();
                UsuarioCLS usuarioCLS = new UsuarioCLS();

                List<LoteCLS> listaLote = (from lote in bd.Lotes
                                           join persona in bd.Personas
                                          on lote.IdPersona equals persona.IdPersona
                                           //join vecino in bd.UsuarioVecinos
                                           //on persona.IdPersona equals vecino.IdPersona
                                           where lote.Bhabilitado == 1  //añadir columna faltante a la base
                                           select new LoteCLS
                                           {
                                               IdLote = lote.IdLote,
                                               // Fecha =necesito alguna fecha?
                                               Calle = lote.Calle,
                                               Altura = lote.Altura,
                                               Manzana = lote.Manzana,
                                               NomenclaturaCatastral = lote.NomenclaturaCatastral,
                                               SupTerreno = lote.SupTerreno,
                                               SupEdificada = lote.SupEdificada,
                                               BaseImponible = lote.BaseImponible,
                                               EstadoDeuda = lote.EstadoDeuda,
                                               ValuacionTotal = lote.ValuacionTotal,
                                               IdPersona = lote.IdPersona,
                                               ApellidoTitular = !String.IsNullOrEmpty(persona.Apellido) ? persona.Apellido : "Sin Dueño",
                                               DniTitular= !String.IsNullOrEmpty(persona.Dni) ? persona.Dni : "Sin dni",

                                               NroLote = lote.NroLote,
                                           }).ToList();
                return listaLote;
            }
        }
        [HttpGet]
        [Route("api/Lote/listarTiposLote")]
        public IEnumerable<TipoLoteCLS> listarTiposLote()
        {
            List<TipoLoteCLS> listaTipoLote = null;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaTipoLote = (from TipoLote in bd.TipoLotes
                                 where TipoLote.Bhabilitado == 1
                                 select new TipoLoteCLS
                                 {
                                     Cod_Tipo_Lote = TipoLote.CodTipoLote,
                                     Nombre = TipoLote.Nombre,
                                     Descripcion = TipoLote.Descripcion

                                 }).ToList();
                return listaTipoLote;
            }

        }
        [HttpGet]

        [Route("api/Lote/RecuperarLotePreExistente/{idLote}")]
        public long RecuperarLotePreExistente(string idLote)
        {
            long nomenclatura = 0;
            LoteCLS oloteCLS2 = new LoteCLS();
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    oloteCLS2 = (from lote in bd.Lotes

                                 where lote.Bhabilitado == 1
                                 && lote.NomenclaturaCatastral == idLote
                                 select new LoteCLS
                                 {
                                     NomenclaturaCatastral = lote.NomenclaturaCatastral
                                 }).First();

                }
                if (oloteCLS2.NomenclaturaCatastral != null)
                    nomenclatura = long.Parse(oloteCLS2.NomenclaturaCatastral);
                else
                { nomenclatura = 0; }
            }
            catch (Exception ex)
            {
                //oloteCLS2.Error = ex.Message;
                return nomenclatura;
            }

            return nomenclatura;
        }





        [HttpPost]
        [Route("api/Lote/GuardarLote")]
        public int GuardarLote([FromBody] LoteCLS oLoteCLS)
        {
            Lote oLoteIdLote = new Lote();
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    using (var transaccion = new TransactionScope())
                    {
                        Persona oPersona = new Persona();
                        Lote oLote = new Lote();
                        oLote.NroLote = oLoteCLS.NroLote;
                        oLote.Manzana = oLoteCLS.Manzana;
                        oLote.Altura = oLoteCLS.Altura;
                        oLote.Calle = oLoteCLS.Calle;
                        oLote.SupTerreno = oLoteCLS.SupTerreno;
                        oLote.SupEdificada = oLoteCLS.SupEdificada;
                        oLote.NomenclaturaCatastral = oLoteCLS.NomenclaturaCatastral;
                        oLote.Bhabilitado = 1;
                        oLote.Esquina = oLoteCLS.Esquina;
                        oLote.Asfaltado = oLoteCLS.Asfaltado;
                        //Busca id de persona con el dni que pase por parametro para insertar
                        oPersona = bd.Personas.Where(p => p.Dni == oLoteCLS.DniTitular).First();
                        oLote.IdPersona = oPersona.IdPersona;
                        //oLote.IdTipoLote = int.Parse(oLoteCLS.CodTipoInmueble!);
                        //oLote.IdTipoLote = oLoteCLS.CodTipoInmueble;

                        bd.Lotes.Add(oLote);

                        bd.SaveChanges();

                        //oLoteIdLote = bd.Lotes.OrderBy(IdLote => IdLote).Last();
                        //Seteo de ID de Lote seleccionado para traer el correo de la persona cuando pagCoue la boleta.
                        //HttpContext.Session.SetString("idLoteaPagar", idLote.ToString());
                        //HttpContext.Session.Remove("idLoteaPagar");
                        ////Antes creo necesitamos un modal para cargar el dueño y luego crear el lote por ultimo un procedimiento almacenado para 
                        ///calcular la base imponible.

                        transaccion.Complete();

                        //oLote = bd.Lote.Where(d => d.NroLote == LoteCLS.NroLote).First();


                    } //Fin de La Transaccion
                }
                //rpta = oLote.IdLote;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                rpta = 0;
            }
            return rpta;
        }

        //Continuar ma;ana para largar los lotes cargados en el grafico.
        [HttpGet]
        [Route("api/Lote/LotesCargados")]
        public Chartlotes LotesCargados([FromHeader(Name = "id_usuario")] string idUsuario)
        {
            Chartlotes oChartLotes = new Chartlotes();

            try
            {

                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    oChartLotes.totalLotes = (from lote in bd.Lotes
                                                     where lote.Bhabilitado == 1
                                                     select new LoteCLS
                                                     {
                                                         NroLote = (int)lote.NroLote,
                                                     }).Count();
                    oChartLotes.totalVecinos = (from usuario in bd.Usuarios join rol in bd.Rols
                                              on usuario.IdTipoUsuario equals rol.IdRol
                                              where rol.CodRol=="VEC"
                                              select new VecinoCLS
                                              {
                                                  IdPersona = (int)usuario.IdPersona,
                                              }).Count();

                    return oChartLotes;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return oChartLotes;
                // return (ex.Message);
            }


        }




        //fin clase
    }
}
