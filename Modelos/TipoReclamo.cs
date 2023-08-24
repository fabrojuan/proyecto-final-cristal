using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class TipoReclamo
{
    public int CodTipoReclamo { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Bhabilitado { get; set; }

    public int TiempoMaxTratamiento { get; set; }

    public DateTime? FechaAlta { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int IdUsuarioAlta { get; set; }

    public int IdUsuarioModificacion { get; set; }

    public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioModificacionNavigation { get; set; } = null!;

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
