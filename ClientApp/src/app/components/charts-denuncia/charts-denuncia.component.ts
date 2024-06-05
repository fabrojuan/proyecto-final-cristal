import { Component, OnInit } from '@angular/core';
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
  labeldata: any[] = [];
  realdata: any[] = [];
  colordata: any[] = ['rgb(54, 162, 235)',
    'rgb(255, 205, 86)', 'rgb(153, 0, 153)', 'rgb(255,255, 51)'];


  ngOnInit() {
    this.indicadoresService.CantidadDenunciasAbiertas().subscribe(abiertas => {
      if (abiertas) { this.denunciasAbiertas = abiertas; }
      this.indicadoresService.CantidadDenunciasCerradas().subscribe(cerradas => {
        if (cerradas) { this.denunciasCerradas = cerradas; }
        //console.log("Trae la cantidad de denuncias Cerradas:" + this.denunciasCerradas);
        this.chart = new Chart("chart", {
          type: 'pie' as ChartType, // tipo de la gráfica 
          data: this.getChartData() // datos 
        });
      });
    });


    this.indicadoresService.FechaTrabajosEnDenuncias().subscribe(result => {
      this.chartdata = result;
      if (this.chartdata != null) {
        for (let i = 0; i < this.chartdata.length; i++) {
          console.log(this.chartdata[i]);
          this.labeldata.push(this.formatFecha(this.chartdata[i].fecha));
          this.realdata.push(this.chartdata[i].cantidadPorFecha);
         this.colordata.push(this.chartdata[i].colorcode);
        }
        this.RenderChart(this.labeldata, this.realdata, this.colordata, 'bar', 'barchart');
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
          label: '#Cantidad de trabajosxMes',
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
          // 'rgb(255, 99, 132)',
          'rgb(54, 162, 235)',
          'rgb(255, 205, 86)'
        ],
        hoverOffset: 4
      }]
    };
  }


  volver() {
    this.router.navigate(["/bienvenida"]);
  }

  volverAtras() {
    this.router.navigate(["/indicadores-graficos"]);
  }

}





//  }//fin ngoninit





