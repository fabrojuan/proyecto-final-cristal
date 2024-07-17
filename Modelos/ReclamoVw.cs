using System.ComponentModel.DataAnnotations.Schema;

namespace MVPSA_V2022.Modelos
{
    public class ReclamoVw
    {
        [Column("Nro_Reclamo")]
        public int NroReclamo { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        [Column("Cod_Tipo_Reclamo")]
        public int CodTipoReclamo { get; set; }
        [Column("Cod_Estado_Reclamo")]
        public int CodEstadoReclamo { get; set; }
        public int Bhabilitado { get; set; }
        public string? Calle { get; set; }
        public string? Altura { get; set; }
        public string? EntreCalles { get; set; }
        public int? IdVecino { get; set; }
        public int? IdUsuario { get; set; }
        public string? NomApeVecino { get; set; }
        [Column("Nro_Prioridad")]
        public int NroPrioridad { get; set; }

        [Column("tipo_reclamo")]
        public string TipoReclamo { get; set; }

        [Column("estado_reclamo")]
        public string EstadoReclamo { get; set; }

        [Column("Prioridad_reclamo")]
        public string PrioridadReclamo { get; set; }

        [Column("usuario")]
        public string? Usuario { get; set; }

        [Column("empleado")]
        public string Empleado { get; set; }
    }
}
