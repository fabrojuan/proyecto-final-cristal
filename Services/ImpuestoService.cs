using Microsoft.Data.SqlClient;
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

            return generarImpuestoAnual(solicitud);

        }

        public ResultadoEjecucionProcesoCLS generarIntesesMensuales() {

            int anio = DateTime.Now.Year;
            int mes = DateTime.Now.Month;

            if (estaProcesoEjecutadoParaAnioYMes(2, anio, mes))
            {
                return new ResultadoEjecucionProcesoCLS(
                    "ERROR", "El interés para este mes ya ha sido generado");
            }

            try {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    bd.Database.ExecuteSqlRaw("GENERACION_INTERESES");
                    bd.SaveChanges();
                    return new ResultadoEjecucionProcesoCLS("OK");
                }
            } catch (Exception ex) {
                Console.WriteLine("Error al generar los intereses para el año " + anio
                    + " y mes " + mes + ". Error: " + ex.Message);
                return new ResultadoEjecucionProcesoCLS(
                    "ERROR", "No se pudieron generar los intereses para el año " + anio
                    + " y mes " + mes);
            }
        }

        public ResultadoEjecucionProcesoCLS confirmarBoletas() {
            int anio = DateTime.Now.Year;

            if (estaProcesoEjecutadoParaAnio(3, anio)) {
                return new ResultadoEjecucionProcesoCLS(
                    "ERROR", "La confirmación de boletas ya ha sido realizada previamente");
            }

            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    bd.Database.ExecuteSqlRaw("BORRADO_BOLETAS");
                    bd.SaveChanges();
                    return new ResultadoEjecucionProcesoCLS("OK");
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error al confirmar las boletas para el año " + anio
                    + ". Error: " + ex.Message);
                return new ResultadoEjecucionProcesoCLS(
                    "ERROR", "No se pudieron confirmar las boletas para el año " + anio);
            }
        }

        private Boolean estaProcesoEjecutadoParaAnio(int idProceso, int anio) {

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                return bd.ControlProcesos.Where(cp => cp.IdProceso == idProceso
                && cp.FechaEjecucion.Year == anio).Count() > 0;
            }

        }

        private Boolean estaProcesoEjecutadoParaAnioYMes(int idProceso, int anio, int mes)
        {

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                return bd.ControlProcesos.Where(cp => cp.IdProceso == idProceso
                && cp.FechaEjecucion.Year == anio && cp.FechaEjecucion.Month == mes).Count() > 0;
            }

        }

        private ResultadoEjecucionProcesoCLS generarImpuestoAnual(
            SolicitudGeneracionImpuestosCLS solicitud) {
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    SqlParameter construido = new SqlParameter("@montoSupEdificada", solicitud.montoSuperficieEdificada);
                    SqlParameter importe_sin_contruir = new SqlParameter("@montoSupTerreno", solicitud.montoSuperficieTerreno);
                    SqlParameter interes_esquina = new SqlParameter("@interes_esquina", solicitud.coeficienteInteresEsquina);
                    SqlParameter interes_asfalto = new SqlParameter("@interes_asfalto", solicitud.coeficienteInteresAsfalto);
                    bd.Database.ExecuteSqlRaw("GENERACION_IMPUESTOS_LOTES @montoSupEdificada,@montoSupTerreno, @interes_esquina," +
                                                  "@interes_asfalto", construido, importe_sin_contruir, interes_esquina, interes_asfalto);
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

        public IEnumerable<ImpuestoPagoDto> listarImpuestosPagados(int idLote)
        {
            
            List<ImpuestoPagoDto> result = new List<ImpuestoPagoDto>();

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                result = (from ii in bd.Impuestoinmobiliarios
                                  join det in bd.Detalleboleta
                                  on ii.IdImpuesto equals det.IdImpuesto
                                  join bol in bd.Boleta
                                  on det.IdBoleta equals bol.IdBoleta
                                  where ii.IdLote == idLote
                                  && bol.FechaPago != null
                                  && det.Estado == 1
                                  orderby bol.FechaPago descending, ii.Año, ii.Mes
                                  select new ImpuestoPagoDto
                                  {
                                      idImpuesto = (int)ii.IdImpuesto,
                                      mes = (int)ii.Mes,
                                      anio = (int)ii.Año,
                                      fechaPago = (DateTime)bol.FechaPago,
                                      importePagado = (decimal)det.Importe,
                                      periodo = ii.Mes == 0 ?  ii.Año.ToString() : 
                                      ii.Año.ToString() + "/" + ii.Mes.ToString(),
                                      cuota = ii.Mes == 0 ? "Única" : ii.Mes .ToString()
                                  }).ToList();

                return result;
            }
        }
    }
      
}
