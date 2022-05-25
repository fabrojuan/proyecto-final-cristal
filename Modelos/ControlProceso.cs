using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class ControlProceso
    {
        public int IdEjecucion { get; set; }
        public int IdProceso { get; set; }
        public DateTime? FechaEjecucion { get; set; }

        public virtual ParametriaProceso IdProcesoNavigation { get; set; } = null!;
    }
}
