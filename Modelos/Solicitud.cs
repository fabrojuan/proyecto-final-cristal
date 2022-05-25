using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Solicitud
    {
        public Solicitud()
        {
            TrabajoSolicituds = new HashSet<TrabajoSolicitud>();
        }

        public int NroSolicitud { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Descripcion { get; set; }
        public int? CodTipoSolicitud { get; set; }
        public int? CodEstadoSolicitud { get; set; }
        public int? Bhabilitado { get; set; }
        public int? IdVecino { get; set; }
        public int? IdUsuario { get; set; }

        public virtual EstadoSolicitud? CodEstadoSolicitudNavigation { get; set; }
        public virtual TipoSolicitud? CodTipoSolicitudNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual UsuarioVecino? IdVecinoNavigation { get; set; }
        public virtual ICollection<TrabajoSolicitud> TrabajoSolicituds { get; set; }
    }
}
