using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Reclamo
    {
        public Reclamo()
        {
            PruebaGraficaReclamos = new HashSet<PruebaGraficaReclamo>();
            TrabajoReclamos = new HashSet<TrabajoReclamo>();
        }

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

        public virtual EstadoReclamo? CodEstadoReclamoNavigation { get; set; }
        public virtual TipoReclamo? CodTipoReclamoNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual UsuarioVecino? IdVecinoNavigation { get; set; }
        public virtual ICollection<PruebaGraficaReclamo> PruebaGraficaReclamos { get; set; }
        public virtual ICollection<TrabajoReclamo> TrabajoReclamos { get; set; }
    }
}
