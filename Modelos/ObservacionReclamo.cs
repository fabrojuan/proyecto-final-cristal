using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class ObservacionReclamo
{
    public int Id { get; set; }

    public int NroReclamo { get; set; }

    public string Observacion { get; set; } = null!;

    public int CodEstadoReclamo { get; set; }

    public int IdUsuarioAlta { get; set; }

    public DateTime FechaAlta { get; set; }

    public string CodAccion { get; set; } = null!;

    public virtual EstadoReclamo CodEstadoReclamoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;

    public virtual Reclamo NroReclamoNavigation { get; set; } = null!;

}
