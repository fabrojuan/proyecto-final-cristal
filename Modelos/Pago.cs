using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Pago
    {
        public int IdPago { get; set; }
        public DateTime? FechaDePago { get; set; }
        public decimal? Importe { get; set; }
        public int? Bhabilitado { get; set; }
        public int? IdTipoPago { get; set; }
        public int? IdBoleta { get; set; }

        public virtual Tipopago? IdTipoPagoNavigation { get; set; }
    }
}
