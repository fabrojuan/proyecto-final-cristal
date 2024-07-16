﻿using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class PrioridadReclamo
{
    public int NroPrioridad { get; set; }

    public string? NombrePrioridad { get; set; }

    public int? Bhabilitado { get; set; }

    public string? Descripcion { get; set; }

    public int TiempoMaxTratamiento { get; set; }

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
