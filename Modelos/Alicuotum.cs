using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Alicuotum
    {
        public int IdAlicuota { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaGenerada { get; set; }
        public decimal? ImporteBase { get; set; }
    }
}
