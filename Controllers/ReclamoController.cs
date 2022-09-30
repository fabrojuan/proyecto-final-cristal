﻿using Microsoft.AspNetCore.Authorization;
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
    [Route("api/reclamos")]
    [Authorize]
    public class ReclamoController : Controller
    {
        private readonly IReclamoService reclamoService;
        private readonly IUsuarioService _usuarioService;

        public ReclamoController(IReclamoService reclamoService, IUsuarioService usuarioService)
        {
            this.reclamoService = reclamoService;
            this._usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("tipos-reclamo/{codTipoReclamo}")]
        public IActionResult ListarTiposReclamo(int codTipoReclamo)
        {
            try
            {
                TipoReclamoCLS tipoReclamo = this.reclamoService.getTipoReclamo(codTipoReclamo);
                return Ok(tipoReclamo);
            }
            catch (TipoReclamoNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("tipos-reclamo")]
        public IEnumerable<TipoReclamoCLS> getTipoReclamo()
        {
            return reclamoService.listarTiposReclamo();

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
        public IActionResult guardarTipoReclamo([FromBody] TipoReclamoCLS tipoReclamoCLS)
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

                var authorization = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(authorization);
                string userId = jwt.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

                UsuarioCLS usuarioCLS = _usuarioService.findUsuarioById(Int32.Parse(userId));
                tipoReclamoCLS.IdUsuarioAlta = usuarioCLS.IdUsuario;
                tipoReclamoCLS.IdUsuarioModificacion = usuarioCLS.IdUsuario;

                tipoReclamoCLS = this.reclamoService.guardarTipoReclamo(tipoReclamoCLS);
                return Ok(tipoReclamoCLS);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("tipos-reclamo")]
        public IActionResult actualizarTipoReclamo([FromBody] TipoReclamoCLS tipoReclamoCLS)
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

                tipoReclamoCLS = this.reclamoService.modificarTipoReclamo(tipoReclamoCLS);
                return Ok(tipoReclamoCLS);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
