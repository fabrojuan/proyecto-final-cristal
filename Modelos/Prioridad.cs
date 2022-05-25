using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Prioridad
    {
        public Prioridad()
        {
            Denuncia = new HashSet<Denuncium>();
        }

        public int NroPrioridad { get; set; }
        public string? NombrePrioridad { get; set; }
        public string? Descripcion { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<Denuncium> Denuncia { get; set; }
    }
}
