import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';
import { PruebaGraficaService } from '../../services/prueba-grafica.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DenunciaService } from '../../services/denuncia.service';

@Component({
  selector: 'denuncia-detalle',
  templateUrl: './denuncia-detalle.component.html',
  styleUrls: ['./denuncia-detalle.component.css']
})
export class DenunciaDetalleComponent implements OnInit {
  Trabajo: FormGroup;
  Empleados: any;
  Prioridades: any;
  pruebas: any;
  parametro: any;
  NroDenuncia: any;


  constructor(private TrabajoService: TrabajoService, private denunciaService: DenunciaService, private PruebaGraficaService: PruebaGraficaService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"];
      if (this.parametro >= 1) {
        console.log(this.parametro);
      } else {
        this.NroDenuncia = 0;
      }
    });

    this.Trabajo = new FormGroup(
      {
        "idUsuario": new FormControl("0"),
        "Descripcion": new FormControl("", [Validators.required, Validators.minLength(50), Validators.maxLength(2500)]),
        "Nro_Prioridad": new FormControl("0", [Validators.required, Validators.pattern("[^4]")]),
        "estado_Denuncia": new FormControl(""),
        "nro_Denuncia": new FormControl("0"),
        "legajoActual": new FormControl("0"),
        "nombre_Infractor": new FormControl("0"),
        "tipo_Denuncia": new FormControl(""),
        //"Mail": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),
        "altura": new FormControl("0"),
        "calle": new FormControl(""),
        "entre_Calles": new FormControl("")

      });
  }

  ngOnInit() {
    this.TrabajoService.getUsuario().subscribe(data => this.Empleados = data);
    this.TrabajoService.getPrioridad().subscribe(data => this.Prioridades = data);

    if (this.parametro >= 1) {
      this.TrabajoService.detalleDenuncia(this.parametro).subscribe(param => {
        this.Trabajo.controls["idUsuario"].setValue(param.idUsuario); //este es el empleado que esta logueado actualmente si se quiere derivar debe seleccionarse el valolr.
        this.Trabajo.controls["legajoActual"].setValue(param.idUsuario);  //este es el empleado que esta logueado actualmente .
        this.Trabajo.controls["estado_Denuncia"].setValue(param.estado_Denuncia);
        this.Trabajo.controls["nro_Denuncia"].setValue(param.nro_Denuncia);
        this.Trabajo.controls["Descripcion"].setValue(param.descripcion);
        this.Trabajo.controls["Nro_Prioridad"].setValue(param.nro_Prioridad);
        this.Trabajo.controls["nombre_Infractor"].setValue(param.nombre_Infractor);
        this.Trabajo.controls["tipo_Denuncia"].setValue(param.tipo_Denuncia);
        this.Trabajo.controls["altura"].setValue(param.altura);
        this.Trabajo.controls["calle"].setValue(param.calle);
        this.Trabajo.controls["entre_Calles"].setValue(param.entre_Calles);

        this.NroDenuncia = param.nro_Denuncia;
        this.PruebaGraficaService.ListarPruebasIniciales(this.parametro).subscribe(data => this.pruebas = data);
      });

    } else {
    }
  }

  //En guardar datos si la denuncia no se deriva a nadie se carga el usuario que está logueado, sino se asignara la denuncia al id correspondiente que se seleccione en el combobx
  guardarDatos() {
    if (this.Trabajo.valid == true) {

      // Aca preguntar si el combo esta activado para derivar.
      //   Y para realizar todo el tratamiento de los estados de la denuncia.
      this.denunciaService.DerivaPriorizaDenuncia(this.Trabajo.value).subscribe(data => { });
    }
  }
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
