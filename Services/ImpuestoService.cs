using Microsoft.EntityFrameworkCore;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class ImpuestoService : IImpuestoService
    {
        public ResultadoEjecucionProcesoCLS generarImpuestos(
            SolicitudGeneracionImpuestosCLS solicitud)
        {

            if (estaProcesoEjecutadoParaAnio(2, solicitud.anio)) {
                return new ResultadoEjecucionProcesoCLS(
                    "ERROR", "La generacion de Impuestos ya se ha realizado este año");
            }

            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    bd.Database.ExecuteSqlRaw("GENERACION_IMPUESTOS_LOTES");
                    bd.SaveChanges();
                    return new ResultadoEjecucionProcesoCLS("OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar los impuestos para el año " + solicitud.anio 
                    + ". Error: " + ex.Message);
                return new ResultadoEjecucionProcesoCLS(
                    "ERROR", "No se pudieron generar los impuestos para el año " + solicitud.anio);
            }

        }

        private Boolean estaProcesoEjecutadoParaAnio(int idProceso, int anio) {

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                return bd.ControlProcesos.Where(cp => cp.IdProceso == idProceso
                && cp.FechaEjecucion.Year == anio).Count() > 0;
            }

        }
    }
}
