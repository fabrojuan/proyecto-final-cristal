using System;

namespace MVPSA_V2022.clases
{
    public class DenunciaCLS2
    {
        public int Nro_Denuncia { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string Tipo_Denuncia { get; set; }

        public int CodTipoDenuncia { get; set; }

        public string? Estado_Denuncia { get; set; }
        public string? Nombre_Infractor { get; set; }
        public string? Apellido_Infractor { get; set; }
        public int Bhabilitado { get; set; }
        public string? Mail_Notificacion { get; set; }
        public string? Prioridad { get; set; }
        public string? Movil_Notificacion { get; set; }
        public int IdUsuario { get; set; }
        public string? NombreUser { get; set; }

        public string? Entre_Calles { get; set; }
        public string? Altura { get; set; }
        public string? Calle { get; set; }
        public int Nro_Prioridad { get; set; }
        public string? PruebaGrafica { get; set; }
        public string? foto { get; set; }
        public string? foto2 { get; set; }
        public string? foto3 { get; set; }
        public string? NombreEmpleado { get; set; }


    }
}




