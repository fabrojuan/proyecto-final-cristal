using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class TipoReclamo
    {
        public TipoReclamo()
        {
            Reclamos = new HashSet<Reclamo>();
        }

        public int CodTipoReclamo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? Bhabilitado { get; set; }
        public int? TiempoMaxTratamiento { get; set; }

        public virtual ICollection<Reclamo> Reclamos { get; set; }
    }
}
