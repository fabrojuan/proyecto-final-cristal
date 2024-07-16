using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class EstadoSugerencia
{
   
    public int CodEstadoSugerencia { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Bhabilitado { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Sugerencium> Sugerencia { get; set; } = new List<Sugerencium>();
}
