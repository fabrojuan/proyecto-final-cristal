using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface IImpuestoService
    {

        public ResultadoEjecucionProcesoCLS generarImpuestos(SolicitudGeneracionImpuestosCLS solicitud);

    }
}
