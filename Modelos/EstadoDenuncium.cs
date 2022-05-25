using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class EstadoDenuncium
    {
        public EstadoDenuncium()
        {
            Denuncia = new HashSet<Denuncium>();
        }

        public int CodEstadoDenuncia { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<Denuncium> Denuncia { get; set; }
    }
}
