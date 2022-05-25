using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class TipoDatoAbierto
    {
        public TipoDatoAbierto()
        {
            DatosAbiertos = new HashSet<DatosAbierto>();
        }

        public int IdTipoDato { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<DatosAbierto> DatosAbiertos { get; set; }
    }
}
