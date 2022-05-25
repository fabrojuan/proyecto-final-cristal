using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Usuario
    {
        public Usuario()
        {
            Denuncia = new HashSet<Denuncium>();
            PruebaGraficaDenuncia = new HashSet<PruebaGraficaDenuncium>();
            PruebaGraficaReclamos = new HashSet<PruebaGraficaReclamo>();
            Reclamos = new HashSet<Reclamo>();
            Sesiones = new HashSet<Sesione>();
            Solicituds = new HashSet<Solicitud>();
            TrabajoReclamos = new HashSet<TrabajoReclamo>();
            TrabajoSolicituds = new HashSet<TrabajoSolicitud>();
            Trabajos = new HashSet<Trabajo>();
        }

        public int IdUsuario { get; set; }
        public string? NombreUser { get; set; }
        public string? Contrasenia { get; set; }
        public int? IdPersona { get; set; }
        public int? Bhabilitado { get; set; }
        public int? IdTipoUsuario { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }

        public virtual Persona? IdPersonaNavigation { get; set; }
        public virtual Rol? IdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<Denuncium> Denuncia { get; set; }
        public virtual ICollection<PruebaGraficaDenuncium> PruebaGraficaDenuncia { get; set; }
        public virtual ICollection<PruebaGraficaReclamo> PruebaGraficaReclamos { get; set; }
        public virtual ICollection<Reclamo> Reclamos { get; set; }
        public virtual ICollection<Sesione> Sesiones { get; set; }
        public virtual ICollection<Solicitud> Solicituds { get; set; }
        public virtual ICollection<TrabajoReclamo> TrabajoReclamos { get; set; }
        public virtual ICollection<TrabajoSolicitud> TrabajoSolicituds { get; set; }
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}
