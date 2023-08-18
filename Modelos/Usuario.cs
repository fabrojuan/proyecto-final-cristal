using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUser { get; set; }

    public string? Contrasenia { get; set; }

    public int? IdPersona { get; set; }

    public int? Bhabilitado { get; set; }

    public int? IdTipoUsuario { get; set; }

    public DateTime? FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public virtual ICollection<Denuncium> Denuncia { get; set; } = new List<Denuncium>();

    public virtual Persona? IdPersonaNavigation { get; set; }

    public virtual Rol? IdTipoUsuarioNavigation { get; set; }

    public virtual ICollection<PruebaGraficaDenuncium> PruebaGraficaDenuncia { get; set; } = new List<PruebaGraficaDenuncium>();

    public virtual ICollection<PruebaGraficaReclamo> PruebaGraficaReclamos { get; set; } = new List<PruebaGraficaReclamo>();

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();

    public virtual ICollection<Sesione> Sesiones { get; set; } = new List<Sesione>();

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();

    public virtual ICollection<TipoReclamo> TipoReclamoIdUsuarioAltaNavigations { get; set; } = new List<TipoReclamo>();

    public virtual ICollection<TipoReclamo> TipoReclamoIdUsuarioModificacionNavigations { get; set; } = new List<TipoReclamo>();

    public virtual ICollection<TrabajoReclamo> TrabajoReclamos { get; set; } = new List<TrabajoReclamo>();

    public virtual ICollection<TrabajoSolicitud> TrabajoSolicituds { get; set; } = new List<TrabajoSolicitud>();

    public virtual ICollection<Trabajo> Trabajos { get; set; } = new List<Trabajo>();
}
