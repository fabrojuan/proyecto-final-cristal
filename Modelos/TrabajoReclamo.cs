using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class TrabajoReclamo
{
    public int NroTrabajo { get; set; }

    public int NroReclamo { get; set; }

    public int NroAreaTrabajo { get; set; }

    public DateTime FechaTrabajo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdUsuarioAlta { get; set; }

    public DateTime FechaAlta { get; set; }

    public int Bhabilitado { get; set; }

    public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;

    public virtual Area NroAreaTrabajoNavigation { get; set; } = null!;

    public virtual Reclamo NroReclamoNavigation { get; set; } = null!;
}
