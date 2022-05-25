using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Paginaxrol
    {
        public int IdPaginaxRol { get; set; }
        public int? IdPagina { get; set; }
        public int? IdRol { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual Pagina? IdPaginaNavigation { get; set; }
        public virtual Rol? IdRolNavigation { get; set; }
    }
}
