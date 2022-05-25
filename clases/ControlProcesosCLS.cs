using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVPSA_V2022.clases
{
    public class ControlProcesosCLS
    {
        public int idEjecucion { get; set; }
        public int idProceso { get; set; }

        public DateTime fechaEjecucion { get; set; }
        public string Error { get; internal set; }
    }
}
