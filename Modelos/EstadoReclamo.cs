using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{

    public enum EstadoReclamoEnum : uint
    {
        NUEVO = 1,
        ASIGNADO = 2,
        EN_TRATAMIENTO = 3,
        CANCELADO = 4,
        SOLUCIONADO = 5,
        SUSPENDIDO = 6
    }

    public partial class EstadoReclamo
    {
        public EstadoReclamo()
        {
            Reclamos = new HashSet<Reclamo>();
        }

        public int CodEstadoReclamo { get; set; }
        public string? Nombre { get; set; }
        public int? Bhabilitado { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Reclamo> Reclamos { get; set; }
    }
}
