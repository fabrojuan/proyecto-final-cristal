using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Sesione
    {
        public string IdSesion { get; set; } = null!;
        public string? Maquina { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
