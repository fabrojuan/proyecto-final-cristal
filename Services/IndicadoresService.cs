using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using Microsoft.VisualBasic;
using MVPSA_V2022.clases;
using MVPSA_V2022.Enums;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class IndicadoresService : IindicadoresService
    {
        private readonly M_VPSA_V3Context dbContext;

        public IndicadoresService(M_VPSA_V3Context dbContext)
        {
            this.dbContext = dbContext;
        }

        public ChartDataDto getDatosChartReclamosCerradosPorMesyTipoCierre()
        {
            DateTime mesDesde = DateTime.Today.AddMonths(-5);

            int[] meses = new int[6];
            meses[0] = mesDesde.Month;
            for (int i = 1; i < 6; i++)
            {
                if (meses[i - 1] != 12)
                {

                DateTime diasatras = DateTime.Now.AddDays(-180);

                    meses[i] = meses[i - 1] + 1;
                }
                else
                {
                    meses[i] = 1;
                }
            }

            String[] mesesNombre = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            int[] datosSolucionado = new int[6];
            int[] datosCancelado = new int[6];
            int[] datosRechazado = new int[6];

            DateTime fechaDesde = new DateTime(mesDesde.Year, mesDesde.Month, 1);
            this.dbContext.Reclamos
                .Where(reclamos => reclamos.FechaCierre >= fechaDesde)
                .ToList()
                .ForEach(rec =>
                {

                    int[] datosSegunEstado = new int[6];
                    if (rec.CodEstadoReclamo == (int)EstadoReclamoEnum.SOLUCIONADO)
                    {
                        datosSegunEstado = datosSolucionado;
                    }
                    else if (rec.CodEstadoReclamo == (int)EstadoReclamoEnum.CANCELADO)
                    {
                        datosSegunEstado = datosCancelado;
                    }
                    else if (rec.CodEstadoReclamo == (int)EstadoReclamoEnum.RECHAZADO)
                    {
                        datosSegunEstado = datosRechazado;
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        if (rec.FechaCierre.GetValueOrDefault().Month == meses[i])
                        {
                            datosSegunEstado[i] = datosSegunEstado[i] + 1;
                        }
                    }

                });

            ChartDataDto chartDataDto = new ChartDataDto();
            chartDataDto.labels = new String[6] { mesesNombre[meses[0] - 1], mesesNombre[meses[1] - 1], mesesNombre[meses[2] - 1], mesesNombre[meses[3] - 1], mesesNombre[meses[4] - 1], mesesNombre[meses[5] - 1] };

            ChartDatasetDto chartDatasetDto1 = new ChartDatasetDto();
            chartDatasetDto1.label = "Solucionados";
            chartDatasetDto1.data = datosSolucionado;
            chartDatasetDto1.backgroundColor = new string[] {"rgb(255, 99, 132)"};

            ChartDatasetDto chartDatasetDto2 = new ChartDatasetDto();
            chartDatasetDto2.label = "Cancelados";
            chartDatasetDto2.data = datosCancelado;
            chartDatasetDto2.backgroundColor = new string[] { "rgb(75, 192, 192)" };

            ChartDatasetDto chartDatasetDto3 = new ChartDatasetDto();
            chartDatasetDto3.label = "Rechazados";
            chartDatasetDto3.data = datosRechazado;
            chartDatasetDto3.backgroundColor = new string[] { "rgb(255, 205, 86)" };

            chartDataDto.datasets = new ChartDatasetDto[3] { chartDatasetDto1, chartDatasetDto2, chartDatasetDto3 };

            return chartDataDto;
        }

        public ChartDataDto getDatosChartReclamosNuevosPorMes()
        {
            DateTime mesDesde = DateTime.Today.AddMonths(-5);

            int[] meses = new int[6];
            meses[0] = mesDesde.Month;
            for (int i = 1; i < 6; i++)
            {
                if (meses[i - 1] != 12)
                {
                    meses[i] = meses[i - 1] + 1;
                }
                else
                {
                    meses[i] = 1;
                }
            }

            String[] mesesNombre = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            int[] datos = new int[6];

            DateTime fechaDesde = new DateTime(mesDesde.Year, mesDesde.Month, 1);
            this.dbContext.Reclamos
                .Where(reclamos => reclamos.Fecha >= fechaDesde)
                .ToList()
                .ForEach(rec =>
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (rec.Fecha.GetValueOrDefault().Month == meses[i])
                        {
                            datos[i] = datos[i] + 1;
                        }
                    }

                });

            ChartDataDto chartDataDto = new ChartDataDto();
            chartDataDto.labels = new String[6] { mesesNombre[meses[0] - 1], mesesNombre[meses[1] - 1], mesesNombre[meses[2] - 1], mesesNombre[meses[3] - 1], mesesNombre[meses[4] - 1], mesesNombre[meses[5] - 1] };

            ChartDatasetDto chartDatasetDto1 = new ChartDatasetDto();
            chartDatasetDto1.label = "Cantidad";
            chartDatasetDto1.data = datos;
            chartDatasetDto1.borderColor = "rgb(75, 192, 192)";
            chartDatasetDto1.backgroundColor = new string[] { "rgb(75, 192, 192)" };

            chartDataDto.datasets = new ChartDatasetDto[1] { chartDatasetDto1 };

            return chartDataDto;
        }

        public ChartDataDto getDatosChartReclamosAbiertosPorEstado()
        {

            int[] datos = new int[4];

            this.dbContext.Reclamos
                .Where(reclamos => reclamos.CodEstadoReclamo == (int)EstadoReclamoEnum.CREADO
                    || reclamos.CodEstadoReclamo == (int)EstadoReclamoEnum.EN_CURSO
                    || reclamos.CodEstadoReclamo == (int)EstadoReclamoEnum.SUSPENDIDO
                    || reclamos.CodEstadoReclamo == (int)EstadoReclamoEnum.PENDIENTE_CIERRE)
                .ToList()
                .ForEach(rec =>
                {
                    
                    if (rec.CodEstadoReclamo == (int)EstadoReclamoEnum.CREADO)
                    {
                        datos[0] = datos[0] + 1;
                    }

                    if (rec.CodEstadoReclamo == (int)EstadoReclamoEnum.EN_CURSO)
                    {
                        datos[1] = datos[1] + 1;
                    }

                    if (rec.CodEstadoReclamo == (int)EstadoReclamoEnum.SUSPENDIDO)
                    {
                        datos[2] = datos[2] + 1;
                    }

                    if (rec.CodEstadoReclamo == (int)EstadoReclamoEnum.PENDIENTE_CIERRE)
                    {
                        datos[3] = datos[3] + 1;
                    }
                    

                });

            ChartDataDto chartDataDto = new ChartDataDto();
            chartDataDto.labels = new String[4] { "Creado", "En Curso", "Suspendido", "Pendiente" };

            ChartDatasetDto chartDatasetDto1 = new ChartDatasetDto();
            chartDatasetDto1.label = "Cantidad";
            chartDatasetDto1.data = datos;
            chartDatasetDto1.borderColor = "white";
            chartDatasetDto1.backgroundColor = new string[] { "rgb(255, 99, 132)", "rgb(54, 162, 235)", "rgb(255, 205, 86)", "rgb(75, 192, 192)" };

            chartDataDto.datasets = new ChartDatasetDto[1] { chartDatasetDto1 };

            return chartDataDto;
        }

        public ChartDataDto getDatosChartTrabajosReclamosPorAreaYMes()
        {
            DateTime mesDesde = DateTime.Today.AddMonths(-5);

            int[] meses = new int[6];
            meses[0] = mesDesde.Month;
            for (int i = 1; i < 6; i++)
            {
                if (meses[i - 1] != 12)
                {
                    meses[i] = meses[i - 1] + 1;
                }
                else
                {
                    meses[i] = 1;
                }
            }

            String[] mesesNombre = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            int[] datosME = new int[6];
            int[] datosEV = new int[6];
            int[] datosAP = new int[6];
            int[] datosOV = new int[6];
            int[] datosRS = new int[6];

            DateTime fechaDesde = new DateTime(mesDesde.Year, mesDesde.Month, 1);
            this.dbContext.TrabajoReclamos
                .Where(tr => tr.FechaTrabajo >= fechaDesde)
                .ToList()
                .ForEach(tr =>
                {

                    int[] datosSegunArea = new int[6];
                    if (tr.NroAreaTrabajoNavigation.CodArea == "ME")
                    {
                        datosSegunArea = datosME;
                    }
                    else if (tr.NroAreaTrabajoNavigation.CodArea == "EV")
                    {
                        datosSegunArea = datosEV;
                    }
                    else if (tr.NroAreaTrabajoNavigation.CodArea == "AP")
                    {
                        datosSegunArea = datosAP;
                    }
                    else if (tr.NroAreaTrabajoNavigation.CodArea == "OV")
                    {
                        datosSegunArea = datosOV;
                    }
                    else if (tr.NroAreaTrabajoNavigation.CodArea == "RS")
                    {
                        datosSegunArea = datosRS;
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        if (tr.FechaTrabajo.Month == meses[i])
                        {
                            datosSegunArea[i] = datosSegunArea[i] + 1;
                        }
                    }

                });

            ChartDataDto chartDataDto = new ChartDataDto();
            chartDataDto.labels = new String[6] { mesesNombre[meses[0] - 1], mesesNombre[meses[1] - 1], mesesNombre[meses[2] - 1], mesesNombre[meses[3] - 1], mesesNombre[meses[4] - 1], mesesNombre[meses[5] - 1] };

            ChartDatasetDto chartDatasetDto1 = new ChartDatasetDto();
            chartDatasetDto1.label = "Mesa Entrada";
            chartDatasetDto1.data = datosME;
            chartDatasetDto1.backgroundColor = new string[] {"rgb(255, 99, 132)"};

            ChartDatasetDto chartDatasetDto2 = new ChartDatasetDto();
            chartDatasetDto2.label = "Espacios Verdes";
            chartDatasetDto2.data = datosEV;
            chartDatasetDto2.backgroundColor = new string[] { "rgb(75, 192, 192)" };

            ChartDatasetDto chartDatasetDto3 = new ChartDatasetDto();
            chartDatasetDto3.label = "Alumbrado Público";
            chartDatasetDto3.data = datosAP;
            chartDatasetDto3.backgroundColor = new string[] { "rgb(255, 205, 86)" };

            ChartDatasetDto chartDatasetDto4 = new ChartDatasetDto();
            chartDatasetDto4.label = "Obras Viales";
            chartDatasetDto4.data = datosOV;
            chartDatasetDto4.backgroundColor = new string[] { "rgb(54, 162, 235)" };

            ChartDatasetDto chartDatasetDto5 = new ChartDatasetDto();
            chartDatasetDto5.label = "Recolección Residuos";
            chartDatasetDto5.data = datosRS;
            chartDatasetDto5.backgroundColor = new string[] { "orange" };

            chartDataDto.datasets = new ChartDatasetDto[5] { chartDatasetDto1, chartDatasetDto2, chartDatasetDto3, chartDatasetDto4, chartDatasetDto5 };

            return chartDataDto;
        }

        int IindicadoresService.DenunciasAbiertas()
        {
            // throw new NotImplementedException();

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                DateTime diasatras = DateTime.Now.AddDays(-60);
                int denunciasAbiertas = bd.Denuncia
                    .Where(denuncia => denuncia.Fecha >= diasatras && denuncia.CodEstadoDenuncia != 8)
                    .Count();

                if (denunciasAbiertas == 0)
                {
                    throw new Exception("No hay Denuncias abiertas en el ultimo mes.");

                }
                return denunciasAbiertas;

            }
        }
        public int DenunciasCerradas()
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

       [HttpGet]
        [Route("FechaTrabajosEnDenuncias")]
            public IEnumerable<CantTrabajosEnDenunciaCLS> FechaTrabajosEnDenuncias()
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
        public IEnumerable<TrabajosEnDenunciaporTipoCLS> DenunciasporTipo()
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

       public IndicadorGenericoDto getIndicadorTiempoMedioResolucionRequerimientosGeneral(TipoPeriodoEnum tipoPeriodo)
        {
            IndicadorGenericoDto indicador = new IndicadorGenericoDto();

            DateTime fechaDesdeActual = DateTime.Now;
            DateTime fechaHastaActual = DateTime.Now;

            DateTime fechaDesdeAnterior = DateTime.Now;
            DateTime fechaHastaAnterior = DateTime.Now;

            if (tipoPeriodo == TipoPeriodoEnum.MES) {
                fechaDesdeActual = new DateTime(fechaHastaActual.Year, fechaHastaActual.Month, 1);
                fechaDesdeAnterior = fechaDesdeActual.AddMonths(-1);
                fechaHastaAnterior = fechaDesdeActual.AddDays(-1);

            } else if (tipoPeriodo == TipoPeriodoEnum.TRIMESTRE) {
                if (fechaHastaActual.Month >= 1 && fechaHastaActual.Month <= 3) {
                    fechaDesdeActual = new DateTime(fechaHastaActual.Year, 1, 1);

                } else if (fechaHastaActual.Month >= 4 && fechaHastaActual.Month <= 6) {
                    fechaDesdeActual = new DateTime(fechaHastaActual.Year, 4, 1);

                } else if (fechaHastaActual.Month >= 7 && fechaHastaActual.Month <= 9) {
                    fechaDesdeActual = new DateTime(fechaHastaActual.Year, 7, 1);
 
                } else {
                    fechaDesdeActual = new DateTime(fechaHastaActual.Year, 10, 1);

                }

                fechaDesdeAnterior = fechaDesdeActual.AddMonths(-3);
                fechaHastaAnterior = fechaDesdeActual.AddDays(-1);


            } else if (tipoPeriodo == TipoPeriodoEnum.ANIO) {
                fechaDesdeActual = new DateTime(fechaHastaActual.Year, 1, 1);
                fechaDesdeAnterior = new DateTime(fechaHastaActual.Year - 1, 1, 1);
                fechaHastaAnterior = fechaDesdeActual.AddDays(-1);       

            }

             Double valorActual = Math.Round(this.dbContext.Reclamos.Where(rec => rec.CodEstadoReclamo == (int)EstadoReclamoEnum.SOLUCIONADO
                 && rec.Fecha >= fechaDesdeActual && rec.Fecha <= fechaHastaActual && rec.FechaCierre != null && rec.Fecha != null)
                 .Select(rec => ((TimeSpan)(rec.FechaCierre - rec.Fecha)).Days + 1 )
                 .ToList().Average(), 2);

            Double valorAnterior = Math.Round(this.dbContext.Reclamos.Where(rec => rec.CodEstadoReclamo == (int)EstadoReclamoEnum.SOLUCIONADO
                 && rec.Fecha >= fechaDesdeAnterior && rec.Fecha <= fechaHastaAnterior && rec.FechaCierre != null && rec.Fecha != null)
                 .Select(rec =>  Math.Ceiling(((TimeSpan)(rec.FechaCierre - rec.Fecha)).TotalDays) )
                 .ToList().Average(), 2);

            indicador.valorActual = valorActual.ToString() + " días";
            indicador.valorAnterior = valorAnterior.ToString() + " días";
            indicador.valorVariacion = Math.Round(Math.Abs((valorActual * 100 / valorAnterior) - 100), 2) .ToString() + " %";
            indicador.periodo = tipoPeriodo.ToString();
            
            if (valorActual > valorAnterior) {
                indicador.tipoVariacion = "incremento";
            } else if (valorActual < valorAnterior) {
                indicador.tipoVariacion = "decremento";
            } else {
                indicador.tipoVariacion = "sin cambios";
            }

            return indicador;
        }

        public ChartDoubleDataDto getDatosChartComparativaMensualTiempoResolucionRequerimientos()
        {

            String[] mesesNombre = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            var fechaActual = DateTime.Now;
            var fechaDesde = new DateTime(fechaActual.Year - 1, 1, 1);
            var anioActual = fechaActual.Year;
            var anioAnterior = fechaDesde.Year;

            Double[] datosActual = new Double[fechaActual.Month];
            Double[] datosAnterior = new Double[fechaActual.Month];

            Double[] sumaCantDiasResolucionActual = new Double[fechaActual.Month];
            int[] sumaCantRequerimientosActual = new int[fechaActual.Month];

            Double[] sumaCantDiasResolucionAnterior = new Double[fechaActual.Month];
            int[] sumaCantRequerimientosAnterior = new int[fechaActual.Month];

            int[] meses = new int[fechaActual.Month];
            for (int i = 0; i < fechaActual.Month; i++) {
                meses[i] = i + 1;
                sumaCantDiasResolucionActual[i] = 0;
                sumaCantDiasResolucionAnterior[i] = 0;

                sumaCantRequerimientosActual[i] = 0;
                sumaCantRequerimientosAnterior[i] = 0;
               
                datosActual[i] = 0;
                datosAnterior[i] = 0;
            }

            this.dbContext.Reclamos.Where(rec => rec.CodEstadoReclamo == (int)EstadoReclamoEnum.SOLUCIONADO
                 && rec.Fecha >= fechaDesde && rec.FechaCierre != null && rec.Fecha != null)
                 .ToList()
                 .ForEach(rec => {

                    int indice = (int)(rec.Fecha?.Month - 1);
                    if (indice < fechaActual.Month) {
                        if (rec.Fecha?.Year == anioActual) {                        
                            sumaCantDiasResolucionActual[indice] = sumaCantDiasResolucionActual[indice] + ((TimeSpan)(rec.FechaCierre - rec.Fecha)).Days + 1;
                            sumaCantRequerimientosActual[indice] = sumaCantRequerimientosActual[indice] + 1;
                        } else {
                            sumaCantDiasResolucionAnterior[indice] = sumaCantDiasResolucionAnterior[indice] + ((TimeSpan)(rec.FechaCierre - rec.Fecha)).Days + 1;
                            sumaCantRequerimientosAnterior[indice] = sumaCantRequerimientosAnterior[indice] + 1;
                        }
                    }                    
                 });

            for (int i = 0; i < fechaActual.Month; i++) {
                datosActual[i] = Math.Round(sumaCantDiasResolucionActual[i] / sumaCantRequerimientosActual[i], 2);
                datosAnterior[i] = Math.Round(sumaCantDiasResolucionAnterior[i] / sumaCantRequerimientosAnterior[i], 2);
            }

            ChartDoubleDataDto chartDataDto = new ChartDoubleDataDto();
            chartDataDto.labels = new String[fechaActual.Month]; //{ mesesNombre[meses[0] - 1], mesesNombre[meses[1] - 1], mesesNombre[meses[2] - 1], mesesNombre[meses[3] - 1], mesesNombre[meses[4] - 1], mesesNombre[meses[5] - 1] };
            for (int i = 0; i < fechaActual.Month; i++) {
                chartDataDto.labels[i] = mesesNombre[i];
            }

            ChartDatasetDoubleDto chartDatasetDto1 = new ChartDatasetDoubleDto();
            chartDatasetDto1.label = "Año " + anioAnterior;
            chartDatasetDto1.data = datosAnterior;
            chartDatasetDto1.backgroundColor = new string[] {"rgb(255, 99, 132)"};

            ChartDatasetDoubleDto chartDatasetDto2 = new ChartDatasetDoubleDto();
            chartDatasetDto2.label = "Año " + anioActual;
            chartDatasetDto2.data = datosActual;
            chartDatasetDto2.backgroundColor = new string[] { "rgb(75, 192, 192)" };

            chartDataDto.datasets = new ChartDatasetDoubleDto[2] { chartDatasetDto1, chartDatasetDto2 };

            return chartDataDto;
        }
    }
    
}

   

        
    

