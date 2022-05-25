using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class UsuarioVecino
    {
        public UsuarioVecino()
        {
            PruebaGraficaReclamos = new HashSet<PruebaGraficaReclamo>();
            Reclamos = new HashSet<Reclamo>();
            Solicituds = new HashSet<Solicitud>();
            TrabajoReclamos = new HashSet<TrabajoReclamo>();
            TrabajoSolicituds = new HashSet<TrabajoSolicitud>();
        }

        public int IdVecino { get; set; }
        public string? NombreUser { get; set; }
        public string? Contrasenia { get; set; }
        public int? IdPersona { get; set; }
        public int? Bhabilitado { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }

        public virtual Persona? IdPersonaNavigation { get; set; }
        public virtual ICollection<PruebaGraficaReclamo> PruebaGraficaReclamos { get; set; }
        public virtual ICollection<Reclamo> Reclamos { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
        public virtual ICollection<TrabajoReclamo> TrabajoReclamos { get; set; }
        public virtual ICollection<TrabajoSolicitud> TrabajoSolicituds { get; set; }
    }
}
