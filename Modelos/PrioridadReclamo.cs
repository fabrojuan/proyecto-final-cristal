using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class PrioridadReclamo
    {
        public int NroPrioridad { get; set; }
        public string? NombrePrioridad { get; set; }
        public int? Bhabilitado { get; set; }
        public string? Descripcion { get; set; }
    }
}
