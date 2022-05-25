using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class PruebaGraficaDenuncium
    {
        public int NroImagen { get; set; }
        public byte[]? Imagen { get; set; }
        public int? NroDenuncia { get; set; }
        public int? IdUsuario { get; set; }
        public int? Bhabilitado { get; set; }
        public string? Foto { get; set; }
        public int? NroTrabajo { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
