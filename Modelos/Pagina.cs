using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Pagina
    {
        public Pagina()
        {
            Paginaxrols = new HashSet<Paginaxrol>();
        }

        public int IdPagina { get; set; }
        public string? Mensaje { get; set; }
        public string? Accion { get; set; }
        public int? Bhabilitado { get; set; }
        public int? Bvisible { get; set; }

        public virtual ICollection<Paginaxrol> Paginaxrols { get; set; }
    }
}
