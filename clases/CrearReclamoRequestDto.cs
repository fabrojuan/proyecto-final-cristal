namespace MVPSA_V2022.clases
{
    public class CrearReclamoRequestDto
    {
        public string descripcion { get; set; }
        public int codTipoReclamo { get; set; }
        public string calle { get; set; }
        public string altura { get; set; }
        public string entreCalles { get; set; }
        public string? nombreYapellido { get; set; }
        public string foto1 { get; set; }
        public string foto2 { get; set; }
    }
}
