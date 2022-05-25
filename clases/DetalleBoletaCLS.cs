using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVPSA_V2022.clases
{
    public class DetalleBoletaCLS
    {
        public int IdDetalleBoleta { get; set; }
        public int IdBoleta { get; set; }
        public int IdImpuesto { get; set; }
        public int? Bhabilitado { get; set; }
        public decimal Importe { get; set; }
        public int Estado { get; set; }

        public string Valores { get; set; }

        public int IdBoletaNavigation { get; set; }
        public int IdImpuestoNavigation  { get; set; }
    }
}
