namespace MVPSA_V2022.clases
{
    public class ImpuestoPagoDto {
        public int idImpuesto { get; set; }
        public int mes { get; set; }
        public int anio { get; set; }
        public string periodo { get; set; }
        public string cuota { get; set; }
        public DateTime fechaPago { get; set; }
        public decimal importePagado { get; set; }
    }
}