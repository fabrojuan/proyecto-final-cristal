namespace MVPSA_V2022.clases
{
    public class TipoReclamoCLS
    {
        public int cod_Tipo_Reclamo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int tiempo_Max_Tratamiento { get; set; }
        public DateTime? fechaAlta { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public string? usuarioAlta { get; set; }
        public string? usuarioModificacion { get; set; }
    }
}
