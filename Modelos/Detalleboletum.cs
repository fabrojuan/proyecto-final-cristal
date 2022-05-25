using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Detalleboletum
    {
        public int IdDetalleBoleta { get; set; }
        public int IdBoleta { get; set; }
        public int IdImpuesto { get; set; }
        public int? Bhabilitado { get; set; }
        public decimal? Importe { get; set; }
        public int? Estado { get; set; }
    }
}
