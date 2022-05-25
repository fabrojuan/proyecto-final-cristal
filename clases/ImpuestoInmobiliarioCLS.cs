using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVPSA_V2022.clases
{
    public class ImpuestoInmobiliarioCLS
    {   public int idImpuesto { get; set; }
        public int mes { get; set; }
        public int anio { get; set; }
        public DateTime fechaEmision { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int estado { get; set; }
        public decimal importeBase { get; set; }
        public decimal interesValor { get; set; }
        public decimal importeFinal { get; set; }
        public int bhabilitado { get; set; }
        public int idLote { get; set; }
        
    }
}
