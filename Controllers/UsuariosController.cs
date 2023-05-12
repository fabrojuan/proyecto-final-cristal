using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MVPSA_V2022.Controllers
{

    [Authorize]
    public class UsuariosController : Controller
    {

        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public UsuariosController(IJwtAuthenticationService jwtAuthenticationService) {
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        [HttpGet]
        [Route("api/usuarios")]
        public IEnumerable<UsuarioCLS> ListarUsuarios()
        {
            try
            { 
            List<UsuarioCLS> listaUsuario;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaUsuario = (from usuario in bd.Usuarios
                                join rol in bd.Rols
                                on usuario.IdTipoUsuario equals rol.IdRol
                                join persona in bd.Personas
                                on usuario.IdPersona equals persona.IdPersona
                                where usuario.Bhabilitado == 1
                                select new UsuarioCLS
                                {
                                    IdUsuario = usuario.IdUsuario,
                                    NombreUser = usuario.NombreUser!,
                                    NombreTipoUsuario = rol.NombreRol!,
                                    NombreCompleto = persona.Apellido + ", " + persona.Nombre,
                                    FechaAlta = (DateTime)usuario.FechaAlta
                                }).ToList();
                
            }
                return listaUsuario;
            }
            catch (Exception ex)
            {
               UsuarioCLS oUserCLS= new UsuarioCLS();   
                oUserCLS.Error = ex.Message;
                return (IEnumerable<UsuarioCLS>)NotFound(oUserCLS);
            }
        }

        [HttpGet]
        [Route("api/usuarios/{idUsuario}")]
        public UsuarioCLS RecuperarUsuario(int idUsuario)
        {
            UsuarioCLS oUserCLS = new UsuarioCLS();
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    oUserCLS = (from Usuario in bd.Usuarios
                                join Persona in bd.Personas
                                on Usuario.IdPersona equals Persona.IdPersona
                                join Rol in bd.Rols on Usuario.IdTipoUsuario equals Rol.IdRol
                                where Usuario.Bhabilitado == 1
                                && Usuario.IdUsuario == idUsuario
                                select new UsuarioCLS
                                {
                                    IdUsuario = Usuario.IdUsuario,
                                    NombreUser = Usuario.NombreUser,
                                    NombrePersona = Persona.Nombre,
                                    Apellido = Persona.Apellido,
                                    Telefono = Persona.Telefono,
                                    Dni = (Persona.Dni.Length > 5) ? Persona.Dni : "-",
                                    Mail = Persona.Mail,
                                    // FechaNac=(DateTime)Persona.FechaNac,
                                    TiposRol = (int)((Usuario.IdTipoUsuario > 0) ? Usuario.IdTipoUsuario : 5),   //El rol   Ojo con este HARDCODEO DEL TIPO DE USUARIO
                                    Domicilio = Persona.Domicilio,
                                    Altura = Persona.Altura,
                                    Contrasenia = Usuario.Contrasenia,
                                    BHabilitado = (int)Usuario.Bhabilitado
                                }).First();

                }
            }
            catch (Exception ex)
            {
                oUserCLS.Error = ex.Message;
            }
            return oUserCLS;
        }

        [HttpPost]
        [Route("api/usuarios")]
        public int GuardarUsuario([FromBody] UsuarioCLS oUsuarioCLS)
        {
            int rpta = 0;
            // int idPersonatem = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    if (oUsuarioCLS.IdUsuario == 0)
                    {
                        Persona oPersona = new Persona(); //Es igual que new persona() pero en la base
                        oPersona.Nombre = oUsuarioCLS.NombrePersona;
                        oPersona.Apellido = oUsuarioCLS.Apellido;
                        oPersona.Telefono = oUsuarioCLS.Telefono;
                        oPersona.Dni = oUsuarioCLS.Dni;
                        oPersona.Bhabilitado = 1;
                        oPersona.Domicilio = oUsuarioCLS.Domicilio;
                        oPersona.Altura = oUsuarioCLS.Altura;
                        oPersona.Mail = oUsuarioCLS.Mail;
                        bd.Personas.Add(oPersona);
                        bd.SaveChanges();
                        Usuario oUsuario = new Usuario();
                        oPersona = bd.Personas.Where(p => p.Dni == oUsuarioCLS.Dni).First();
                        oUsuario.IdPersona = oPersona.IdPersona;
                        oUsuario.NombreUser = oUsuarioCLS.NombreUser;
                        SHA256Managed sha = new SHA256Managed();
                        byte[] dataNocifrada = Encoding.Default.GetBytes(oUsuarioCLS.Contrasenia);
                        byte[] dataCifrada = sha.ComputeHash(dataNocifrada);
                        string claveCifrada = BitConverter.ToString(dataCifrada).Replace("-", "");
                        oUsuario.Contrasenia = claveCifrada;

                        //oUsuario.Contrasenia = oUsuarioCLS.Contrasenia;
                        oUsuario.Bhabilitado = 1;
                        oUsuario.IdTipoUsuario = oUsuarioCLS.TiposRol;
                        bd.Usuarios.Add(oUsuario);
                        bd.SaveChanges();

                    }
                    else
                    {    //FaltaTerminar LA EDICION
                        Usuario oUsuario = bd.Usuarios.Where(p => p.IdUsuario == oUsuarioCLS.IdUsuario).First();
                        oUsuario.NombreUser = oUsuarioCLS.NombreUser;
                        oUsuario.Contrasenia = oUsuarioCLS.Contrasenia;
                        oUsuario.IdTipoUsuario = oUsuarioCLS.TiposRol;
                        bd.SaveChanges();

                    }
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


        // Listar paginas a las que puede acceder el usuario logueado
        [HttpGet]
        [Route("api/usuarios/paginas")]
        public List<PaginaCLS> ListarPaginas([FromHeader(Name = "id_usuario")] int idUsuario)
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaPagina = (from paginaRol in bd.Paginaxrols
                               join pagina in bd.Paginas
                               on paginaRol.IdPagina equals pagina.IdPagina
                               join usuario in bd.Usuarios
                               on paginaRol.IdRol equals usuario.IdTipoUsuario
                               where paginaRol.Bhabilitado == 1
                               && usuario.IdUsuario == idUsuario
                               && pagina.Bvisible == 1
                               select new PaginaCLS
                               {
                                   idPagina = pagina.IdPagina,
                                   Accion = pagina.Accion,
                                   Mensaje = pagina.Mensaje,
                                   Bhabilitado = (int)pagina.Bhabilitado
                               }).ToList();
                return listaPagina;
            }
        }

        //OBTENER VARIABLE DE SESSION DE EMPLEADO
        [HttpGet]
        [Route("api/usuarios/obtenerVariableSession")]
        public SeguridadCLS obtenerVariableSession()
        {
            SeguridadCLS oSeguridadCLS = new SeguridadCLS();
            var variableSession = HttpContext.Session.GetString("empleado");
            if (variableSession == null)
            {
                oSeguridadCLS.valor = "";
            }
            else
            {
                oSeguridadCLS.valor = variableSession;
                List<PaginaCLS> listaPaginaCLS = new List<PaginaCLS>();
                int idUsuario = int.Parse(HttpContext.Session.GetString("empleado"));
                int idTipoRol = int.Parse(HttpContext.Session.GetString("tipoEmpleado"));

                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    listaPaginaCLS = (from empleado in bd.Usuarios
                                      join rol in bd.Rols
                                      on empleado.IdTipoUsuario equals rol.IdRol
                                      join paginaxrol in bd.Paginaxrols
                                      on empleado.IdTipoUsuario equals paginaxrol.IdRol
                                      join pagina in bd.Paginas
                                      on paginaxrol.IdPagina equals pagina.IdPagina
                                      where empleado.IdUsuario == idUsuario && empleado.IdTipoUsuario == idTipoRol
                                      select new PaginaCLS
                                      {
                                          //con substring contamo a partir del primer caracter o sea que la url irá sin el / para no redundar y no tener dobe //
                                          Accion = pagina.Accion.Substring(1),

                                      }).ToList();
                    oSeguridadCLS.lista = listaPaginaCLS;
                }


            }
            return oSeguridadCLS;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/usuarios/login")]
        public IActionResult login([FromBody] UsuarioCLS oUsuarioCLS)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {

                    SHA256Managed sha = new SHA256Managed();
                    byte[] dataNocifrada = Encoding.Default.GetBytes(oUsuarioCLS.Contrasenia);
                    byte[] dataCifrada = sha.ComputeHash(dataNocifrada);
                    string claveCifrada = BitConverter.ToString(dataCifrada).Replace("-", "");

                    rpta = bd.Usuarios.Where( p => p.NombreUser.ToUpper() == oUsuarioCLS.NombreUser.ToUpper() && p.Contrasenia == claveCifrada).Count();
                    if (rpta == 1)
                    {
                        Usuario oUsuarioRecuperado = bd.Usuarios
                            .Where(p => p.NombreUser.ToUpper() == oUsuarioCLS.NombreUser.ToUpper()
                                        && p.Contrasenia == claveCifrada).First();

                        return Ok(_jwtAuthenticationService.getToken(oUsuarioRecuperado.IdUsuario));
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex); 
            }

            return Unauthorized();
        }

    }
}