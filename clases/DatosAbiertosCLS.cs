using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVPSA_V2022.clases
{
    public class DatosAbiertosCLS
    {
        public int idArchivo { get; set; }
        public string nombreArchivo { get; set; }
        public string extension { get; set; }
        public double? tamaño { get; set; }
        public string ubicacion { get; set; }
        public int? Bhabilitado { get; set; }
        public int? idTipoDato { get; set; }
    }
}
