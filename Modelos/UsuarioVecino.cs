using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class UsuarioVecino
{
    public int IdVecino { get; set; }

    public string? NombreUser { get; set; }

    public string? Contrasenia { get; set; }

    public int? IdPersona { get; set; }

    public int? Bhabilitado { get; set; }

    public DateTime? FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public virtual Persona? IdPersonaNavigation { get; set; }

    public virtual ICollection<PruebaGraficaReclamo> PruebaGraficaReclamos { get; set; } = new List<PruebaGraficaReclamo>();

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();

    public virtual ICollection<TrabajoReclamo> TrabajoReclamos { get; set; } = new List<TrabajoReclamo>();

    public virtual ICollection<TrabajoSolicitud> TrabajoSolicituds { get; set; } = new List<TrabajoSolicitud>();
}
