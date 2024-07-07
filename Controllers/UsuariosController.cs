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

    //[Authorize]
    public class UsuariosController : Controller
    {

        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public UsuariosController(IJwtAuthenticationService jwtAuthenticationService) {
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        [HttpGet]
        [Route("api/usuarios/empleados")]
        public IEnumerable<UsuarioCLS> ListarUsuariosEmpleados()
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
                                && rol.TipoRol == "EMPLEADO"
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
                                    FechaNac=(DateTime)Persona.FechaNac,
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

        [HttpDelete]
        [Route("api/usuarios/{idUsuario}")]
        public ActionResult borrarUsuario(int idUsuario)
        {
            try {
                M_VPSA_V3Context dbContext = new M_VPSA_V3Context();
                var usuarioABorrar = dbContext.Usuarios.Where(x => x.IdUsuario == idUsuario).FirstOrDefault();
                usuarioABorrar.Bhabilitado = 0;
                dbContext.Usuarios.Update(usuarioABorrar);
                dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }            
        }

        [HttpPost]
        [Route("api/usuarios")]
        public ActionResult GuardarUsuario([FromBody] UsuarioCLS oUsuarioCLS)
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
                        oPersona.FechaNac = oUsuarioCLS.FechaNac;
                        oPersona.BtieneUser = 1;
                        bd.Personas.Add(oPersona);
                        bd.SaveChanges();
                        Usuario oUsuario = new Usuario();
                        //oPersona = bd.Personas.Where(p => p.Dni == oUsuarioCLS.Dni).First();
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
                    {   
                        //FaltaTerminar LA EDICION
                        Usuario oUsuario = bd.Usuarios.Where(p => p.IdUsuario == oUsuarioCLS.IdUsuario).First();
                        oUsuario.NombreUser = oUsuarioCLS.NombreUser;
                        oUsuario.Contrasenia = oUsuarioCLS.Contrasenia;
                        oUsuario.IdTipoUsuario = oUsuarioCLS.TiposRol;

                        var oPersona = bd.Personas.Where(p => p.IdPersona == oUsuario.IdPersona).FirstOrDefault();
                        oPersona.Nombre = oUsuarioCLS.NombrePersona;
                        oPersona.Apellido = oUsuarioCLS.Apellido;
                        oPersona.Telefono = oUsuarioCLS.Telefono;
                        oPersona.Dni = oUsuarioCLS.Dni;
                        oPersona.Domicilio = oUsuarioCLS.Domicilio;
                        oPersona.Altura = oUsuarioCLS.Altura;
                        oPersona.Mail = oUsuarioCLS.Mail;
                        oPersona.FechaNac = oUsuarioCLS.FechaNac;
                        bd.Personas.Update(oPersona);

                        bd.SaveChanges();

                    }
                }
                rpta = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
            return Ok(rpta);
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
                               where //paginaRol.Bhabilitado == 1
                               //&&   esta linea anterior es para uqe no se visualicen los que no deben verse activar cuando terminemos.
                               usuario.IdUsuario == idUsuario
                               //&& pagina.Bvisible == 1
                               select new PaginaCLS
                               {
                                   idPagina = pagina.IdPagina,
                                   Accion = pagina.Accion.Substring(1),
                                   Mensaje = pagina.Mensaje,
                                   Bvisible=(int)pagina.Bvisible,
                                   Bhabilitado = (int)pagina.Bhabilitado
                               }).ToList();
                return listaPagina;
            }
        }

        [HttpGet]
        [Route("api/usuarios/paginas/menu")]
        public List<PaginaCLS> ListarPaginasMenu([FromHeader(Name = "id_usuario")] int idUsuario)
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();    
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaPagina = (from paginaRol in bd.Paginaxrols
                               join pagina in bd.Paginas
                               on paginaRol.IdPagina equals pagina.IdPagina
                               join usuario in bd.Usuarios
                               on paginaRol.IdRol equals usuario.IdTipoUsuario
                               where //paginaRol.Bhabilitado == 1
                               //&&   esta linea anterior es para uqe no se visualicen los que no deben verse activar cuando terminemos.
                               usuario.IdUsuario == idUsuario
                               && pagina.Bvisible == 1
                               & pagina.Bhabilitado == 1
                               orderby pagina.Mensaje
                               select new PaginaCLS
                               {
                                   idPagina = pagina.IdPagina,
                                   Accion = pagina.Accion.Substring(1),
                                   Mensaje = pagina.Mensaje,
                                   Bvisible=(int)pagina.Bvisible,
                                   Bhabilitado = (int)pagina.Bhabilitado
                               }).ToList();
                return listaPagina;
            }
        }

        //OBTENER VARIABLE DE SESSION DE EMPLEADO
        [HttpGet]
        [Route("api/usuarios/obtenerVariableSession")]
        public SeguridadCLS obtenerVariableSession([FromHeader(Name = "id_usuario")] int idUsuario)
        {
            SeguridadCLS oSeguridadCLS = new SeguridadCLS();
            oSeguridadCLS.valor = idUsuario.ToString();

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context()) {
                oSeguridadCLS.lista =
                                (from usuario in bd.Usuarios
                                  join rol in bd.Rols
                                  on usuario.IdTipoUsuario equals rol.IdRol
                                  join paginaxrol in bd.Paginaxrols
                                  on usuario.IdTipoUsuario equals paginaxrol.IdRol
                                  join pagina in bd.Paginas
                                  on paginaxrol.IdPagina equals pagina.IdPagina
                                  where usuario.IdUsuario == idUsuario
                                  && rol.TipoRol == "EMPLEADO"
                                  select new PaginaCLS
                                  {
                                      //con substring contamo a partir del primer caracter o sea que la url irá sin el / para no redundar y no tener dobe //
                                      Accion = pagina.Accion.Substring(1),
                                  }).ToList();
            }

            return oSeguridadCLS;
        }

        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/usuarios/login")]
        public IActionResult loginNuevo([FromBody] SolicitudLoginDto solicitudLoginDto)
        {
            int rpta = 0;
            try
            {

                SHA256Managed sha = new SHA256Managed();
                byte[] dataNocifrada = Encoding.Default.GetBytes(solicitudLoginDto.usuarioContrasenia);
                byte[] dataCifrada = sha.ComputeHash(dataNocifrada);
                string claveCifrada = BitConverter.ToString(dataCifrada).Replace("-", "");


                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {

                    TokenUsuarioDto tokenUsuarioDto = (from usuarios in bd.Usuarios
                                       join roles in bd.Rols
                                       on usuarios.IdTipoUsuario equals roles.IdRol
                                       where usuarios.Bhabilitado == 1
                                       &&
                                       usuarios.NombreUser == solicitudLoginDto.usuarioNombre.ToUpper()
                                       && usuarios.Contrasenia == claveCifrada
                                       && roles.TipoRol == "EMPLEADO"
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Unauthorized();
        }

    }
}