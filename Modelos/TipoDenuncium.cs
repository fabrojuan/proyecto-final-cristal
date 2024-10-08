﻿using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class TipoDenuncium
{
    public int CodTipoDenuncia { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? TiempoMaxTratamiento { get; set; }

    public int? Bhabilitado { get; set; }

    public int? IdUsuarioAlta { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaAlta { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Denuncium> Denuncia { get; set; } = new List<Denuncium>();
}
