using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class Area
{
    public int NroArea { get; set; }

    public string Nombre { get; set; } = null!;

    public string CodArea { get; set; } = null!;

    public int Bhabilitado { get; set; }

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();

    public virtual ICollection<TrabajoReclamo> TrabajoReclamos { get; set; } = new List<TrabajoReclamo>();
}
