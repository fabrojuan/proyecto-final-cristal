using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public interface IindicadoresService
    {
        public int DenunciasAbiertas();
        public int DenunciasCerradas();
       public IEnumerable<CantTrabajosEnDenunciaCLS> FechaTrabajosEnDenuncias();

       public ChartDataDto getDatosChartReclamosCerradosPorMesyTipoCierre();
       public ChartDataDto getDatosChartReclamosNuevosPorMes();
       public ChartDataDto getDatosChartReclamosAbiertosPorEstado();
       public ChartDataDto getDatosChartTrabajosReclamosPorAreaYMes();

        //el total de las denuncias se obtinene de la suma de estas dos

    }
}
