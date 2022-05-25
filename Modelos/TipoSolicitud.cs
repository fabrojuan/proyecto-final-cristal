using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class TipoSolicitud
    {
        public TipoSolicitud()
        {
            Solicituds = new HashSet<Solicitud>();
        }

        public int CodTipoSolicitud { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? Bhabilitado { get; set; }
        public int? TiempoMaxTratamiento { get; set; }

        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}
