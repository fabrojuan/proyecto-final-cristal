import { Component, OnInit } from '@angular/core';
import { ImpuestoService } from '../../services/impuesto.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'impuesto-pago-send',
  templateUrl: './impuesto-pago-send.component.html',
  styleUrls: ['./impuesto-pago-send.component.css']
})
export class ImpuestoPagoSendComponent implements OnInit {
  mobexx: any;
  mobexx2: any;
  generarCupon: boolean = true;
  generarPago: boolean = false;
  titulo: any = `Pulse el botón "Generar Cupón" para iniciar el proceso de pagos`;
  constructor(private impuestoService: ImpuestoService, private router: Router) { }

  GenerarCupon() {
    this.generarPago = true;
    this.generarCupon = false;
    this.titulo = "";  }
  sendToMbx() {
    console.log("Lllamo a la funcion.");
    //  this.respuesta =
    this.impuestoService.obtenerUrlMobbexx().subscribe(data => {
      this.mobexx = data
      //window.location.href = this.mobexx;
      var myJSON = decodeURIComponent(this.mobexx);
      var myObject = JSON.parse(myJSON);
      // console.log("Se hace +" + myObject);
      console.log("Se hace +" + this.mobexx);
      // this.router.navigate([this.mobexx]);
      this.router.navigateByUrl(this.mobexx);
    });
    console.log(this.mobexx);
  }

  sendToMbx2() {
    this.generarPago = true;
    this.generarCupon = false;
    this.titulo = `Ahora pulse el botón "Generar Pago" para ser redireccionado al sitio de pagos`;
//  this.respuesta =
    this.impuestoService.obtenerUrlMobbexx2().subscribe(data => {
      this.mobexx2 = data;
      //window.location.href = this.mobexx;
      var myJSON = decodeURIComponent(this.mobexx2);
      this.mobexx2 = JSON.parse(myJSON);
      // console.log("Se hace +" + myObject);
      //console.log("Se hace +" + this.my);
      // this.router.navigate([this.mobexx]);
      // this.router.navigateByUrl(this.mobexx);
    });
    console.log('La url es', this.mobexx2);
    /*this.router.navigate(["/"]).then(result => {*/
    //window.location.href = this.mobexx2;
    //});
  }

  volver() {
    this.router.navigate(["/impuestos-vecino-identificador"]);
  }
  ngOnInit() {
  }

}
