using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Recibo
    {
        public int IdRecibo { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? Bhabilitado { get; set; }
        public int? IdBoleta { get; set; }
        public decimal? Importe { get; set; }
        public string? Mail { get; set; }

        public virtual Boletum? IdBoletaNavigation { get; set; }
    }
}
