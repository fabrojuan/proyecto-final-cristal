using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Exceptions;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;


namespace MVPSA_V2022.Controllers
{
    [ApiController]
    [Route("api/Indicadores/")]
    //[Authorize]

    public class IndicadoresController : Controller
    {
        private readonly IindicadoresService indicadoresService;

        public IndicadoresController(IindicadoresService indicadoresService, IUsuarioService usuarioService)
        {
            this.indicadoresService = indicadoresService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("CantidadDenunciasCerradas")]
        public IActionResult CantidadDenunciasCerradas()
        {
            try
            {
                return Ok(indicadoresService.DenunciasCerradas());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("CantidadDenunciasAbiertas")]
        public IActionResult CantidadDenunciasAbiertas()
        {
            try
            {
                return Ok(indicadoresService.DenunciasAbiertas());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("FechaTrabajosEnDenuncias")]
        public IActionResult FechaTrabajosEnDenuncias()
        {
            try
            {
                return Ok(indicadoresService.FechaTrabajosEnDenuncias());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("DenunciasporTipo")]
        public IActionResult DenunciasporTipo()
        {
            try
            {
                return Ok(indicadoresService.DenunciasporTipo());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("reclamos-cerrados-por-mes-y-tipo-cierre")]
        public IActionResult getDatosChartReclamosCerradosPorMesyTipoCierre() {
            return Ok(this.indicadoresService.getDatosChartReclamosCerradosPorMesyTipoCierre());
        }

        [HttpGet]
        [Route("reclamos-nuevos-por-mes")]
        public IActionResult getDatosChartReclamosNuevosPorMes() {
            return Ok(this.indicadoresService.getDatosChartReclamosNuevosPorMes());
        }

        [HttpGet]
        [Route("reclamos-abiertos-por-estado")]
        public IActionResult getDatosChartReclamosAbiertosPorEstado() {
            return Ok(this.indicadoresService.getDatosChartReclamosAbiertosPorEstado());
        }

        [HttpGet]
        [Route("trabajos-reclamos-por-area-y-mes")]
        public IActionResult getDatosChartTrabajosReclamosPorAreaYMes() {
            return Ok(this.indicadoresService.getDatosChartTrabajosReclamosPorAreaYMes());
        }


    }
}
