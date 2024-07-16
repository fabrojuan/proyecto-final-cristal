using MVPSA_V2022.Modelos;
using Org.BouncyCastle.Bcpg;
using System;
using System.Globalization;

namespace MVPSA_V2022.clases
{
    public class ChartDataDto
    {
        public String[] labels { get; set; }
        public ChartDatasetDto[] datasets { get; set; }
    }

    public class ChartDatasetDto
    {
        public String label { get; set; }
        public int[] data { get; set; }
        public String[] backgroundColor { get; set; }
        public String borderColor { get; set; }
    }

}









