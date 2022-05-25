using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Impuestoinmobiliario
    {
        public int IdImpuesto { get; set; }
        public int? Mes { get; set; }
        public int? Año { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int? Estado { get; set; }
        public decimal? ImporteBase { get; set; }
        public decimal? InteresValor { get; set; }
        public decimal? ImporteFinal { get; set; }
        public int? Bhabilitado { get; set; }
        public int IdLote { get; set; }

        public virtual Lote IdLoteNavigation { get; set; } = null!;
    }
}
