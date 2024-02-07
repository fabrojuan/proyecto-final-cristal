using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUser { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public int? IdPersona { get; set; }

    public int Bhabilitado { get; set; }

    public int? IdTipoUsuario { get; set; }

    public DateTime FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public virtual ICollection<Denuncium> Denuncia { get; set; } = new List<Denuncium>();

    public virtual Persona? IdPersonaNavigation { get; set; }

    public virtual Rol? IdTipoUsuarioNavigation { get; set; }

    public virtual ICollection<ObservacionReclamo> ObservacionReclamos { get; set; } = new List<ObservacionReclamo>();

    public virtual ICollection<PruebaGraficaDenuncium> PruebaGraficaDenuncia { get; set; } = new List<PruebaGraficaDenuncium>();

    public virtual ICollection<PruebaGraficaReclamo> PruebaGraficaReclamos { get; set; } = new List<PruebaGraficaReclamo>();

    public virtual ICollection<Reclamo> ReclamoIdUsuarioNavigations { get; set; } = new List<Reclamo>();

    public virtual ICollection<Reclamo> ReclamoIdUsuarioResponsableNavigations { get; set; } = new List<Reclamo>();

    public virtual ICollection<Reclamo> ReclamoIdVecinoNavigations { get; set; } = new List<Reclamo>();

    public virtual ICollection<Solicitud> SolicitudIdUsuarioNavigations { get; set; } = new List<Solicitud>();

    public virtual ICollection<Solicitud> SolicitudIdVecinoNavigations { get; set; } = new List<Solicitud>();

    public virtual ICollection<TipoReclamo> TipoReclamoIdUsuarioAltaNavigations { get; set; } = new List<TipoReclamo>();

    public virtual ICollection<TipoReclamo> TipoReclamoIdUsuarioModificacionNavigations { get; set; } = new List<TipoReclamo>();

    public virtual ICollection<TrabajoReclamo> TrabajoReclamos { get; set; } = new List<TrabajoReclamo>();

    public virtual ICollection<TrabajoSolicitud> TrabajoSolicitudIdUsuarioNavigations { get; set; } = new List<TrabajoSolicitud>();

    public virtual ICollection<TrabajoSolicitud> TrabajoSolicitudIdVecinoNavigations { get; set; } = new List<TrabajoSolicitud>();

    public virtual ICollection<Trabajo> Trabajos { get; set; } = new List<Trabajo>();
}
