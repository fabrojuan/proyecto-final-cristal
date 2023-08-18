using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? NombreRol { get; set; }

    public int? Bhabilitado { get; set; }

    public string TipoRol { get; set; } = null!;

    public string CodRol { get; set; } = null!;

    public virtual ICollection<Paginaxrol> Paginaxrols { get; set; } = new List<Paginaxrol>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
