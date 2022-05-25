using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Trabajo
    {
        public int NroTrabajo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public int NroDenuncia { get; set; }
        public int? IdUsuario { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
