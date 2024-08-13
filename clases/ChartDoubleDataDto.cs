using MVPSA_V2022.Modelos;
using Org.BouncyCastle.Bcpg;
using System;
using System.Globalization;

namespace MVPSA_V2022.clases
{
    public class ChartDoubleDataDto
    {
        public String[] labels { get; set; }
        public ChartDatasetDoubleDto[] datasets { get; set; }
    }

    public class ChartDatasetDoubleDto
    {
        public String label { get; set; }
        public Double[] data { get; set; }
        public String[] backgroundColor { get; set; }
        public String borderColor { get; set; }
    }

}









