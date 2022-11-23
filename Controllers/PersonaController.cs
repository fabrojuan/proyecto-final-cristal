using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System.Globalization;

namespace MVPSA_V2022.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //********************* Registrar Persona ******************************************
        [HttpPost]
        [Route("api/Persona/guardarPersona")]
        public int guardarPersona([FromBody] PersonaLoteCLS oPersonaCLS)
        {
            if (oPersonaCLS is null)
            {
                throw new ArgumentNullException(nameof(oPersonaCLS));
            }

            int rpta = 0;

            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    if (oPersonaCLS.Iidpersona == 0)
                    {
                        //Resolver duplicacion de persona porque tambien lo registro cuando genero un lote, el vecino si se registra luego 
                        // de generar el lote deberia actualizar solo telefono y mail ya que los otros datos los tendremos por dni.
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                        string FechaFormateada = oPersonaCLS.FechaNac.Replace("/", "-");
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                        DateTime dt =
                        DateTime.ParseExact(FechaFormateada, "dd-MM-yyyy", CultureInfo.InvariantCulture);
#pragma warning restore CS8604 //Posible argumento de referencia nulo
                        Persona oPersona = new Persona();
                        oPersona.Nombre = oPersonaCLS.Nombre;
                        oPersona.Apellido = oPersonaCLS.Apellido;
                        oPersona.Telefono = oPersonaCLS.Telefono;
                        oPersona.Dni = oPersonaCLS.Dni;
                        oPersona.Bhabilitado = 1;
                        oPersona.Domicilio = oPersonaCLS.Domicilio;
                        oPersona.Altura = oPersonaCLS.Altura;
                        oPersona.Mail = oPersonaCLS.Mail;
                        oPersona.FechaNac = dt;
                        bd.Personas.Add(oPersona);
                        bd.SaveChanges();

                        //Para el caso de edicion no hace falta hacer nada porque la relacion a persona ya esta                     
                        rpta = 1;
                    }
                    else
                    {    //FaltaTerminar LA EDICION
                        Usuario oUsuario = bd.Usuarios.Where(p => p.IdUsuario == oPersonaCLS.Iidpersona).First();
                        bd.SaveChanges();

                        rpta = 1;
                    }
                }
                rpta = 1;
            }

            catch (DbUpdateException ex)
            {
                //Si el error es por mail ducplicado como hago para enviar ese error
                //con un codigo de error al html que sea con un valor duplicado
                ModelState.AddModelError("", "No pueden salvarse los cambios. " +
                        "Intentalo de nuevo cambiando todos los valores " +
                        "si el problema persiste llamalo a Roman..");
                rpta = 0;
                Console.WriteLine(ex.ToString());
            }
            return rpta;
        }
        //*********************Fin Registrar Persona ******************************************

        //********************* Buscar si existe Persona ******************************************

        [Route("api/Persona/RecuperarPersonaPreExistente/{dniTitular}")]
        public int RecuperarPersonaPreExistente(string dniTitular)
        {
            int rpta = 0;

            PersonaLoteCLS oPersonaCLS2 = new PersonaLoteCLS();
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    oPersonaCLS2 = (from persona in bd.Personas

                                    where persona.Bhabilitado == 1
                                    && persona.Dni == dniTitular
                                    select new PersonaLoteCLS
                                    {
                                        Dni = persona.Dni
                                    }).First();

                }
                if (oPersonaCLS2.Dni != null)
                    rpta = 1;
                else
                { rpta = 0; }
            }
            catch (Exception ex)
            {
                oPersonaCLS2.Error = ex.Message;
                return rpta = 0;
            }

            return rpta;
        }




    }
}
