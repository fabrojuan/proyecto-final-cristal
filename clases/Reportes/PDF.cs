using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using iText.IO.Image;
using iText.StyledXmlParser.Jsoup.Nodes;
using Document = iText.Layout.Document;
using iText.Layout.Borders;

namespace MVPSA_V2022.clases.Reportes
{
    public class PDF
    {

        public static void crearParrafo(Document doc, string texto, int sizefuente = 14,
        string alineacion = "left")
        {
            //Image oimagen = new Image(ImageDataFactory.Create("C:\\Users\\roman\\source\\repos\\romansad\\Proyecto_Cristal_2022\\ClientApp\\src\\assets\\Imagenes\\logo-cristal_3.png"));
           // oimagen.SetWidth(UnitValue.CreatePointValue(50));
            //oimagen.SetHeight(UnitValue.CreatePointValue(50));
           // oimagen.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
           Paragraph c1 = new Paragraph(texto);
            c1.SetFontSize(sizefuente);
            c1.SetBorder(new SolidBorder(1));
            Color colorNegro = new DeviceRgb(7, 0, 2);
            c1.SetFontColor(colorNegro);
          //  c1.Add(oimagen.SetHorizontalAlignment(HorizontalAlignment.RIGHT));//, oimagen.SetWidth(UnitValue.CreatePercentValue(100))) ; 
            alineacionTexto(c1, alineacion);
            doc.Add(c1);
        }

        public static void alineacionTexto(Paragraph c1, string alineacion)
        {
            switch (alineacion)
            {
                case "center": c1.SetTextAlignment(TextAlignment.CENTER); break;
                case "left": c1.SetTextAlignment(TextAlignment.LEFT); break;
                case "right": c1.SetTextAlignment(TextAlignment.RIGHT); break;
                default: c1.SetTextAlignment(TextAlignment.LEFT); break;
            }
        }

        public static void crearTabla<T>(Document doc, List<string> cabeceras,List<float>anchos, List<LoteImpuestoCLS> listaImpuestos, List<string> campos)
        {
            Table oTable;
            if (anchos == null)
                oTable = new Table(cabeceras.Count);
            else
                oTable = new Table(UnitValue.CreatePercentArray(anchos.ToArray()));
            oTable.SetWidth(UnitValue.CreatePercentValue(100));
            Cell oCell;
            foreach (string cabecera in cabeceras)
            {
                oCell = new Cell();
                oCell.SetBackgroundColor(ColorConstants.BLUE);
                oCell.SetFontColor(ColorConstants.WHITE);
                oCell.SetTextAlignment(TextAlignment.CENTER);
                oCell.Add(new Paragraph(cabecera));
                oTable.AddCell(oCell);
            }
        //Aca va el de crear tabla la llamada al metodo.
           for (int j = 0; j < listaImpuestos.Count; j++)
                {
                //Defono el color para las celdas
                Color colorNegro = new DeviceRgb(7, 0, 2);
                //La columna Manzana
                oCell = new Cell();
                    oCell.Add(new Paragraph(listaImpuestos[j].manzana.ToString()));
                    oCell.SetTextAlignment(TextAlignment.CENTER);
                     oCell.SetFontColor(colorNegro);
                    oTable.AddCell(oCell);
                    //La columna NroLote
                    oCell = new Cell();
                    oCell.Add(new Paragraph(listaImpuestos[j].nroLote.ToString()));
                    oCell.SetTextAlignment(TextAlignment.CENTER);
                oCell.SetFontColor(colorNegro);
                oTable.AddCell(oCell);
                    //La columna Mes
                    oCell = new Cell();
                    oCell.Add(new Paragraph(listaImpuestos[j].mes.ToString()));
                    oCell.SetTextAlignment(TextAlignment.CENTER);
                oCell.SetFontColor(colorNegro);
                oTable.AddCell(oCell);
                }
                doc.Add(oTable);





        }

    }



}

