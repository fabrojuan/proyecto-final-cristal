namespace MVPSA_V2022.clases
{
    public class TipoDenunciaCLS
    {
        public int cod_Tipo_Denuncia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Tiempo_Max_Tratamiento { get; set; }
        public DateTime? fechaAlta { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public string? usuarioAlta { get; set; }
        public string? usuarioModificacion { get; set; }


    }
}
