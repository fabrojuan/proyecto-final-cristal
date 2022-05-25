using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class EstadoSolicitud
    {
        public EstadoSolicitud()
        {
            Solicituds = new HashSet<Solicitud>();
        }

        public int CodEstadoSolicitud { get; set; }
        public string? Nombre { get; set; }
        public int? Bhabilitado { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}
