import { Component, OnInit } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IndicadoresService } from '../../services/indicadores.service';
//import { Chart } from 'chart.js/auto';
import Chart, { ChartType } from 'chart.js/auto';



@Component({
  selector: 'bienvenida',
  templateUrl: './bienvenida.component.html',
  styleUrls: ['./bienvenida.component.css']
})
export class BienvenidaComponent implements OnInit {

  idEmpleado: any;
  nombreEmpleado: any;
  chartDenuncia: any;
  totalDenuncias: any;
  totalDenunAsignaEmpleado: any;
  totalDenunCerradas: any;
 // public chart: Chart;
  

  constructor(private usuarioService: UsuarioService, private indicadoresService: IndicadoresService) {
    //this.chart = new Chart('canvas', {
    //  type: 'pie',
    //  data: {
    //    labels: ['Red', 'Blue', 'Yellow'],
    //    datasets: [{
    //      label: 'My First Dataset',
    //      data: [30, 50, 10],
    //      backgroundColor: [
    //        'rgba(255, 99, 132, 0.2)',
    //        'rgba(54, 162, 235, 0.2)',
    //        'rgba(255, 206, 86, 0.2)'
    //      ],
    //      borderColor: [
    //        'rgba(255, 99, 132, 1)',
    //        'rgba(54, 162, 235, 1)',
    //        'rgba(255, 206, 86, 1)'
    //      ],
    //      borderWidth: 1
    //    }]
    //  }
    //});

  }
  ngOnInit() {
    this.indicadoresService.getDenunciasxEmpleado().subscribe(chartDenuncia => {
      this.idEmpleado = chartDenuncia.idUsuario;
      this.nombreEmpleado = chartDenuncia.nombreEmpleado;
      this.totalDenuncias = chartDenuncia.totalDenuncias;
      this.totalDenunAsignaEmpleado = chartDenuncia.totalDenunAsignaEmpleado;
      console.log(this.idEmpleado, this.nombreEmpleado, this.totalDenuncias, this.totalDenunAsignaEmpleado);
    });
    this.indicadoresService.getDenunciasCerradas().subscribe(chartDenuncia => {
      this.idEmpleado = chartDenuncia.idUsuario;
      this.nombreEmpleado = chartDenuncia.nombreEmpleado;
      this.totalDenunCerradas = chartDenuncia.totalDenuncias;
      this.totalDenunAsignaEmpleado = chartDenuncia.totalDenunAsignaEmpleado;
      console.log(this.idEmpleado, this.nombreEmpleado, this.totalDenuncias, this.totalDenunAsignaEmpleado);
    });
/////////////*******charts************ */
    // datos
    //const data = {
    //  labels: [
    //    'Blue',
    //    'Yellow'
    //  ],
    //  datasets: [{
    //    label: 'My First Dataset',
    //    data: [300, 50, 100],
    //    backgroundColor: [
    //      'rgb(255, 99, 132)',
    //      'rgb(54, 162, 235)',
    //      'rgb(255, 205, 86)'
    //    ],
    //    hoverOffset: 4
    //  }]
    //};
    //// Creamos la gráfica
    //this.chart = new Chart("chart", {
    //  type: 'pie' as ChartType, // tipo de la gráfica 
    //  data // datos 
    //})
/////****fin charts*****/
  }


}




