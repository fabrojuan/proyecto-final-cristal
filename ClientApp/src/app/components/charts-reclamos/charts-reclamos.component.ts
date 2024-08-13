import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { IndicadoresService } from 'src/app/services/indicadores.service';

@Component({
  selector: 'app-charts-reclamos',
  templateUrl: './charts-reclamos.component.html',
  styleUrls: ['./charts-reclamos.component.css']
})
export class ChartsReclamosComponent implements OnInit {

  chartReclamosCerradosPorMes?: Chart;
  chartReclamosNuevosPorMes?: Chart;
  chartReclamosAbiertosPorEstado?: any;
  chartTrabajosReclamosPorAreaYMes?: any;
  chartComparativaMensualTiempoResolucionReclamos?: Chart;
  dataChartReclamosCerradosPorMes: any;
  dataChartReclamosNuevosPorMes: any;
  dataChartReclamosAbiertosPorEstado: any;
  dataChartTrabajosReclamosPorAreaYMes: any;
  dataChartComparativaMensualTiempoResolucionReclamos: any;

  tituloPeriodoIndicadorTiempoMedioResolucionRequerimientos: string = "Este Mes"
  valorActual: string = ""
  valorVariacion: string = ""
  tipoVariacion: string = ""

  constructor(private indicadoresService: IndicadoresService) {

    this.indicadoresService.getReclamosCerradosPorMesYTipoCierre().subscribe(resp => {

      this.dataChartReclamosCerradosPorMes = resp;
      this.chartReclamosCerradosPorMes = new Chart("chartReclamosCerradosPorMes", {
        type: 'bar',
        data: this.dataChartReclamosCerradosPorMes,
        options: {
          plugins: {
            title: {
              display: false,
              text: 'Últimos 6 meses'
            },
          },
          responsive: true,
          scales: {
            x: {
              stacked: true,
            },
            y: {
              stacked: true
            }
          }
        }
      });

    });


    this.indicadoresService.getReclamosNuevosPorMes().subscribe(resp => {

      this.dataChartReclamosNuevosPorMes = resp;

      this.chartReclamosNuevosPorMes = new Chart("chartReclamosNuevosPorMes", {

        type: 'line',
        data: this.dataChartReclamosNuevosPorMes,
        options: {
          scales: {
            y: {
              beginAtZero: true
            }
          },
          plugins: {
            title: {
              display: false,
              text: 'Últimos 6 meses'
            },
          }
        }

      });

    });

    this.indicadoresService.getReclamosAbiertosPorEstado().subscribe(resp => {

      this.dataChartReclamosAbiertosPorEstado = resp;

      this.chartReclamosAbiertosPorEstado = new Chart("chartReclamosAbiertosPorEstado", {
        type: 'doughnut',
        data: this.dataChartReclamosAbiertosPorEstado,
        options: {
          responsive: true,
          plugins: {
            legend: {
              position: 'top',
            },
            title: {
              display: false
            }
          }
        }
      });

    });


    this.indicadoresService.getTrabajosReclamosPorAreaYMes().subscribe(resp => {

      this.dataChartTrabajosReclamosPorAreaYMes = resp;
      this.chartReclamosCerradosPorMes = new Chart("chartTrabajosReclamosPorAreaYMes", {
        type: 'bar',
        data: this.dataChartTrabajosReclamosPorAreaYMes,
        options: {
          plugins: {
            title: {
              display: false,
              text: 'Últimos 6 meses'
            },
          },
          responsive: true,
          scales: {
            x: {
              stacked: true,
            },
            y: {
              stacked: true
            }
          }
        }
      });

    });

    /**
     * Inicio ChartComparativaMensualTiempoResolucionRequerimientos 
     */
    this.indicadoresService.getDatosChartComparativaMensualTiempoResolucionRequerimientos().subscribe(resp => {
      this.dataChartComparativaMensualTiempoResolucionReclamos = resp;
      this.chartComparativaMensualTiempoResolucionReclamos = new Chart("chartComparativaMensualTiempoResolucionReclamos", {
        type: 'bar',
        data: this.dataChartComparativaMensualTiempoResolucionReclamos,
        options: {
          responsive: true,
          plugins: {
            legend: {
              position: 'top',
            },
            title: {
              display: false,
              text: 'Chart.js Bar Chart'
            }
          }
        }
      })
    });
    /**
     * Fin ChartComparativaMensualTiempoResolucionRequerimientos
     */

  }

  ngOnInit(): void { 
    this.cargarDatos("MES");
  }

  private cargarDatos(tipoPeriodo: string) {
    this.indicadoresService.getIndicadorTiempoMedioResolucionRequerimientosGeneral(tipoPeriodo)
      .subscribe(res => {
        console.log(res);
        this.valorActual = res.valorActual;
        this.valorVariacion = res.valorVariacion;
        this.tipoVariacion = res.tipoVariacion;
      });
  }

  verTiempoMedioResolucionRequerimientosMes() {
    this.tituloPeriodoIndicadorTiempoMedioResolucionRequerimientos = "Este Mes"
    this.cargarDatos("MES");
  }

  verTiempoMedioResolucionRequerimientosTrimestre() {
    this.tituloPeriodoIndicadorTiempoMedioResolucionRequerimientos = "Este Trimestre"
    this.cargarDatos("TRIMESTRE");
  }

  verTiempoMedioResolucionRequerimientosAnio() {
    this.tituloPeriodoIndicadorTiempoMedioResolucionRequerimientos = "Este Año"
    this.cargarDatos("ANIO");
  }

}
