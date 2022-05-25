import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ImpuestoService } from '../../services/impuesto.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'ejecucion-procesos',
  templateUrl: './ejecucion-procesos.component.html',
  styleUrls: ['./ejecucion-procesos.component.css']
})
export class EjecucionProcesosComponent implements OnInit {
  generacionImpuestos: any;
  interesMensual: any;
  fechaEjecucion: Date = new Date();
  limpiezaBoleta: any;
  fechaHoy: Date = new Date();
  // fechaEjecucion: Date ;
  MesfechaEjeTemp: any;
  MesActualTemp: any;
  aniofechaEjeTemp: any;
  anioActualTemp: any;

  constructor(private impuestoService: ImpuestoService, private router: Router, @Inject('BASE_URL') baseUrl: string) { }

  ngOnInit() {
  }

  //CONFIRMACION DE BOLETAS DIARIAS
  getConfirmacionBoletas() {
    this.impuestoService.getUltimaFechaBoleta().subscribe(param => this.limpiezaBoleta = param);
    this.fechaEjecucion = new Date(this.limpiezaBoleta.fechaEjecucion);
    console.log('dia de ultima ejecucion:' + this.fechaEjecucion.toLocaleString('es-AR', { day: 'numeric' }));
    this.aniofechaEjeTemp = this.fechaEjecucion.toLocaleString('es-AR', { day: '2-digit' });
    console.log('dia Actual' + this.fechaHoy.toLocaleString('es-AR', { day: '2-digit' }));
    this.anioActualTemp = this.fechaHoy.toLocaleString('es-AR', { day: '2-digit' });
    if (this.anioActualTemp == this.aniofechaEjeTemp) {
      alert("La confirmacion de boletas ya ha sido realizada previamente");
      this.router.navigate(["/bienvenida"]);
    }
    else {
      alert("La confirmacion de boletas se realizó exitosamente");
      this.impuestoService.SP_LimpiezaBoletas().subscribe(param => this.limpiezaBoleta = param);
    }
  }



  //Generacion Anual de Impuestos
  getGeneracionImpuestos() {
    this.impuestoService.getUltimaFechaInteres().subscribe(param => this.generacionImpuestos = param);
    this.fechaEjecucion = new Date(this.generacionImpuestos.fechaEjecucion);
    // console.log('AÑO de ultima ejecucion:' + this.fechaEjecucion.toLocaleString('es-AR', { year: 'numeric' }));
    this.aniofechaEjeTemp = this.fechaEjecucion.toLocaleString('es-AR', { year: 'numeric' });
    // console.log('AÑO Actual' + this.fechaHoy.toLocaleString('es-AR', { year: 'numeric' }));
    this.anioActualTemp = this.fechaHoy.toLocaleString('es-AR', { year: 'numeric' });
    if (this.anioActualTemp == this.aniofechaEjeTemp) {
      alert("La generacion de Impuestos ya se ha realizado este año");
      this.router.navigate(["/bienvenida"]);
    }
    else {
      alert("La generacion de Impuestos se realizó exitosamente");
      this.impuestoService.SP_GeneracionImpuestos().subscribe(param => this.generacionImpuestos = param);
    }
  }
  //Generacion de Interes Mensual.
  getinteresMensual() {
    this.impuestoService.getUltimaFechaInteres().subscribe(param => this.interesMensual = param);
    this.fechaEjecucion = new Date(this.interesMensual.fechaEjecucion);
    console.log('Mes ultima ejecucion:' + this.fechaEjecucion.toLocaleString('en-us', { month: 'long' }));
    this.MesfechaEjeTemp = this.fechaEjecucion.toLocaleString('en-us', { month: 'long' });
    console.log('Mes Actual' + this.fechaHoy.toLocaleString('en-us', { month: 'long' }));
    this.MesActualTemp = this.fechaHoy.toLocaleString('en-us', { month: 'long' });
    if (this.MesActualTemp == this.MesfechaEjeTemp) {
      alert("El INTERES MENSUAL ya ha sido ejecutado anteriormente");
      this.router.navigate(["/bienvenida"]);
    }
    else {
      alert("El INTERES ha sido ejecutado exitosamente");
      this.impuestoService.SP_GeneracionInteresesMensuales().subscribe(param => this.generacionImpuestos = param);

    }
  }

  //      this.error = true;v
  //      this.router.navigate(["/home"]);

  //    }







}
