namespace MVPSA_V2022.clases
{
    public class LoteCLS
    {
        public int IdLote { get; set; }
        public int? Manzana { get; set; }
        public int? NroLote { get; set; }
        public string? Calle { get; set; }
        public string? Altura { get; set; }
        public decimal? SupTerreno { get; set; }
        public decimal? SupEdificada { get; set; }
        public string? NomenclaturaCatastral { get; set; }
        public string? TipoInmueble { get; set; }
        public string? EstadoInmueble { get; set; }
        public decimal? BaseImponible { get; set; }
        public decimal? ValuacionTotal { get; set; }
        public int? EstadoDeuda { get; set; }
        public int? IdPersona { get; set; }

    }
}
