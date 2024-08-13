import { Component, OnInit, OnDestroy } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IndicadoresService } from '../../services/indicadores.service';
import { ActivatedRoute, Router } from '@angular/router';
import { registerables } from 'node_modules/chart.js'
import Chart, { ChartType } from 'chart.js/auto';
import { Time } from '@angular/common';

@Component({
  selector: 'charts-denuncia',
  templateUrl: './charts-denuncia.component.html',
  styleUrls: ['./charts-denuncia.component.css']
})
export class ChartsDenunciaComponent implements OnInit {
  public chart: Chart;

  chartDenuncia: any;
  denunciasAbiertas: Number = 0;
  denunciasCerradas: any = 0;
  cantFechaTrabajosEnDenuncias: any;
  Fecha: Date = new Date();
  
  constructor(private indicadoresService: IndicadoresService, private router: Router) {
    const data = {
      labels: [
        'Blue',
        'Yellow'
      ],
      datasets: [{
        label: '',
        data: [this.denunciasAbiertas, this.denunciasCerradas],
        backgroundColor: [
          'rgb(54, 162, 235)',
          'rgb(255, 205, 86)'
        ],
        hoverOffset: 4
      }]
    };
    // Creamos la gráfica
    this.chart = new Chart("chart", {
      type: 'pie' as ChartType, // tipo de la gráfica 
      data // datos 
    })


  }
  //fuera del constructor

  chartdata: any;
  chartdataTipo: any;
  labeldata: any[] = [];
  realdata: any[] = [];
  colordata: any[] = ['rgb(255, 99, 132)',
    'rgb(75, 192, 192)', 'rgb(255, 205, 86)', 'rgb(54, 162, 235)'];
  labeldata2: any[] = [];
  realdata2: any[] = [];
  colordata2: any[] = ['rgb(54, 162, 235)',
    'rgb(255, 205, 86)', 'rgb(153, 0, 153)', 'rgb(255,255, 51)'];

  ngOnInit() {
    this.indicadoresService.CantidadDenunciasAbiertas().subscribe(abiertas => {
      if (abiertas) { this.denunciasAbiertas = abiertas; }
      this.indicadoresService.CantidadDenunciasCerradas().subscribe(cerradas => {
        if (cerradas) { this.denunciasCerradas = cerradas; }
        this.chart = new Chart("chart", {
          type: 'pie' as ChartType, // tipo de la gráfica 
          data: this.getChartData() // datos 
        });
      });
    });


    this.indicadoresService.FechaTrabajosEnDenuncias().subscribe(result => {
      this.chartdata = result;
      this.labeldata.splice(0, this.labeldata.length);
      this.realdata.splice(0, this.realdata.length);
      if (this.chartdata != null) {
        for (let i = 0; i < this.chartdata.length; i++) {
          console.log(this.chartdata[i]);
          this.labeldata.push(this.formatFecha(this.chartdata[i].fecha));
          this.realdata.push(this.chartdata[i].cantidadPorFecha);
          this.colordata.push(this.chartdata[i].colorcode);
        }
        this.RenderChart(this.labeldata, this.realdata, this.colordata, 'bar', 'barchart');
      }
    });
    
  
  this.indicadoresService.Denunciasportipo().subscribe(result => {
    this.chartdataTipo = result;
    if (this.chartdataTipo != null) {
      this.labeldata.splice(0, this.labeldata.length);
      this.realdata.splice(0, this.realdata.length);

      for (let i = 0; i < this.chartdataTipo.length; i++) {
        console.log(this.chartdataTipo[i]);
       
        this.labeldata.push(this.chartdataTipo[i].tipoDenuncia);
        this.realdata.push(this.chartdataTipo[i].cantidadporTipo);
        this.colordata.push(this.chartdataTipo[i].colorcode);
      }
      this.RenderChartTipo(this.labeldata, this.realdata, this.colordata, 'bar', 'barchart2');
      // this.RenderChart(this.labeldata, this.realdata, this.colordata, 'pie', 'piechart');
    }
  });
    
  }

  formatFecha(dateString: string): string {
    const date = new Date(dateString);
    return `${date.getMonth() + 1}/${date.getFullYear()}`;
  }

  RenderChart(labeldata: any, maindata: any, colordata: any, type: any, id: any) {
    const myChart = new Chart(id, {
      type: type,
      data: {
        labels: labeldata,
        datasets: [{
          label: '#Cantidad de trabajos x Mes',
          data: maindata,
          backgroundColor: colordata,
          borderColor: [
            'rgba(255, 99, 132, 1)'
          ],
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }
  getChartData(): any {
    return {
      labels: [
        'Abiertas',
        'Cerradas'
      ],
      datasets: [{
        label: 'Denuncias',
        data: [this.denunciasAbiertas, this.denunciasCerradas],
        backgroundColor: [
          'rgb(54, 162, 235)',
          'rgb(255, 205, 86)'
        ],
        hoverOffset: 4
      }]
    };
  }
  RenderChartTipo(labeldata: any, maindata: any, colordata: any, type: any, id: any) {
    const myChart = new Chart(id, {
      type: type,
      data: {
        labels: labeldata,
        datasets: [{
          label: '#Tipos de Denuncias',
          data: maindata,
          backgroundColor: colordata
        }]
      },
      options: {
        plugins: {
          legend: {
            position: 'top',
            display:   false
          },
          title: {
            display: false
          }
        }
      }
    });
  }

  volver() {
    this.router.navigate(["/bienvenida"]);
  }

  volverAtras() {
    this.router.navigate(["/indicadores-graficos"]);
  }
  ngOnDestroy() {
    // Cancelar la suscripción cuando se destruya el componente
  // this.indicadoresService.Denunciasportipo().unsubscribe();
  }

}
