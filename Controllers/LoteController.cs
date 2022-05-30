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







        //fin clase
    }
}
