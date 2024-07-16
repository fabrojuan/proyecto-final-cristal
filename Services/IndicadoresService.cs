using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class IndicadoresService : IindicadoresService
    {
         int IindicadoresService.DenunciasAbiertas()
        {
           // throw new NotImplementedException();
       
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                DateTime diasatras = DateTime.Now.AddDays(-180);
                int denunciasAbiertas = bd.Denuncia
                    .Where(denuncia => denuncia.Fecha >= diasatras && denuncia.CodEstadoDenuncia != 8)
                    .Count();
               
                    if (denunciasAbiertas==0)
                    {
                        throw new Exception("No hay Denuncias abiertas en el ultimo mes.");

                    }
                    return denunciasAbiertas;
                    
                }

            }
        

         int IindicadoresService.DenunciasCerradas()
        {
        using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
        {
            DateTime diasatras = DateTime.Now.AddDays(-180);
            int denunciasCerradas = bd.Denuncia
                .Where(denuncia => denuncia.Fecha >= diasatras && denuncia.CodEstadoDenuncia == 8)
                .Count();

            if (denunciasCerradas == 0)
            {
                throw new Exception("No hay Denuncias cerradas en el ultimos mes.");

            }
            return denunciasCerradas;
           }
    }
        //////////////////***************///////////////////

        [HttpGet]
        [Route("FechaTrabajosEnDenuncias")]
         IEnumerable<CantTrabajosEnDenunciaCLS> IindicadoresService.FechaTrabajosEnDenuncias()
        {
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {

                    DateTime diasatras = DateTime.Now.AddDays(-180);
                    List<CantTrabajosEnDenunciaCLS> listaFechasTrabajo = (from trabajo in bd.Trabajos
                                                           join denuncia in bd.Denuncia
                                                          on trabajo.NroDenuncia equals denuncia.NroDenuncia
                                                          where trabajo.Fecha >= diasatras
                                                          select new CantTrabajosEnDenunciaCLS
                                                           {
                                                             Fecha = (DateTime)trabajo.Fecha,
                                                          }).ToList();
                    //var resultado = listaFechasTrabajo   //funcion original que agrupa por fecha especifica
                    //  .GroupBy(x => x.Fecha.Date)
                    //  .Select(g => new CantTrabajosEnDenunciaCLS
                    //  {

                    //      Fecha = g.Key,
                    //      cantidadPorFecha = g.Count()
                    //  }).ToList();
                    //return resultado;
                    var fechaActual = DateTime.Now;
                    var fechaInicio = fechaActual.AddMonths(-6); // Hace 6 meses desde hoy

                    var resultado = listaFechasTrabajo
                        .Where(x => x.Fecha >= fechaInicio) // Filtrar fechas dentro del rango de los últimos 3 meses
                        .GroupBy(x => new { x.Fecha.Year, x.Fecha.Month, Quincena = (x.Fecha.Day - 1) / 15 + 1 }) // Agrupar por año, mes y quincena
                        .Select(g => new CantTrabajosEnDenunciaCLS
                        {
                            Fecha = new DateTime(g.Key.Year, g.Key.Month, g.Key.Quincena * 15), // Crear una nueva fecha con el último día de la quincena correspondiente
                            cantidadPorFecha = g.Count()
                        }).ToList();
                        return resultado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("AlgorithmConfiguration salio mal");
            }
        } //Cierre de La CLASE


          //////////////////aca debo devoler la cantidad de denuncias realizadas por tipo///////////////////

        [HttpGet]
        [Route("DenunciasporTipo")]
        IEnumerable<TrabajosEnDenunciaporTipoCLS> IindicadoresService.DenunciasporTipo()
        {
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {

                    DateTime diasatras = DateTime.Now.AddDays(-180);
                    List<TrabajosEnDenunciaporTipoCLS> denunciasXTipo = (from tipoDenuncia in bd.TipoDenuncia
                                                                         join denuncia in bd.Denuncia
                                                                        on tipoDenuncia.CodTipoDenuncia equals denuncia.CodTipoDenuncia
                                                                         where denuncia.Fecha >= diasatras
                                                                         select new TrabajosEnDenunciaporTipoCLS
                                                                         { 
                                                                             tipoDenuncia = !String.IsNullOrEmpty(tipoDenuncia.Nombre) ? tipoDenuncia.Nombre : "No tipificada",
                                                                          }).ToList();
                    //var fechaActual = DateTime.Now;
                    //var fechaInicio = fechaActual.AddMonths(-6); // Hace 6 meses desde hoy

                    var resultado = denunciasXTipo
                        .Where(x => x.tipoDenuncia != "") // Filtrar fechas dentro del rango de los últimos 3 meses
                        .GroupBy(x => new { x.tipoDenuncia }) // Agrupar por año, mes y quincena
                        .Select(g => new TrabajosEnDenunciaporTipoCLS
                        {
                            cantidadporTipo = g.Count(),
                            tipoDenuncia= g.Key.tipoDenuncia
                        }).ToList();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("AlgorithmConfiguration salio mal");
            }
        } //Cierre de La CLASE





    }
}

