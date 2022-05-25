import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';
import { PruebaGraficaService } from '../../services/prueba-grafica.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DenunciaService } from '../../services/denuncia.service';

@Component({
  selector: 'trabajo-detalle-denuncia',
  templateUrl: './trabajo-detalle-denuncia.component.html',
  styleUrls: ['./trabajo-detalle-denuncia.component.css']
})
export class TrabajoDetalleDenunciaComponent implements OnInit {
  //Trabajo: FormGroup;
  Empleados: any;
  trabajo: any;
  pruebas: any;
  parametro: any;
  NroDenuncia: any;
  foto: any;
  Foto: any;
  constructor(private TrabajoService: TrabajoService, private denunciaService: DenunciaService, private PruebaGraficaService: PruebaGraficaService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"];
      if (this.parametro >= 1) {
        console.log(this.parametro);
      } else {
        this.NroDenuncia = 0;
      }
    });

  }


  ngOnInit() {
    this.TrabajoService.getUsuario().subscribe(data => this.Empleados = data);

    //original if (this.parametro >= 1) {
    //  this.TrabajoService.detalleTrabajoDenuncia(this.parametro).subscribe(param => this.trabajo = param);

    if (this.parametro >= 1) {
      this.TrabajoService.detalleTrabajoDenuncia(this.parametro).subscribe(param => {
        if (param) {
          this.trabajo = param
          console.log(param);

        }

      });
    }

    if (this.parametro >= 1) {
      // original      this.TrabajoService.ImagenTrabajoDenuncia(this.parametro).subscribe(param => this.pruebas = param);
      this.TrabajoService.ImagenTrabajoDenuncia(this.parametro).subscribe(param => {
        if (param) {
          this.pruebas = param
          console.log("Pruebas fotos"+param);

        }
        //console.log("Datos Trabajo trabajo dtalle denuncia" + this.pruebas);

      });

    }
  }

    
  //  original if (this.parametro >= 1) {
  //    this.TrabajoService.ImagenTrabajoDenuncia(this.parametro).subscribe(param => this.pruebas = param);

  //  }
  //  console.log("Datos Trabajo trabajo dtalle denuncia" + this.trabajo);

  //}

  clickMethod() {
    alert("La denuncia se Derivó y Priorizó correctamente.");
    //Luego de presionar click debe redireccionar al home
  }
  volver() {
    this.router.navigate(["/tabla-denuncia"]);
  }

  registrarTrabajo() {
    this.router.navigate(["/trabajo-form-generar", this.parametro]);
  }




}



