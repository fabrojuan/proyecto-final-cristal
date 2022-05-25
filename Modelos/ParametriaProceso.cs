using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class ParametriaProceso
    {
        public ParametriaProceso()
        {
            ControlProcesos = new HashSet<ControlProceso>();
        }

        public int IdProceso { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Periodicidad { get; set; } = null!;

        public virtual ICollection<ControlProceso> ControlProcesos { get; set; }
    }
}
