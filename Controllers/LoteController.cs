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
                                                        Calle=lote.Calle,
                                                        Altura=lote.Altura,
                                                        Manzana = lote.Manzana,
                                                        NomenclaturaCatastral=lote.NomenclaturaCatastral,
                                                        SupTerreno = lote.SupTerreno,
                                                        SupEdificada=lote.SupEdificada,
                                                        BaseImponible =lote.BaseImponible,
                                                        EstadoDeuda = lote.EstadoDeuda, 
                                                        ValuacionTotal = lote.ValuacionTotal,   
                                                        IdPersona=lote.IdPersona,
                                                        NroLote=lote.NroLote,
                                                    }).ToList();
                return listaLote;
            }
        }
        [HttpGet]
        [Route("api/Lote/listarTiposLote")]
        public IEnumerable<TipoLoteCLS> listarTiposLote()
        {
            List<TipoLoteCLS> listaTipoLote;
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
        [HttpPost]
        [Route("api/Lote")]
        public int guardarLote([FromBody] LoteCLS oLoteCLS)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    using (var transaccion = new TransactionScope())
                    {
                        Lote oLote = new Lote();
                        oLote.NroLote = oLoteCLS.NroLote;
                        oLote.Manzana = oLoteCLS.Manzana;
                        oLote.Altura = oLoteCLS.Altura;
                        oLote.Calle = oLoteCLS.Calle;
                        oLote.SupTerreno = oLoteCLS.SupTerreno;
                        oLote.SupEdificada = oLoteCLS.SupEdificada;
                        oLote.NomenclaturaCatastral = oLoteCLS.NomenclaturaCatastral;
                        oLote.Bhabilitado = 1;
                        oLote.IdTipoLote = int.Parse(oLoteCLS.TipoInmueble!);
                        bd.Lotes.Add(oLote);
                       
                        bd.SaveChanges();

                        //Antes creo necesitamos un modal para cargar el dueño y luego crear el lote por ultimo un procedimiento almacenado para 
                        ///calcular la base imponible.

                        transaccion.Complete();

                        // oLote = bd.Lote.Where(d => d.NroLote == LoteCLS.NroLote).First();


                    } //Fin de La Transaccion
                }
                rpta = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                rpta = 0;
            }
            return rpta;
        }






        //fin clase
    }
}
