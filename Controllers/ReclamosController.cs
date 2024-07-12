using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVPSA_V2022.clases;
using MVPSA_V2022.Exceptions;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MVPSA_V2022.Controllers
{
    [ApiController]
    [Route("api/reclamos")]
    [Authorize]
    public class ReclamosController : Controller
    {
        private readonly IReclamoService reclamoService;
        private readonly ITrabajoReclamoService trabajoReclamoService;

        public IActionResult Index()
        {
            return View();
        }

        public ReclamosController(IReclamoService reclamoService, ITrabajoReclamoService trabajoReclamoService)
        {
            this.reclamoService = reclamoService;
            this.trabajoReclamoService = trabajoReclamoService;
        }

        [HttpGet]
        [Route("tipos-reclamo/{codTipoReclamo}")]
        public IActionResult consultarTipoReclamo(int codTipoReclamo)
        {
            try
            {
                return Ok(this.reclamoService.getTipoReclamo(codTipoReclamo));
            }
            catch (TipoReclamoNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("tipos-reclamo")]
        public IActionResult getTiposReclamo()
        {
            return Ok(reclamoService.listarTiposReclamo());
        }

        [HttpDelete]
        [Route("tipos-reclamo/{codTipoReclamo}")]
        public IActionResult emininarTipoReclamo(int codTipoReclamo)
        {
            try
            {
                this.reclamoService.eliminarTipoReclamo(codTipoReclamo);
                return Ok();
            }
            catch (TipoReclamoEnUsoException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("tipos-reclamo")]
        public IActionResult guardarTipoReclamo(
            [FromHeader(Name = "id_usuario")] string idUsuarioAlta,
            [FromBody] TipoReclamoDto tipoReclamoCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    StringBuilder errors = new StringBuilder("Errores: ");
                    ModelState.Keys.ToList().ForEach(key => errors.Append(key.ToString() + ". "));
                    Console.WriteLine(errors);
                    throw new PagoNoValidoException(errors.ToString());
                }

                tipoReclamoCLS = this.reclamoService.guardarTipoReclamo(tipoReclamoCLS, Int32.Parse(idUsuarioAlta));
                return Ok(tipoReclamoCLS);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("tipos-reclamo")]
        public IActionResult actualizarTipoReclamo(
            [FromHeader(Name = "id_usuario")] string idUsuarioModificacion,
            [FromBody] TipoReclamoDto tipoReclamoCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    StringBuilder errors = new StringBuilder("Errores: ");
                    ModelState.Keys.ToList().ForEach(key => errors.Append(key.ToString() + ". "));
                    Console.WriteLine(errors);
                    throw new PagoNoValidoException(errors.ToString());
                }

                tipoReclamoCLS = this.reclamoService.modificarTipoReclamo(tipoReclamoCLS, Int32.Parse(idUsuarioModificacion));
                return Ok(tipoReclamoCLS);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //   Agregaro porque por ahora es todo nu,l!!! tiempo_Max_Tratamiento = (int)tipoReclamo.TiempoMaxTratamiento,

        [HttpPost]
        public IActionResult guardarReclamo([FromHeader(Name = "id_usuario")] string idUsuarioAlta,
                                  [FromBody] CrearReclamoRequestDto reclamoCLS)
        {
            try
            {
                return Ok(reclamoService.guardarReclamo(reclamoCLS, Int32.Parse(idUsuarioAlta)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ListarReclamos([FromHeader(Name = "id_usuario")] string idUsuarioAlta,
                                            [FromQuery] int area,
                                            [FromQuery] int estado,
                                            [FromQuery] int numero,
                                            [FromQuery(Name = "nom_ape_vecino")] string? nomApeVecino)
        {
            try
            {
                return Ok(reclamoService.listarReclamos(Int32.Parse(idUsuarioAlta), area, estado, numero, nomApeVecino));
            } catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("{nroReclamo}")]
        public IActionResult getReclamo(int nroReclamo)
        {
            try
            {
                return Ok(reclamoService.getReclamo(nroReclamo));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("{nroReclamo}")]
        public IActionResult modificarReclamo(int nroReclamo,
                                           [FromBody] ModificarReclamoRequestDto reclamoDto) {
            
            return Ok(reclamoService.modificarReclamo(nroReclamo, reclamoDto));

        }

        [HttpGet]
        [Route("prioridades")]
        public IActionResult getPrioridades()
        {
            try
            {
                    return Ok(reclamoService.getPrioridades());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("{nroReclamo}/acciones")]
        public IActionResult aplicarAccion([FromHeader(Name = "id_usuario")] string idUsuarioAccion,
                                                        int nroReclamo,
                                                       [FromBody] AplicarAccionDto aplicarAccionDto) {

            aplicarAccionDto.nroReclamo = nroReclamo;
            aplicarAccionDto.idUsuario = idUsuarioAccion;
            reclamoService.aplicarAccion(aplicarAccionDto);
            return Ok();
        }

        /**
         * Observaciones reclamos
         */

        [HttpGet]
        [Route("{nroReclamo}/observaciones")]
        public IActionResult buscarObservaciones([FromHeader(Name = "id_usuario")] string idUsuarioAccion,
                                                  int nroReclamo)
        {
            return Ok(reclamoService.obtenerObservacionesDeReclamo(nroReclamo));            
        }

        [HttpGet]
        [Route("estados")]
        public IActionResult getEstadosReclamos() {
            try
            {
                    return Ok(reclamoService.getEstadosReclamo());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("{nroReclamo}/trabajos")]
        public IActionResult listarTrabajos([FromHeader(Name = "id_usuario")] string idUsuarioAlta,
                                             int nroReclamo) {
            return Ok(trabajoReclamoService.obtenerTrabajosReclamo(nroReclamo));
        }

        [HttpPost]
        [Route("{nroReclamo}/trabajos")]
        public IActionResult guardarTrabajo([FromHeader(Name = "id_usuario")] string idUsuarioAlta,
                                             int nroReclamo,
                                             [FromBody] TrabajoReclamoCreacionRequestDto trabajoReclamoDto) {

            trabajoReclamoDto.nroReclamo = nroReclamo;                                    
            trabajoReclamoService.guardarTrabajo(trabajoReclamoDto, Int32.Parse(idUsuarioAlta));
            return Ok();
            
        }

        [HttpGet]
        [Route("{nroReclamo}/opciones")]
        public IActionResult getOpcionesReclamo([FromHeader(Name = "id_usuario")] string idUsuario,
                                                 int nroReclamo) {
            return Ok(this.reclamoService.getOpcionesReclamo(nroReclamo, Int32.Parse(idUsuario)));
        }

    }
}
