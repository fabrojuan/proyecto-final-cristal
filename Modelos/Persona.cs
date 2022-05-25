using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Persona
    {
        public Persona()
        {
            Lotes = new HashSet<Lote>();
            UsuarioVecinos = new HashSet<UsuarioVecino>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Dni { get; set; }
        public string? Cuil { get; set; }
        public string? Mail { get; set; }
        public string? Domicilio { get; set; }
        public string? Altura { get; set; }
        public DateTime? FechaNac { get; set; }
        public int? Bhabilitado { get; set; }
        public int? BtieneUser { get; set; }

        public virtual ICollection<Lote> Lotes { get; set; }
        public virtual ICollection<UsuarioVecino> UsuarioVecinos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
