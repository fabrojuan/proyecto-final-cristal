using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVPSA_V2022.clases
{
    public class BoletaCLS
    {

        public int idBoleta { get; set; }
        public DateTime? fechaGenerada { get; set; }
        public DateTime? fechaPago { get; set; }
        public int? estado { get; set; }
        public int? bhabilitado { get; set; }
        public string tipoMoneda { get; set; }
        public string url { get; set; }
        public decimal? importe { get; set; }
        public DateTime? fechaVencimiento { get; set; }

        public  Decimal total { get; set; }
        public string description { get; set; }

        public string reference { get; set; }

        public string currency { get; set; }

        public Boolean test { get; set; }

        public string return_url { get; set; }

        public string webhook { get; set; }
     


    }
}
