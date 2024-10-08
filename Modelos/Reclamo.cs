﻿using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class Reclamo
{
    public int NroReclamo { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Descripcion { get; set; }

    public int? CodTipoReclamo { get; set; }

    public int? CodEstadoReclamo { get; set; }

    public int? Bhabilitado { get; set; }

    public string? Calle { get; set; }

    public string? Altura { get; set; }

    public string? EntreCalles { get; set; }

    public int? IdVecino { get; set; }

    public int IdUsuario { get; set; }

    public string? NomApeVecino { get; set; }

    public int? NroPrioridad { get; set; }

    public string? MailVecino { get; set; }

    public string? TelefonoVecino { get; set; }

    public int? NroArea { get; set; }

    public int? IdUsuarioResponsable { get; set; }

    public int? IdSugerenciaOrigen { get; set; }

    public string Interno { get; set; } = null!;

    public DateTime? FechaCierre { get; set; }

    public virtual EstadoReclamo? CodEstadoReclamoNavigation { get; set; }

    public virtual TipoReclamo? CodTipoReclamoNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioResponsableNavigation { get; set; }

    public virtual Usuario? IdVecinoNavigation { get; set; }

    public virtual Area? NroAreaNavigation { get; set; }

    public virtual PrioridadReclamo? NroPrioridadNavigation { get; set; }

    public virtual ICollection<ObservacionReclamo> ObservacionReclamos { get; set; } = new List<ObservacionReclamo>();

    public virtual ICollection<PruebaGraficaReclamo> PruebaGraficaReclamos { get; set; } = new List<PruebaGraficaReclamo>();

    public virtual ICollection<TrabajoReclamo> TrabajoReclamos { get; set; } = new List<TrabajoReclamo>();
}
