using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace MVPSA_V2022.Controllers
{
    public class VecinoController : Controller
    {
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
        [HttpPost]
        [Route("api/Vecino/login")]
        public VecinoCLS login([FromBody] VecinoCLS oVecinoCLS)
        {
            VecinoCLS oUsuario = new VecinoCLS();
            int rpta = 0;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                SHA256Managed sha = new SHA256Managed();
                byte[] dataNocifrada = Encoding.Default.GetBytes(oVecinoCLS.Contrasenia);
                byte[] dataCifrada = sha.ComputeHash(dataNocifrada);
                string claveCifrada = BitConverter.ToString(dataCifrada).Replace("-", "");

                rpta = bd.UsuarioVecinos.Where(p => p.NombreUser.ToUpper() == oVecinoCLS.NombreUser.ToUpper() && p.Contrasenia == claveCifrada).Count();
                if (rpta == 1)
                {
                    UsuarioVecino oUsuarioRecuperar = bd.UsuarioVecinos.Where(p => p.NombreUser.ToUpper() == oVecinoCLS.NombreUser.ToUpper()
                    && p.Contrasenia == claveCifrada).First();

                    // Seteamos en la Sesion el Id de Vecino
                    HttpContext.Session.SetString("vecino", oUsuarioRecuperar.IdVecino.ToString());
                    // Seteamos en la Sesion el nombre del vecino
                    HttpContext.Session.SetString("nombreVecino", oUsuarioRecuperar.NombreUser.ToString());
                    oUsuario.IdVecino = oUsuarioRecuperar.IdVecino;
                    oUsuario.NombreUser = oUsuarioRecuperar.NombreUser;
                }
                else
                {
                    oUsuario.IdVecino = 0;
                    oUsuario.NombreUser = "";
                }
            }
            return oUsuario;
        }
        //*****************FIN LOGIN **************************************


    }
}
