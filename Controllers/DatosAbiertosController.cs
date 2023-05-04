using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.clases.Reportes;
using MVPSA_V2022.Modelos;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ReportesMVC.Reportes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPSA_V2022.Controllers
{
    [Authorize]
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
            string dateString = DateTime.Now.ToString("dd-MM-yyTHHmm"); //yyyyMMddTHHmmssZ
            string path = @"C:\Generacion_Datasets\Economicos\";
            string filename = "impuestos" + dateString + ".csv";
            var builder = new StringBuilder();
            builder.AppendLine("Mes,Año,FechaVencimiento,Estado,ImporteBase,InteresValor,ImporteFinal,IdLote");
            List<ImpuestoInmobiliarioCLS> listaImpuestos;
            try
            {

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
                                         // interesValor = (decimal)impuestoinmobiliario.InteresValor,
                                          importeFinal = (decimal)impuestoinmobiliario.ImporteFinal,
                                          idLote = (int)impuestoinmobiliario.IdLote

                                      }).ToList();
                    // return listaTipoDenuncia;

                }
                try { 
            foreach (var impuesto in listaImpuestos)
                {
                    builder.AppendLine($"{impuesto.mes},{impuesto.anio},{impuesto.fechaVencimiento},{impuesto.estado},{impuesto.importeBase},{impuesto.interesValor},{impuesto.importeFinal},{impuesto.idLote}");
                }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest(ex);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex);
            }
            return (IActionResult)Task.CompletedTask;
        }


        //Obtencion listado de Datos Abiertos.
        //NO Posee !String.IsNullOrEmpty(persona.Mail) ? persona.Mail : "No Posee",
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
                                           idArchivo=datosAbiertos.IdArchivo,
                                           nombreArchivo = !String.IsNullOrEmpty(datosAbiertos.NombreArchivo) ? datosAbiertos.NombreArchivo : "No Posee",
                                           tamaño = datosAbiertos.Tamaño,
                                           ubicacion = !String.IsNullOrEmpty(datosAbiertos.Ubicacion) ? datosAbiertos.Ubicacion : "No Posee"
                                       }).ToList();
                return listaDatosFincanzas;
            }

        }
        //genearar reporte excel
        //este metodo generara un flujo en base 64 que es lo que se usa para el excel.

        [HttpGet]
        [Route("api/DatosAbiertos/generarregistrosImpuestosDeben")]
        public string generarregistrosImpuestosDeben()
        {
            string titulo = "Lotes Deuda año "+ DateTime.Now.Year;

            using (MemoryStream ms = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage ep = new ExcelPackage();
                ep.Workbook.Worksheets.Add("Impuestos Lotes Adeudados");
                ExcelWorksheet ew1 = ep.Workbook.Worksheets[0];
        
                Excel.tituloHorizontal(ew1, titulo, 1, 1, 3,18);
                Excel.anchosColumnas(ew1, 1, 3, new List<int> { 18, 18, 18 });
                Excel.cabecerasTabla(ew1, 2, 1, new List<string> { "Manzana", "Lote", "Mes que Adeuda" });

                List<LoteImpuestoCLS> listaImpuestos= new List<LoteImpuestoCLS>();  
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {

                         listaImpuestos = (from II in bd.Impuestoinmobiliarios join L in bd.Lotes
                                      on II.IdLote equals L.IdLote
                                           where II.Año == 2023 & II.Estado==0 & II.Mes > 0
                                           select new LoteImpuestoCLS
                                           {
                                               mes = (int)II.Mes,
                                               manzana = (int)L.Manzana,
                                              nroLote  = (int)L.NroLote

                                           }).ToList();
                   
                }

                int iniciofila = 3;
                for (int i = 0; i < listaImpuestos.Count; i++)
                {
                    ew1.Cells[iniciofila, 1].Value = listaImpuestos[i].manzana;
                    ew1.Cells[iniciofila, 2].Value = listaImpuestos[i].nroLote;
                    ew1.Cells[iniciofila, 3].Value = listaImpuestos[i].mes;
                    iniciofila++;
                }
                Excel.contenido(ew1, 1, 3, 3, listaImpuestos);
                ew1.Column(1).Width = 30;
                ep.SaveAs(ms);
                byte[] buffer = ms.ToArray();
                return Convert.ToBase64String(buffer);




            }

        }

        [HttpGet]
        [Route("api/DatosAbiertos/generarregistrosImpuestosDebenPDF")]
        public string generarregistrosImpuestosDebenPDF()
        {
            List<string> cabeceras = new List<string>
            {
                "Manzana","Lote","Mes Adeudado"
            };
            List<LoteImpuestoCLS> listaImpuestos = new List<LoteImpuestoCLS>();
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaImpuestos = (from II in bd.Impuestoinmobiliarios
                                  join L in bd.Lotes
                             on II.IdLote equals L.IdLote
                                  where II.Año == 2023 & II.Estado == 0 & II.Mes>0
                                  select new LoteImpuestoCLS
                                  {
                                      nroLote = (int)L.NroLote,
                                      manzana = (int)L.Manzana,
                                      mes = (int)II.Mes
                                  }).ToList();

                using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                using (PdfDocument pdfDoc = new PdfDocument(writer))
                {
               //===============================Declaro el file Path, nombre del archivo==============================================
                        string pdfPathFolder = "";
                pdfPathFolder = "C:\\Generacion_Datasets\\Economicos\\pdfLotes\\";
                
                
                if (!Directory.Exists(pdfPathFolder))
                {
                    Directory.CreateDirectory(pdfPathFolder);
                }
                string fileName = "Impuestos" + DateTime.Now.ToString("dd-MM-yy") + ".pdf"; //("dd-MM-yyTHHmm")
                        string fullFilePath = System.IO.Path.Combine(pdfPathFolder, fileName);
                      DeviceRgb purpleColour = new(128, 0, 128);
                      DeviceRgb offPurpleColour = new(230, 230, 250);
                        HeaderFooterEventHandler handler = new HeaderFooterEventHandler();
                        //Aqui el pdf que se guarda en el server
                        PdfDocument pdfDocument = new(new PdfWriter(new FileStream(fullFilePath, FileMode.Create, FileAccess.Write)));
                        pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);
                        Document document = new(pdfDocument);//, PageSize.A4, false); //find a way to include the margins
                        document.SetMargins(40, 20, 20, 40);
                        PDF.crearParrafo(document, "Impuestos Adeudados", 20, "center");
                        //Aca va el de crear tabla con la llamada al metodo para pintar los datos..
                        PDF.crearTabla<LoteImpuestoCLS>(document, cabeceras, new List<float> { 10, 40, 15 }, listaImpuestos, new List<string> { "Manzana", "Lote", "Mes que Adeuda" });


                        //Aqui el pdf que se descarga en la pc

                        pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);
                        Document doc = new Document(pdfDoc);
                        doc.SetMargins(40, 20, 20, 40);
                        PDF.crearParrafo(doc, "Impuestos Adeudados", 20, "center");
                        //Aca va el de crear tabla con la llamada al metodo para pintar los datos..
                        PDF.crearTabla<LoteImpuestoCLS>(doc, cabeceras,new List<float> {10,40,15 } ,listaImpuestos, new List<string> { "Manzana", "Lote", "Mes que Adeuda" });
                       document.Close();
                        doc.Close();
                        writer.Close();

                }
                    return Convert.ToBase64String(ms.ToArray());
                }

            }
        }
            [HttpGet]
        [Route("api/DatosAbiertos/eliminarDataset/{idArchivo}")]
        public int eliminarDataset(int idArchivo)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    DatosAbierto oDato = bd.DatosAbiertos.Where(p => p.IdArchivo == idArchivo).First();
                    oDato.Bhabilitado = 0;
                    bd.SaveChanges();
                    rpta = 1;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                rpta = 0;
            }
            return rpta;
        }




    }
}
