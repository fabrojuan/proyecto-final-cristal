using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class PruebaGraficaReclamo
    {
        public int NroImagen { get; set; }
        public byte[]? Imagen { get; set; }
        public int? NroReclamo { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdVecino { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual UsuarioVecino? IdVecinoNavigation { get; set; }
        public virtual Reclamo? NroReclamoNavigation { get; set; }
    }
}
