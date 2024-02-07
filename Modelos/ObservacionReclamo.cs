using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class ObservacionReclamo
{
    public int Id { get; set; }

    public int NroReclamo { get; set; }

    public string Observacion { get; set; } = null!;

    public int CodEstadoReclamoOrigen { get; set; }

    public int CodEstadoReclamoDestino { get; set; }

    public int IdUsuarioAlta { get; set; }

    public DateTime FechaAlta { get; set; }

    public virtual EstadoReclamo CodEstadoReclamoDestinoNavigation { get; set; } = null!;

    public virtual EstadoReclamo CodEstadoReclamoOrigenNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioAltaNavigation { get; set; } = null!;

    public virtual Reclamo NroReclamoNavigation { get; set; } = null!;
}
