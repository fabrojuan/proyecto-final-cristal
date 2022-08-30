using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases.Mobbex;
using MVPSA_V2022.Exceptions;
using MVPSA_V2022.Services;
using System.Text;

namespace MVPSA_V2022.Controllers
{
    [Route("api/pagos")]
    public class PagoController : Controller
    {

        private readonly IPagoService pagoService;

        public PagoController(IPagoService pagoService)
        {
            this.pagoService = pagoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("mobbex")]
        public IActionResult registrarPagoMobbex([FromBody] PagoCLS pago) {
            try
            {
                if (!ModelState.IsValid)
                {
                    StringBuilder errors = new StringBuilder("Errores: ");
                    ModelState.Keys.ToList().ForEach(key => errors.Append(key.ToString() + ". "));
                    Console.WriteLine(errors);
                    throw new PagoNoValidoException(errors.ToString());
                }

                pagoService.registrarPagoMobbex(pago);
                return Ok();
            }
            catch (PagoNoValidoException pnv)
            {
                return BadRequest(pnv.Message);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
