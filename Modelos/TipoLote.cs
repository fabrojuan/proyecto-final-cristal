using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class TipoLote
{
    public int CodTipoLote { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();
}
