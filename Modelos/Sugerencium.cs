using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Sugerencium
    {
        public int IdSugerencia { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaGenerada { get; set; }
        public int? Bhabilitado { get; set; }
        public int? Estado { get; set; }
    }
}
