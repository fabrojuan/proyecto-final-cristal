using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;


namespace ReportesMVC.Reportes
{
	public class Excel
	{

		public static void tituloHorizontal(ExcelWorksheet ew1,String titulo="Reporte",int postFila=1,
			int posInicioColumna=1,int postFinColumna=4,int tamanioTexto=18,int fuenteTexto = 20, Color? fondo=null,Color? colortexto=null)
		{
			using (ExcelRange rango = ew1.Cells[postFila, posInicioColumna, postFila, postFinColumna])
			{
                rango.Merge = true;
                rango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rango.Style.Font.Size = fuenteTexto;
                rango.Style.Fill.PatternType = ExcelFillStyle.Solid;
                if (fondo == null)
                    rango.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                else
                    rango.Style.Fill.BackgroundColor.SetColor((Color)fondo);
                if (colortexto == null)
                    rango.Style.Font.Color.SetColor(Color.White);
                else
                    rango.Style.Font.Color.SetColor((Color)colortexto);
            }
            ew1.Cells[postFila, posInicioColumna].Value = titulo;
		}

        public static void anchosColumnas(ExcelWorksheet ew1, int postInicioColumna = 1, int postFinColumna = 4,
              List<int>? anchos = null)
        {
            int indiceAncho = 0;
            for (int i = postInicioColumna; i <= postFinColumna; i++)
            {
                ew1.Column(i).Width = anchos[indiceAncho];
                indiceAncho++;
            }


        }
        public static void cabecerasTabla(ExcelWorksheet ew1, int posFila, int postInicioColumna,
             List<string> cabeceras, bool bordes = true, bool centrar = true, bool negrita = true)
        {
            int postFinColumna = postInicioColumna + cabeceras.Count - 1;
            int indicecabecera = 0;
            for (int i = postInicioColumna; i <= postFinColumna; i++)
            {
                ew1.Cells[posFila, i].Value = cabeceras[indicecabecera];
                if (negrita == true)
                    ew1.Cells[posFila, i].Style.Font.Bold = true;
                if (bordes == true)
                    Excel.border(ew1, posFila, i);
                if (centrar == true)
                    ew1.Cells[posFila, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                indicecabecera++;
            }
        }
        public static void border(ExcelWorksheet ew1, int fila, int columna)
        {
            ew1.Cells[fila, columna].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ew1.Cells[fila, columna].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ew1.Cells[fila, columna].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ew1.Cells[fila, columna].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }
        public static void contenido(ExcelWorksheet ew1, int postInicioColumna, int posFila,
             int postFinColumna , List<LoteImpuestoCLS> listaImpuestos, bool bordes = true, bool centrar = true, bool negrita = false)
        {
            postFinColumna=postFinColumna + 1;

            foreach (LoteImpuestoCLS li in listaImpuestos)
            {
                for (int i = postInicioColumna; i < postFinColumna; i++) { 
                //ew1.Cells[posFila,i].Value = listaImpuestos[posFila];
                if (centrar == true)
                    ew1.Cells[posFila, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                posFila++;
                
            }
        }

    }

}
