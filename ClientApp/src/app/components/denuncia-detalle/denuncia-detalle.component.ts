import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';
import { PruebaGraficaService } from '../../services/prueba-grafica.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { DenunciaService } from '../../services/denuncia.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'denuncia-detalle',
  templateUrl: './denuncia-detalle.component.html',
  styleUrls: ['./denuncia-detalle.component.css']
})
export class DenunciaDetalleComponent implements OnInit {
  Trabajo: UntypedFormGroup;
  Empleados: any;
  Prioridades: any;
  pruebas: any;
  parametro: any;
  NroDenuncia: any;
  tituloModal: string = "";  //estas cuatro lineas son del modal de accion sobre el formulario.
  resultadoGuardadoModal: any = "";
  @ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any> | undefined;
  redirectModal: number = 0;

  onTouched: () => void = () => { };
  onChange: (value: any) => void = () => { };
  subscriptions: Subscription;

  constructor(private TrabajoService: TrabajoService, private denunciaService: DenunciaService, private modalService: NgbModal, private PruebaGraficaService: PruebaGraficaService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.subscriptions = new Subscription();
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"];
      if (this.parametro >= 1) {
        /*        console.log(this.parametro);*/
      } else {
        this.NroDenuncia = 0;
      }
    });

    this.Trabajo = new UntypedFormGroup(
      {
        "idUsuario": new UntypedFormControl("0"),
        "Descripcion": new UntypedFormControl("", [Validators.required, Validators.minLength(50), Validators.maxLength(2500)]),
        "Nro_Prioridad": new UntypedFormControl("0", [Validators.required, Validators.pattern("[^4]")]),
        "estado_Denuncia": new UntypedFormControl(""),
        "nro_Denuncia": new UntypedFormControl("0"),
        "legajoActual": new UntypedFormControl("0"),
        "nombre_Infractor": new UntypedFormControl("0"),
        "tipo_Denuncia": new UntypedFormControl(""),
        //"Mail": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),
        "altura": new UntypedFormControl("0"),
        "calle": new UntypedFormControl(""),
        "entre_Calles": new UntypedFormControl("")

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
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
  //En guardar datos si la denuncia no se deriva a nadie se carga el usuario que está logueado, sino se asignara la denuncia al id correspondiente que se seleccione en el combobx
  guardarDatos() {

    if (this.Trabajo.valid == true) {

      // Aca preguntar si el combo esta activado para derivar.
      //   Y para realizar todo el tratamiento de los estados de la denuncia.
      this.denunciaService.DerivaPriorizaDenuncia(this.Trabajo.value).subscribe(data => {
        if (data) {
          this.tituloModal = "Gestion Realizada!"
          this.resultadoGuardadoModal = "La denuncia se derivó al área correspondiente";
          this.redirectModal = 1;
        }
        else
          this.resultadoGuardadoModal = " No se cambio la Prioridad de la denuncia. solo se deriva";
        this.redirectModal = 0;
      });
      this.modalService.open(this.myModalInfo);
      this.router.navigate(["/tabla-denuncia"]);

    }
  }
  cerrarmodal() {
    this.modalService.dismissAll(this.myModalInfo);
    if (this.redirectModal > 0) {
      this.router.navigate(["/tabla-denuncia"]);
      this.tituloModal = "Se redirigira al Tablero de Denuncias"
      this.redirectModal = 0;
    }
    else { //resetear campos del modal ya que la persona esta registrada y seteamos lote.
      this.tituloModal = "No te olvides de generar el ticket con el error en el menu de problemas.";
      this.router.navigate(["/tabla-denuncia"]);
    }
  }

  volver() {
    //this.mostrarModal = false; // Ocultar el modal al navegar
    this.router.navigate(["/tabla-denuncia"]);
  }

  registrarTrabajo() {

    this.router.navigate(["/trabajo-form-generar", this.parametro]);
  }
}
