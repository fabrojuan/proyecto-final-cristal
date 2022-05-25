using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPSA_V2022.Controllers
{
    public class DatosAbiertosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/DatosAbiertos/generaImpuestoInmobiliarioMensual")]
        [Produces("text/csv")]
        public async Task<IActionResult> generaImpuestoInmobiliarioMensual()
        {
            //string dateString = DateTime.Today.ToShortDateString();
            string dateString = DateTime.Now.ToString("yyyyMMddTHHmmssZ");
            string path = @"D:\MUNICIPALIDAD\datosAbiertos_CSV\economicos\";
            //string path = @"D:\MUNICIPALIDAD\"+"impuestos"+dateString +".csv";
            string filename = "impuestos" + dateString + ".csv";
            var builder = new StringBuilder();
            builder.AppendLine("Mes,Año,FechaVencimiento,Estado,ImporteBase,InteresValor,ImporteFinal,IdLote");
            List<ImpuestoInmobiliarioCLS> listaImpuestos;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaImpuestos = (from impuestoinmobiliario in bd.Impuestoinmobiliarios
                                  where impuestoinmobiliario.Bhabilitado == 1
                                  select new ImpuestoInmobiliarioCLS
                                  {
                                      mes = (int)impuestoinmobiliario.Mes,
                                      anio = (int)impuestoinmobiliario.Año,
                                      fechaVencimiento = (DateTime)impuestoinmobiliario.FechaVencimiento,
                                      estado = (int)impuestoinmobiliario.Estado,
                                      importeBase = (decimal)impuestoinmobiliario.ImporteBase,
                                      interesValor = (decimal)impuestoinmobiliario.InteresValor,
                                      importeFinal = (decimal)impuestoinmobiliario.ImporteFinal,
                                      idLote = (int)impuestoinmobiliario.IdLote

                                  }).ToList();
                // return listaTipoDenuncia;

            }
            foreach (var impuesto in listaImpuestos)
            {
                builder.AppendLine($"{impuesto.mes},{impuesto.anio},{impuesto.fechaVencimiento},{impuesto.estado},{impuesto.importeBase},{impuesto.interesValor},{impuesto.importeFinal},{impuesto.idLote}");
            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, filename)))
            {
                await outputFile.WriteAsync(builder.ToString());


            }

            //Metodo para insertar la metadata en sql server y recuperar el dato abierto 
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                DatosAbierto oDatos = new DatosAbierto();
                // oDenuncia.CodTipoDenuncia = int.Parse(DenunciaCLS2.Tipo_Denuncia);
                oDatos.Bhabilitado = 1;
                oDatos.IdTipoDato = 5;
                oDatos.Ubicacion = path;
                // reemplazo el path por el hardcodeo del web server que lo levanto en pwerchell de visual studio(string)path;
                oDatos.NombreArchivo = (string)filename;
                oDatos.Tamaño = (int)builder.Length;
                oDatos.Extension = "csv";
                bd.Add(oDatos);
                bd.SaveChanges();

            }
            return (IActionResult)Task.CompletedTask;
        }


        //Obtencion listado de Datos Abiertos.

        [HttpGet]
        [Route("api/DatosAbiertos/ListarFinancieros")]
        public IEnumerable<DatosAbiertosCLS> ListarFinancieros()
        {
            List<DatosAbiertosCLS> listaDatosFincanzas;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaDatosFincanzas = (from datosAbiertos in bd.DatosAbiertos
                                       where datosAbiertos.Bhabilitado == 1
                                       select new DatosAbiertosCLS
                                       {
                                           nombreArchivo = datosAbiertos.NombreArchivo,
                                           tamaño = datosAbiertos.Tamaño,
                                           ubicacion = datosAbiertos.Ubicacion
                                       }).ToList();
                return listaDatosFincanzas;
            }

        }



    }
}
