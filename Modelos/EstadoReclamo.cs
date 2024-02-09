using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVPSA_V2022.Modelos;

public partial class EstadoReclamo
{
    public int CodEstadoReclamo { get; set; }

    public string? Nombre { get; set; }

    public int? Bhabilitado { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<ObservacionReclamo> ObservacionReclamos { get; set; } = new List<ObservacionReclamo>();

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
