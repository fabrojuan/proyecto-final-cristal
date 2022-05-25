using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class DatosAbierto
    {
        public int IdArchivo { get; set; }
        public string? NombreArchivo { get; set; }
        public string? Extension { get; set; }
        public double? Tamaño { get; set; }
        public string? Ubicacion { get; set; }
        public int? Bhabilitado { get; set; }
        public int? IdTipoDato { get; set; }

        public virtual TipoDatoAbierto? IdTipoDatoNavigation { get; set; }
    }
}
