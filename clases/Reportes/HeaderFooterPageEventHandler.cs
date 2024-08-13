using iText.IO.Font.Constants;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.IO.Image;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Layout;
using iText.Kernel.Colors;
using MVPSA_V2022.clases.Reportes;
using System;
using iText.Layout.Borders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MVPSA_V2022.clases.Reportes
{
    public class HeaderFooterEventHandler  : IEventHandler
    {
        public void HandleEvent(Event @event)
        { 
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            //string logoPath = "C:\\Proyecto_JUAN\\proyecto-final-cristal\\ClientApp\\src\\assets\\Imagenes\\Arco100px.png";
            string logoPath = "." + System.IO.Path.DirectorySeparatorChar + "datos_abiertos" + System.IO.Path.DirectorySeparatorChar + "Arco100px.png";
        var logo = ImageDataFactory.Create(logoPath);
            PdfPage page = docEvent.GetPage();
            Rectangle pageSize = page.GetPageSize();
            Color colorGris = new DeviceRgb(192, 192, 192);
            PdfDocument pdf = docEvent.GetDocument();
            PdfCanvas pdfCanvas = new(page.GetLastContentStream(), page.GetResources(), pdf);
         pdfCanvas.AddImageAt(logo, pageSize.GetWidth() - logo.GetWidth() - 20, pageSize.GetHeight() - logo.GetHeight() +10, true);
          _ = new Canvas(pdfCanvas, pageSize);
            // Encabezado
            //definimos el andho de las dos columnas
            List<float> anchosColumna = new List<float> { 35, 65 };
            iText.Layout.Element.Table otable;
             otable = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(anchosColumna.ToArray())); 
            otable.SetWidth(UnitValue.CreatePercentValue(100)); //hacemos que la tabla ocupe todo el ancho
            otable.SetMarginBottom(10); //margin bottom
           
            Cell ocellText = new Cell();
            Paragraph oParagraph = new Paragraph("Municipalidad de Villa Parque Santa Ana");
            oParagraph.SetFontSize(12);
            ocellText.SetBorder(Border.NO_BORDER);
            ocellText.Add(oParagraph);
            //En estas lineas se adjunta la imagen del encabezado 
           // Cell ocellImagen = new Cell();
            //Image oimagen = new Image(ImageDataFactory.Create("C:\\Users\\roman\\source\\repos\\romansad\\Proyecto_Cristal_2022\\ClientApp\\src\\assets\\Imagenes\\Arcoiris.png"));
            //oimagen.SetWidth(UnitValue.CreatePointValue(50));
            //oimagen.SetHeight(UnitValue.CreatePointValue(50));
            //ocellImagen.SetBorder(Border.NO_BORDER);
            //ocellImagen.Add(oimagen);
            ///otable.AddCell(ocellImagen);
            otable.AddCell(ocellText);
          
            pdfCanvas.BeginText()

           .SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN), 12)
                .MoveText(pageSize.GetLeft() +40, pageSize.GetTop() - 30)
                .SetColor(colorGris, true)
              
               .ShowText("Municipalidad de Villa Parque Santa Ana")
                .EndText();
            // Pie de página
            pdfCanvas.BeginText()
                .SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetLeft() + 70, pageSize.GetBottom() + 20)
                .ShowText("Cristal Datos Abiertos")
                .EndText();
           pdfCanvas.Release();
        }
    }
}





