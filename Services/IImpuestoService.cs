using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface IImpuestoService
    {

        public ResultadoEjecucionProcesoCLS generarImpuestos(SolicitudGeneracionImpuestosCLS solicitud);

        public ResultadoEjecucionProcesoCLS generarIntesesMensuales();

        public ResultadoEjecucionProcesoCLS confirmarBoletas();

        public IEnumerable<ImpuestoPagoDto> listarImpuestosPagados(int idLote);

    }
}
