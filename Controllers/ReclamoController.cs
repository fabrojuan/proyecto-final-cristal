using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVPSA_V2022.Controllers
{
    [Route("api/reclamos")]
    public class ReclamoController : Controller
    {
        private readonly IReclamoService reclamoService;

        public ReclamoController(IReclamoService reclamoService)
        {
            this.reclamoService = reclamoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("tipos-reclamo")]
        public IEnumerable<TipoReclamoCLS> ListarTiposReclamo()
        {
            return reclamoService.listarTiposReclamo();

        }
        //   Agregaro porque por ahora es todo nu,l!!! tiempo_Max_Tratamiento = (int)tipoReclamo.TiempoMaxTratamiento,
      
        [HttpPost]
        public int guardarReclamo([FromBody] ReclamoCLS reclamoCLS)
        {
            return reclamoService.guardarReclamo(reclamoCLS);
        }
        [HttpGet]
        public IActionResult/*IEnumerable<ReclamoCLS>*/ ListarReclamos()
        {
            try
            {
                return Ok(reclamoService.listarReclamos());
            } catch (Exception ex) {
                return NotFound();
            }
            
        }

        [HttpGet]
        [Route("otros")]
        public IEnumerable<ReclamoCLS> ListarOtrosReclamos()
        {
            return reclamoService.listarReclamos();
        }

    }
}
