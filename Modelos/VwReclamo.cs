using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class VwReclamo
    {
        public int NroReclamo { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Descripcion { get; set; }
        public int? CodTipoReclamo { get; set; }
        public int? CodEstadoReclamo { get; set; }
        public int? Bhabilitado { get; set; }
        public string? Calle { get; set; }
        public string? Altura { get; set; }
        public string? EntreCalles { get; set; }
        public int? IdVecino { get; set; }
        public int? IdUsuario { get; set; }
        public int? NroPrioridad { get; set; }
        public string? TipoReclamo { get; set; }
        public string? EstadoReclamo { get; set; }
        public string? PrioridadReclamo { get; set; }
        public string? Usuario { get; set; }
        public string Empleado { get; set; } = null!;
    }
}
