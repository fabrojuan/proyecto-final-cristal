using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MVPSA_V2022.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/Usuario/listarUsuarios")]
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
                                where usuario.Bhabilitado == 1
                                select new UsuarioCLS
                                {
                                    IdUsuario = usuario.IdUsuario,
                                    NombreUser = usuario.NombreUser!,
                                    NombreTipoUsuario = rol.NombreRol!,
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
        //Obtener Rol
        [HttpGet]
        [Route("api/Usuario/listarRol")]
        public IEnumerable<RolCLS> listarRol()
        {
            List<RolCLS> listaRol;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaRol = (from Rol in bd.Rols
                            where Rol.Bhabilitado == 1
                            select new RolCLS
                            {
                                IidRol = Rol.IdRol,
                                NombreRol = Rol.NombreRol
                            }).ToList();
                return listaRol;
            }
        }
        [HttpGet]
        [Route("api/Usuario/recuperarUsuario/{idUsuario}")]
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




        // Continuar aca GUARDAR Empleado!!!!
        [HttpPost]
        [Route("api/Usuario/guardarUsuario")]
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

        // Listar paginas a las que puede acceder el usuario 
        [HttpGet]
        [Route("api/Usuario/listarPaginas")]
        public List<PaginaCLS> ListarPaginas()
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            int idTipoEmpleado = int.Parse(HttpContext.Session.GetString("tipoEmpleado"));
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaPagina = (from paginaRol in bd.Paginaxrols
                               join pagina in bd.Paginas
                               on paginaRol.IdPagina equals pagina.IdPagina
                               where paginaRol.Bhabilitado == 1
                               && paginaRol.IdRol == idTipoEmpleado
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




        //CERRAR SESION
        [HttpGet]
        [Route("api/Usuario/cerrarSession")]
        public SeguridadCLS cerrarSession()
        {
            SeguridadCLS oSeguridadCLS = new SeguridadCLS();
            try
            {
                HttpContext.Session.Remove("usuario");
                HttpContext.Session.Remove("empleado");
                HttpContext.Session.Remove("tipoEmpleado");


                oSeguridadCLS.valor = "OK";

            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return oSeguridadCLS;
        }



        //OBTENER VARIABLE DE SESSION DE EMPLEADO
        [HttpGet]
        [Route("api/Usuario/obtenerVariableSession")]
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
        //OBTENER VARIABLE DE SESSION idUsuario la variable es Empleado
        [HttpGet]
        [Route("api/Usuario/obtenerVariableSessionID")]
        public SeguridadCLS obtenerVariableSessionID()
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
            }
            return oSeguridadCLS;
        }

        //Insertar metodo para obtener user.
        [HttpPost]
        [Route("api/Usuario/login")]
        public UsuarioCLS login([FromBody] UsuarioCLS oUsuarioCLS)
        {
            UsuarioCLS oUsuario = new UsuarioCLS();
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
                        Usuario oUsuarioRecuerar = bd.Usuarios.Where(p => p.NombreUser.ToUpper() == oUsuarioCLS.NombreUser.ToUpper()
                        && p.Contrasenia == claveCifrada).First();
                        HttpContext.Session.SetString("empleado", oUsuarioRecuerar.IdUsuario.ToString());
                        HttpContext.Session.SetString("tipoEmpleado", oUsuarioRecuerar.IdTipoUsuario.ToString());
                        oUsuario.IdUsuario = oUsuarioRecuerar.IdUsuario;
                        oUsuario.NombreUser = oUsuarioRecuerar.NombreUser;
                    }
                    else
                    {
                        oUsuario.IdUsuario = 0;
                        oUsuario.NombreUser = "";
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            return oUsuario;
        }
        //*****************FIN LOGIN **************************************

    }
}
