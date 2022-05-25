using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Boletum
    {
        public Boletum()
        {
            Recibos = new HashSet<Recibo>();
        }

        public int IdBoleta { get; set; }
        public DateTime? FechaGenerada { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? Estado { get; set; }
        public int? Bhabilitado { get; set; }
        public string? TipoMoneda { get; set; }
        public string? Url { get; set; }
        public decimal? Importe { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        public virtual ICollection<Recibo> Recibos { get; set; }
    }
}
