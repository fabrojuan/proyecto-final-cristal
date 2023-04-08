namespace MVPSA_V2022.clases
{
    public class PrioridadReclamoDto
    {
        public int NroPrioridad { get; set; }
        public string? NombrePrioridad { get; set; }
        public int Bhabilitado { get; set; }
        public string? Descripcion { get; set; }

        public int TiempoMaxTratamiento { get; set; }
    }
}
