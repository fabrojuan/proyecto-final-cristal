using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Rol
    {
        public Rol()
        {
            Paginaxrols = new HashSet<Paginaxrol>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string? NombreRol { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<Paginaxrol> Paginaxrols { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
