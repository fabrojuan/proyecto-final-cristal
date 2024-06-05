import { Component, OnInit } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IndicadoresService } from '../../services/indicadores.service';



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

  

  constructor(private usuarioService: UsuarioService, private indicadoresService: IndicadoresService) {
    
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




