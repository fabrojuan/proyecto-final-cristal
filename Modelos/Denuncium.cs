using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class Denuncium
    {
        public int NroDenuncia { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Descripcion { get; set; }
        public int? CodTipoDenuncia { get; set; }
        public int? CodEstadoDenuncia { get; set; }
        public string? NombreInfractor { get; set; }
        public string? ApellidoInfractor { get; set; }
        public int? IdUsuario { get; set; }
        public int? Bhabilitado { get; set; }
        public string? MailNotificacion { get; set; }
        public string? MovilNotificacion { get; set; }
        public string? EntreCalles { get; set; }
        public string? Altura { get; set; }
        public string? Calle { get; set; }
        public int? NroPrioridad { get; set; }

        public virtual EstadoDenuncium? CodEstadoDenunciaNavigation { get; set; }
        public virtual TipoDenuncium? CodTipoDenunciaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual Prioridad? NroPrioridadNavigation { get; set; }
    }
}
