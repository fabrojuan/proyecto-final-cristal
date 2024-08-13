using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Transactions;
using static System.Net.WebRequestMethods;

namespace MVPSA_V2022.Controllers
{
    [Authorize]
    public class ImpuestosController : Controller
    {

        private readonly IImpuestoService impuestoService;
        private readonly M_VPSA_V3Context dbContext;

        public ImpuestosController(IImpuestoService impuestoService, M_VPSA_V3Context dbContext)
        {
            this.impuestoService = impuestoService;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Funciona para obtener la ultima fecha de ejecucion Mensual..
        [HttpGet]
        [Route("api/impuestos/getUltimaFechaInteres")]
        public ControlProcesosCLS getUltimaFechaInteres()
        {
            ControlProcesosCLS oCtrlCLS = new ControlProcesosCLS();
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    oCtrlCLS = (from CONTROLPROCESOS in bd.ControlProcesos
                                where CONTROLPROCESOS.IdProceso == 2
                                select new ControlProcesosCLS
                                {
                                    idEjecucion = CONTROLPROCESOS.IdEjecucion,
                                    idProceso = CONTROLPROCESOS.IdProceso,
                                    fechaEjecucion = (DateTime)CONTROLPROCESOS.FechaEjecucion
                                }).OrderBy(CONTROLPROCESOS => CONTROLPROCESOS.fechaEjecucion).Last();
                    //.Last(); Probamos el descending en vez de last solo a ver que onda. Con El order By va como piña
                    return oCtrlCLS;
                }
            }
            catch (Exception ex)
            {
                oCtrlCLS.Error = ex.Message;
            }
            return oCtrlCLS;

        }
        //Fin ejecucion Interes Mensual.
        [HttpGet]
        [Route("api/impuestos/getUltimaFechaBoleta")]
        public ControlProcesosCLS getUltimaFechaBoleta()
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                ControlProcesosCLS oCtrlCLS = (from CONTROLPROCESOS in bd.ControlProcesos
                                               where CONTROLPROCESOS.IdProceso == 3
                                               select new ControlProcesosCLS
                                               {
                                                   idEjecucion = CONTROLPROCESOS.IdEjecucion,
                                                   idProceso = CONTROLPROCESOS.IdProceso,
                                                   fechaEjecucion = (DateTime)CONTROLPROCESOS.FechaEjecucion
                                               }).Last();
                return oCtrlCLS;
            }
        }
        //Fin ejecucion Confirmacion Boletas.
        //Impuestos Por Lote 
        [HttpGet]
        [Route("api/impuestos/{idLote}/impagos")]
        [AllowAnonymous]
        public IEnumerable<ImpuestoInmobiliarioCLS> ListarTrabajos(int idLote)
        {
            List<ImpuestoInmobiliarioCLS> listaImpuestos;

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaImpuestos = (from impuestoinmobiliario in bd.Impuestoinmobiliarios
                                  where impuestoinmobiliario.IdLote == idLote
                                  && impuestoinmobiliario.Estado == 0 
                                  select new ImpuestoInmobiliarioCLS

                                  {

                                      idImpuesto = (int)impuestoinmobiliario.IdImpuesto,
                                      mes = (int)impuestoinmobiliario.Mes,
                                      anio = (int)impuestoinmobiliario.Año,
                                      importeBase = (decimal)impuestoinmobiliario.ImporteBase,
                                      interesValor = (decimal)impuestoinmobiliario.InteresValor,
                                      importeNeto = (decimal)impuestoinmobiliario.ImporteFinal - (decimal)impuestoinmobiliario.InteresValor,
                                      importeFinal = (decimal)impuestoinmobiliario.ImporteFinal,
                                      periodo = impuestoinmobiliario.Mes == 0 ?  impuestoinmobiliario.Año.ToString() : 
                                      impuestoinmobiliario.Año.ToString() + "/" + impuestoinmobiliario.Mes.ToString(),
                                      fechaVencimiento = (DateTime)impuestoinmobiliario.FechaVencimiento,
                                      cuota = impuestoinmobiliario.Mes == 0 ? "Única" : impuestoinmobiliario.Mes .ToString(),
                                      estaVencido = impuestoinmobiliario.FechaVencimiento < DateTime.Now
                                  }).ToList();

                return listaImpuestos;
            }
        }

        [HttpGet]
        [Route("api/impuestos/{idLote}/pagos")]
        [AllowAnonymous]
        public IEnumerable<ImpuestoPagoDto> listarImpuestosPagados(int idLote) {
            return impuestoService.listarImpuestosPagados(idLote);
        }


        //where trabajo.Bhabilitado ==1
        ///SP Generacion Anual de Impuestos
        [HttpPost]
        [Route("api/impuestos/SP_GeneracionImpuestos")]
        public ResultadoEjecucionProcesoCLS generarImpuestos([FromBody] SolicitudGeneracionImpuestosCLS solicitudGeneracionImpuestosCLS)
        {
            return impuestoService.generarImpuestos(solicitudGeneracionImpuestosCLS);

        }
        ///SP Generacion Mensual Impuestos
        [HttpGet]
        [Route("api/impuestos/SP_GeneracionInteresesMensuales")]
        public ResultadoEjecucionProcesoCLS SP_GeneracionInteresesMensuales()
        {
            return impuestoService.generarIntesesMensuales();


        }
        // Procedimiento almacenado [BORRADO_BOLETAS]
        [HttpGet]
        [Route("api/impuestos/SP_LimpiezaBoletas")]
        public ResultadoEjecucionProcesoCLS SP_LimpiezaBoletas()
        {
            return impuestoService.confirmarBoletas();
        }

        [HttpPost]
        [Route("api/impuestos/guardarBoleta")]
        [AllowAnonymous]
        public IActionResult guardarBoleta([FromBody] DetalleBoletaCLS oDetalleBoletaCLS)
        //  public async Task<IActionResult> guardarBoleta([FromBody] DetalleBoletaCLS oDetalleBoletaCLS)

        {
            //objeto global boleta para enviar a mobbex
            BoletaCLS oBoletaCLS = new BoletaCLS();
            Customer customer = new Customer();
            PersonaCLS oPersonaCLS = new PersonaCLS();
            RespCheckoutMOBXX rtaChMobxx = null;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    int idLoteaPagar = oDetalleBoletaCLS.idLote;

                    oPersonaCLS = (from Lote in bd.Lotes
                                   join Persona in bd.Personas
                                   on Lote.IdPersona equals Persona.IdPersona
                                   where Persona.Bhabilitado == 1
                                  && Lote.IdLote == idLoteaPagar
                                   select new PersonaCLS
                                   {
                                       Nombre = Persona.Nombre,
                                       apellido = Persona.Apellido,
                                       Mail = Persona.Mail,
                                       Dni = Persona.Dni

                                   }).First();
                    customer.email = oPersonaCLS.Mail;
                    customer.identification = oPersonaCLS.Dni;
                    customer.name = oPersonaCLS.Nombre + " " + oPersonaCLS.apellido;
                    //AL finalizar la obtencion dinamica de la persona ahora eliminaremos la sesion temporal del backend 
                    
                    //Usaremos transacciones porque tocaremos dos tablas imultaneamente.
                    using (var transaccion = new TransactionScope())
                    {
                        Boletum oBoleta = new Boletum();
                        oBoleta.FechaGenerada = DateTime.Now;
                        oBoleta.Estado = 0;

                        oBoleta.TipoMoneda = "AR$";
                        oBoleta.Url = "https://POSTNGROK";
                        oBoleta.Bhabilitado = 0;
                        bd.Boleta.Add(oBoleta);
                        bd.SaveChanges();
                        // Debo aca  y luego consultar por 
                        // el ultimo id de boleta generado con LAST

                        oBoletaCLS = (from boleta in bd.Boleta
                                      where boleta.Estado == 0
                                      select new BoletaCLS
                                      {
                                          idBoleta = boleta.IdBoleta,
                                      }).OrderBy(boleta => boleta.idBoleta).Last();




                        int idBoleta = oBoletaCLS.idBoleta;
                        String[] idsDetalles = oDetalleBoletaCLS.Valores.Split("-"); //Separo los valores obtenidos enla variable que tienen el guion como separador y los transformo en un array de objetos separados por coma.
                        for (int i = 0; i < idsDetalles.Length; i++)
                        {
                            Detalleboletum oDetalleboleta = new Detalleboletum();
                            oDetalleboleta.IdBoleta = idBoleta;
                            oDetalleboleta.IdImpuesto = int.Parse(idsDetalles[i]);
                            oDetalleboleta.Estado = 0;
                            bd.Detalleboleta.Add(oDetalleboleta);
                        }

                        bd.SaveChanges();
                        // bd.Database.ExecuteSqlCommand("GENERACION_IMPORTE_BOLETA"); reeemplazado por
                        bd.Database.ExecuteSqlRaw("GENERACION_IMPORTE_BOLETA");
                        Boletum oBoleta2 = bd.Boleta.OrderBy(idBoleta => idBoleta).Last();
                        transaccion.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                Boletum oBoleta3 = new Boletum();
                try
                {
                    oBoleta3 = bd.Boleta.Where(b => b.IdBoleta == oBoletaCLS.idBoleta).First();
                    oBoletaCLS.importe = oBoleta3.Importe;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            BoletaMobexx oBolMobexx = new BoletaMobexx(customer.email, customer.name, customer.identification);
            oBolMobexx.total = (decimal)oBoletaCLS.importe;
            oBolMobexx.description = "Pago de Impuestos Vecinos Villa Parque Santa Ana";
            oBolMobexx.reference = oBoletaCLS.idBoleta;
            oBolMobexx.currency = "ARS";
            oBolMobexx.test = true;
            oBolMobexx.return_url = "https://cristal-vpsa.ngrok.dev";
            oBolMobexx.webhook = "https://cristal-vpsa.ngrok.dev/api/pagos/mobbex";
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(oBolMobexx);
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("x-api-key", "BQ32LI_kA4b3DgIfCuYYaRZJcxPRQqt52LXarr_Q");
                    httpClient.DefaultRequestHeaders.Add("x-access-token", "0aac0a06-799e-4251-aff1-11d079ee79f7");

                    //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    //request.Content.Headers.Add("Content-Type", "application/json");
                    var uri = "https://api.mobbex.com/p/checkout";

                    var response = httpClient.PostAsync(uri, new StringContent(jsonString, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //LA VARIANLE Cuerpo es la que tiene el Ok devuelto por mobexx y debo redireccionar al site para el pago efectivo.
                        var cuerpo = response.Content.ReadAsStringAsync().Result;
                        rtaChMobxx = JsonConvert.DeserializeObject<RespCheckoutMOBXX>(cuerpo);
                        rtaChMobxx = new RespCheckoutMOBXX(rtaChMobxx.data.id, rtaChMobxx.data.url, rtaChMobxx.data.description, rtaChMobxx.data.total, rtaChMobxx.data.created);
                        HttpContext.Session.SetString("urlMobbex", rtaChMobxx.data.url.ToString());

                        //return Redirect(rtaChMobxx.data.url.ToString());  //RedirectToAction
                        // esta joya.
                        //Console.WriteLine("La respuesta es: " + rtaChMobxx.data.url.ToString());
                        //ATENTO ACA HAY ALGO RARO!!!!!
                        var camposConErrores = Utilidades.ExtraerErrorDelWebApi(cuerpo);
                        if (!(camposConErrores == null))
                        {
                            foreach (var campoErrores in camposConErrores)
                            {
                                Console.WriteLine($"-{campoErrores}");
                                foreach (var error in campoErrores.Value)
                                {
                                    Console.WriteLine($"-{error}");
                                }
                            }
                            Console.WriteLine("La respuesta es: " + cuerpo);
                        }
                    }
                    else
                    {
                        var cuerpo = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("La respuesta es: " + cuerpo);
                    }

                    response.EnsureSuccessStatusCode();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            var urlMobbex = new UrlMobexx();
            urlMobbex.urlMobexx = rtaChMobxx!.data.url.ToString();
            return Ok(urlMobbex);
            //return Ok(WebUtility.UrlEncode(rtaChMobxx!.data.url.ToString()));
            //return Ok(UrlEncoder.Create().Encode(rtaChMobxx!.data.url) );  //

            //return Ok();
        }   //FIN GUARdar Boleta y enviar a mobex.

        //OBTENER VARIABLE DE SESSION Url Mobexx ver cuando destruirla.....
        [HttpGet]
        [Route("api/impuesto/obtenerUrlMobbexx")]
        public RedirectResult obtenerUrlMobbexx()
        {
            dataUrlMbxCLS oDataUrlMbx = new dataUrlMbxCLS();

            var varUrl = HttpContext.Session.GetString("urlMobbex");
            if (varUrl == null)
            {
                oDataUrlMbx.valor = "/";
            }
            else
            {

                oDataUrlMbx.valor = varUrl;
            }
            return Redirect(oDataUrlMbx.valor);
        }

        [HttpGet]
        [Route("api/impuestos/obtenerUrlMobbexx2")]
        [AllowAnonymous]
        public UrlMobexx obtenerUrlMobbexx2()
        {
            UrlMobexx oUrlMbx = new UrlMobexx();

            var varUrl = HttpContext.Session.GetString("urlMobbex");
            if (varUrl == null)
            {
                oUrlMbx.urlMobexx = "/";
            }
            else
            {

                oUrlMbx.urlMobexx = varUrl;
            }
            return oUrlMbx;
        }

        [HttpGet]
        [Route("api/impuestos/EstadoImpuestosLote/{idLote}")]
        [AllowAnonymous]
        public void getEstadoImpositivoCuenta(int idLote) {
            //this.dbContext.Impuestoinmobiliarios.Where(x => x.IdLote == idLote)
        }


    }
}
