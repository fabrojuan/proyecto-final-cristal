using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
using static System.Linq.Enumerable;
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
            string path = @"C:\Proyecto_JUAN\proyecto-final-cristal\ClientApp\src\assets\DatosAbiertos\";
            //  string path = @"C:\Proyecto_JUAN\proyecto-final-cristal\ClientApp\src\assets\DatosAbiertos\";
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
                                           idArchivo = datosAbiertos.IdArchivo,
                                           nombreArchivo = !String.IsNullOrEmpty(datosAbiertos.NombreArchivo) ? datosAbiertos.NombreArchivo : "No Posee",
                                           tamaño = datosAbiertos.Tamaño,
                                           extension=datosAbiertos.Extension,
                                           ubicacion = !String.IsNullOrEmpty(datosAbiertos.Ubicacion) ? datosAbiertos.Ubicacion : "No Posee"
                                       }).ToList();
                return listaDatosFincanzas;
            }

        }

        //funcion para la descarga de archivos desde datos abiertos visibles para los vecinos.
        [HttpGet]
        [Route("api/DatosAbiertos/DescargarArchivo/{nombreArchivo}")]
        public async Task<IActionResult> DescargarArchivo(string nombreArchivo)
        {
            var rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp\\src\\assets\\DatosAbiertos\\", nombreArchivo); // Reemplaza "nombreCarpeta" con la carpeta donde se encuentran los archivos
            if (!System.IO.File.Exists(rutaArchivo))
            {
                throw new FileNotFoundException($"El archivo '{nombreArchivo}' no fue encontrado.");
            }

            var memory = new MemoryStream();
            try { 
            using (var stream = new FileStream(rutaArchivo, FileMode.Open))
            {
                using (var reader = new StreamReader(stream, Encoding.Default)) // Leer con la codificación actual
                {
                    var contenido = await reader.ReadToEndAsync();
                    var utf8Bytes = Encoding.UTF8.GetBytes(contenido);
                    memory.Write(utf8Bytes, 0, utf8Bytes.Length);
                }
            }
            memory.Position = 0;

                return File(memory, "application/octet-stream", nombreArchivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Ocurrió un error durante la descarga del archivo. Por favor, inténtalo nuevamente.");
            }
        }
        [HttpGet]
        [Route("api/DatosAbiertos/DescargarArchivoExcel/{nombreArchivo}")]
        public async Task<IActionResult> DescargarArchivoExcel(string nombreArchivo)
        {
            var rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp\\src\\assets\\DatosAbiertos\\", nombreArchivo); // Reemplaza "nombreCarpeta" con la carpeta donde se encuentran los archivos
            if (!System.IO.File.Exists(rutaArchivo))
            {
                throw new FileNotFoundException($"El archivo '{nombreArchivo}' no fue encontrado.");
            }

            var memory = new MemoryStream();
            try
            {
                using (var stream = new FileStream(rutaArchivo, FileMode.Open))
                {
                    var excelPackage = new ExcelPackage(stream);
                    var excelBytes = excelPackage.GetAsByteArray();
                    memory.Write(excelBytes, 0, excelBytes.Length);
                }
                memory.Position = 0;

                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Ocurrió un error durante la descarga del archivo. Por favor, inténtalo nuevamente.");
            }
        }
        //generacion de descarga de pedf 
        [HttpGet]
        [Route("api/DatosAbiertos/DescargarArchivoPDF/{nombreArchivo}")]
        public async Task<IActionResult> DescargarArchivoPDF(string nombreArchivo)
        {
            string pdfPathFolder = "";
            pdfPathFolder = "C:\\Proyecto_JUAN\\proyecto-final-cristal\\ClientApp\\src\\assets\\DatosAbiertos\\";

            
            //string fullFilePath = $"{Path.Combine(Directory.GetCurrentDirectory(), "archivosGenerados")}\\{nombreArchivo}.pdf";
           string fullFilePath = pdfPathFolder + nombreArchivo;
            if (!System.IO.File.Exists(fullFilePath))
            {
                return NotFound("El archivo solicitado no pudo ser encontrado");
            }

            var memory = new MemoryStream();
            try
            {
                using (var fileStream = new FileStream(fullFilePath, FileMode.Open))
                {
                    await fileStream.CopyToAsync(memory);
                }
                memory.Position = 0;

                //return File(memory, "application/pdf", $"archivo_{nombreArchivo}.pdf");
                return File(memory, "application/octet-stream", nombreArchivo);

            }
            finally
            {
                //memory.Dispose();
            }
        }

        
        //sanitiza las urls para que el navegador no genere errores
        [HttpGet]
        [Route("api/DatosAbiertos/SanitizarUrls")]
        public IEnumerable<SanitizarUrlCLS> SanitizarUrls()
        {
            List<SanitizarUrlCLS> listaUrls = new List<SanitizarUrlCLS>();
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {

                    listaUrls = (from datosAbiertos in bd.DatosAbiertos
                                 where datosAbiertos.Bhabilitado == 1
                                 select new SanitizarUrlCLS
                                 {
                                     //ubicacion = (!String.IsNullOrEmpty(datosAbiertos.Ubicacion) ? datosAbiertos.Ubicacion : "No Posee") + (!String.IsNullOrEmpty(datosAbiertos.NombreArchivo) ? datosAbiertos.NombreArchivo : "No Posee")
                                     ubicacion = (datosAbiertos.Ubicacion) + (!String.IsNullOrEmpty(datosAbiertos.NombreArchivo.ToString()) ? datosAbiertos.NombreArchivo : "No Posee")
                                 }).ToList();
                    return listaUrls;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return listaUrls;
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
                                           where II.Año == 2024 & II.Estado==0 & II.Mes > 0
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
                long fileSizeInBytes = ms.Length;
                double tamanioenKB = fileSizeInBytes / 1024; // Tamaño en KB
                // Guardar el archivo en una ubicación específica con un nombre personalizado
                string rutaCarpeta = @"C:\Proyecto_JUAN\proyecto-final-cristal\ClientApp\src\assets\DatosAbiertos\"; // Ruta de la carpeta donde deseas guardar el archivo
                string dateString = DateTime.Now.ToString("dd-MM-yyTHHmm"); //yyyyMMddTHHmmssZ
               
                string nombreArchivo = "impuestos" + dateString + ".xlsx"; // Nombre del archivo
                

                ep.SaveAs(new FileInfo(Path.Combine(rutaCarpeta, nombreArchivo)));
                
                    using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    DatosAbierto oDatos = new DatosAbierto();
                    // oDenuncia.CodTipoDenuncia = int.Parse(DenunciaCLS2.Tipo_Denuncia);
                    oDatos.Bhabilitado = 1;
                    oDatos.IdTipoDato = 5;
                    oDatos.Ubicacion = rutaCarpeta;
                    oDatos.NombreArchivo = (string)nombreArchivo;
                    oDatos.Tamaño = (int)fileSizeInBytes;
                    oDatos.Extension = "XLSX";
                    bd.Add(oDatos);
                    bd.SaveChanges();

                }
                //si queremos que ademas lo abra es la linea que sigue a continuacion.

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
                                  where II.Año == 2024 & II.Estado == 0 & II.Mes>0
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
                pdfPathFolder ="C:\\Proyecto_JUAN\\proyecto-final-cristal\\ClientApp\\src\\assets\\DatosAbiertos\\";

                             

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
                        MemoryStream memoryStream = new MemoryStream();
                        //Aca va el de crear tabla con la llamada al metodo para pintar los datos..
                        PDF.crearTabla<LoteImpuestoCLS>(doc, cabeceras,new List<float> {10,40,15 } ,listaImpuestos, new List<string> { "Manzana", "Lote", "Mes que Adeuda" });
                       document.Close();
                        doc.Close();
                        writer.Close();

                      
                        pdfDocument.Close(); // Cerrar el documento antes de copiarlo al MemoryStream

                        using (FileStream fileStream = new FileStream(fullFilePath, FileMode.Open))
                        {
                            fileStream.CopyTo(memoryStream);
                        }

                        // Obtener el tamaño del archivo en bytes
                        long fileSizeInBytes = memoryStream.Length;
                        //guardar la info en la base de datos para luego listarlo
                        DatosAbierto oDatos = new DatosAbierto();
                        // oDenuncia.CodTipoDenuncia = int.Parse(DenunciaCLS2.Tipo_Denuncia);
                        oDatos.Bhabilitado = 1;
                        oDatos.IdTipoDato = 5;
                        oDatos.Ubicacion = pdfPathFolder;
                        oDatos.NombreArchivo = (string)fileName;
                        oDatos.Tamaño = (int)fileSizeInBytes;
                        oDatos.Extension = "pdf";
                        bd.Add(oDatos);
                        bd.SaveChanges();




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
        //Cantidad de datos abiertos generados..cantidadDatosAbiertosGenerados
        [HttpGet]
        [Route("api/DatosAbiertos/cantidadDatosAbiertosGenerados")]
        public ChartDatosAbiertos cantidadDatosAbiertosGenerados()
        {
            ChartDatosAbiertos oChartDatos = new ChartDatosAbiertos();

            try
            {

                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    oChartDatos.totalDatosAbiertos = (from datosAbiertos in bd.DatosAbiertos
                                                      where datosAbiertos.Bhabilitado == 1
                                                      select new DatosAbiertosCLS
                                                      {
                                                          idArchivo = (int)datosAbiertos.IdArchivo,
                                                      }).Count();


                    return oChartDatos;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return oChartDatos;
            }
        }
        }
}
