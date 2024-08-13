using MVPSA_V2022.clases;
using MVPSA_V2022.Enums;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public interface IindicadoresService
    {
        public int DenunciasAbiertas();
        public int DenunciasCerradas();
       public IEnumerable<CantTrabajosEnDenunciaCLS> FechaTrabajosEnDenuncias();
        public IEnumerable<TrabajosEnDenunciaporTipoCLS> DenunciasporTipo();

        

       public ChartDataDto getDatosChartReclamosCerradosPorMesyTipoCierre();
       public ChartDataDto getDatosChartReclamosNuevosPorMes();
       public ChartDataDto getDatosChartReclamosAbiertosPorEstado();
       public ChartDataDto getDatosChartTrabajosReclamosPorAreaYMes();
       public ChartDoubleDataDto getDatosChartComparativaMensualTiempoResolucionRequerimientos();
       public IndicadorGenericoDto getIndicadorTiempoMedioResolucionRequerimientosGeneral(TipoPeriodoEnum tipoPeriodo);
       
    }
}
