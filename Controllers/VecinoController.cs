using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Services;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace MVPSA_V2022.Controllers
{
    [Authorize]
    public class VecinoController : Controller
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public VecinoController(IJwtAuthenticationService jwtAuthenticationService)
        {
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }
        //********************* GUARDAR VECINO ******************************************
        [HttpPost]
        [Route("api/Vecino/guardarVecino")]
        public int GuardarVecino([FromBody] VecinoCLS oVecinoCLS)
        {
            int rpta = 0;

            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    using (var transaccion = new TransactionScope())
                    {
                        if (oVecinoCLS.IdVecino == 0)
                        {
                            Persona oPersona = new Persona();
                            oPersona.Nombre = oVecinoCLS.NombrePersona;
                            oPersona.Apellido = oVecinoCLS.Apellido;
                            oPersona.Telefono = oVecinoCLS.Telefono;
                            oPersona.Dni = oVecinoCLS.Dni;
                            oPersona.Bhabilitado = 1;
                            oPersona.Domicilio = oVecinoCLS.Domicilio;
                            oPersona.Altura = oVecinoCLS.Altura;
                            oPersona.Mail = oVecinoCLS.Mail;
                            oPersona.BtieneUser = 1;
                            oPersona.FechaNac = oVecinoCLS.FechaNac;
                            bd.Personas.Add(oPersona);
                            bd.SaveChanges();
                            UsuarioVecino oUsuario = new UsuarioVecino();
                            oPersona = bd.Personas.Where(p => p.Dni == oVecinoCLS.Dni).First();
                            oUsuario.IdPersona = oPersona.IdPersona;
                            oUsuario.NombreUser = oVecinoCLS.NombreUser;
                            //Cifrado de la contraseña
                            string Contrasenia = oVecinoCLS.Contrasenia;
                            SHA256Managed sha = new SHA256Managed();
                            byte[] dataNocifrada = Encoding.Default.GetBytes(Contrasenia);
                            byte[] dataCifrada = sha.ComputeHash(dataNocifrada);
                            string claveCifrada = BitConverter.ToString(dataCifrada).Replace("-", "");
                            oUsuario.Contrasenia = claveCifrada;
                            oUsuario.Bhabilitado = 1;
                            bd.UsuarioVecinos.Add(oUsuario);
                            bd.SaveChanges();
                            transaccion.Complete();
                            //Para el caso de edicion no hace falta hacer nada porque la relacion a persona ya esta                     
                            rpta = 1;
                        }
                        else
                        {    //FaltaTerminar LA EDICION
                            Usuario oUsuario = bd.Usuarios.Where(p => p.IdUsuario == oVecinoCLS.IdVecino).First();
                            oUsuario.NombreUser = oVecinoCLS.NombreUser;
                            oUsuario.Contrasenia = oVecinoCLS.Contrasenia;
                            bd.SaveChanges();
                            transaccion.Complete();
                            rpta = 1;
                        }
                    }
                }
                rpta = 1;
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "No pueden salvarse los cambios. " +
                        "Intentalo de nuevo cambiando todos los valores " +
                        "si el problema persiste llamalo a Roman..");
                rpta = 0;
                Console.WriteLine(ex.ToString());
            }
            return rpta;
        }
        //********************* Fin GUARDAR VECINO ******************************************

        //********************* VALIDAR EMAIL VECINO ******************************************

        [HttpGet]
        [Route("api/Vecino/validarCorreo/{id}/{correo}")]
        public int validarCorreo(int id, string correo)
        {
            int rpta = 0;

            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    if (id == 0)  //es porque el usuario es nuevo con lo cual no hay mail previo.
                    {
                        rpta = bd.Personas.Where(p => p.Mail.ToLower() == correo.ToLower()).Count();
                    }
                    else
                    {
                        rpta = bd.Personas.Where(p => p.Mail.ToLower() == correo.ToLower() && p.IdPersona != id).Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return rpta = 0;
            }
            return rpta;
        }
        //*********************FIN VALIDAR EMAIL VECINO ******************************************
        //***************** Listar Personas *************************************

        [HttpGet]
        [Route("api/Vecino/listarvecinos")]
        public IEnumerable<PersonaCLS> listarvecinos()
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                Prioridad oPriorodad = new Prioridad();
                UsuarioCLS usuarioCLS = new UsuarioCLS();

                List<PersonaCLS> listaPersona = (from persona in bd.Personas
                                                    where persona.Bhabilitado == 1
                                                    select new PersonaCLS
                                                    {
                                                        Iidpersona=persona.IdPersona,
                                                      Nombre = !String.IsNullOrEmpty(persona.Nombre) ? persona.Nombre : "No Posee",
                                                        apellido = !String.IsNullOrEmpty(persona.Apellido)?persona.Apellido : "No Posee",
                                                        Telefono = !String.IsNullOrEmpty(persona.Telefono) ? persona.Telefono : "No Posee",
                                                        Mail = !String.IsNullOrEmpty(persona.Mail) ? persona.Mail : "No Posee",
                                                        Domicilio = !String.IsNullOrEmpty(persona.Domicilio) ? persona.Domicilio : "No Posee",
                                                        altura = !String.IsNullOrEmpty(persona.Altura) ? persona.Altura : "No Posee"
                                                     }).ToList();
                return listaPersona;
            }

        }
        //*****************FIN Listar Personas *************************************




        //***************** Login Vecino *************************************

        //CERRAR SESION
        [HttpGet]
        [Route("api/Vecino/cerrarSessionVecino")]
        public SeguridadCLS cerrarSessionVecino()
        {
            SeguridadCLS oSeguridadCLS = new SeguridadCLS();
            try
            {
                HttpContext.Session.Remove("vecino");
                HttpContext.Session.Remove("nombreVecino");
                oSeguridadCLS.valor = "OK";

            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return oSeguridadCLS;
        }



        //OBTENER VARIABLE DE SESSION idVecino la variable es vecino
        [HttpGet]
        [Route("api/Vecino/obtenerVariableSession")]
        public SeguridadCLS obtenerVariableSession()
        {
            SeguridadCLS oSeguridadCLS = new SeguridadCLS();
            var variableSession = HttpContext.Session.GetString("vecino");
            if (variableSession == null)
            {
                oSeguridadCLS.valor = "";
            }
            else
            {
                oSeguridadCLS.valor = variableSession;
            }
            return oSeguridadCLS;
        }

        //OBTENER de la VARIABLE DE SESSION el idVecino la variable es vecino
        [HttpGet]
        [Route("api/Vecino/obtenerSessionNombreVecino")]
        public SeguridadCLS obtenerSessionNombreVecino()
        {
            SeguridadCLS oSeguridadCLS = new SeguridadCLS();
            var variableSession = HttpContext.Session.GetString("nombreVecino");
            if (variableSession == null)
            {
                oSeguridadCLS.valor = "";
            }
            else
            {
                oSeguridadCLS.valor = variableSession;
            }
            return oSeguridadCLS;
        }

        //Insertar metodo para obtener user.
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Vecino/login")]
        public IActionResult login([FromBody] VecinoCLS oVecinoCLS)
        {
            SHA256Managed sha = new SHA256Managed();
            byte[] dataNocifrada = Encoding.Default.GetBytes(oVecinoCLS.Contrasenia);
            byte[] dataCifrada = sha.ComputeHash(dataNocifrada);
            string claveCifrada = BitConverter.ToString(dataCifrada).Replace("-", "");

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                TokenUsuarioDto tokenUsuarioDto = (from usuarios in bd.Usuarios
                                                   join roles in bd.Rols
                                                   on usuarios.IdTipoUsuario equals roles.IdRol
                                                   where usuarios.Bhabilitado == 1
                                                   && usuarios.NombreUser == oVecinoCLS.NombreUser.ToUpper()
                                                   && usuarios.Contrasenia == claveCifrada
                                                   && roles.TipoRol == "VECINO"
                                                   select new TokenUsuarioDto
                                                   {
                                                       idUsuario = usuarios.IdUsuario,
                                                       usuarioNombre = usuarios.NombreUser,
                                                       idRol = roles.IdRol,
                                                       codRol = roles.CodRol,
                                                       tipoRol = roles.TipoRol

                                                   }
                     ).FirstOrDefault();

                if (tokenUsuarioDto != null)
                {
                    return Ok(_jwtAuthenticationService.getToken(tokenUsuarioDto.idUsuario, tokenUsuarioDto.codRol));
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
        //*****************FIN LOGIN **************************************


    }
}
