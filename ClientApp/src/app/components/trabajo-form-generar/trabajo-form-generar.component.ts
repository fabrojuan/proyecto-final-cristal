import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';
import { DenunciaService } from '../../services/denuncia.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'trabajo-form-generar',
  templateUrl: './trabajo-form-generar.component.html',
  styleUrls: ['./trabajo-form-generar.component.css']
})
export class TrabajoFormGenerarComponent implements OnInit {
  Trabajo: UntypedFormGroup;
  Empleados: any;
  Prioridades: any;
  parametro: any;
  NroDenuncia: any;
  foto: any;
  foto2: any;
  foto3: any;
  notificacionMail: any;
  resultadoGuardadoModal: any = "";
  @ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any> | undefined;
  tituloModal: string = "";  //estas cuatro lineas son del modal de accion sobre el formulario.
  redirectModal: number = 0;


  constructor(private TrabajoService: TrabajoService, private denunciaService: DenunciaService, private modalService: NgbModal, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"];
      if (this.parametro >= 1) {
        console.log(this.parametro);
      } else {
        this.NroDenuncia = 0;
      }
    });

    this.Trabajo = new UntypedFormGroup(
      {
        "Id_Usuario": new UntypedFormControl("0"),
        "Descripcion": new UntypedFormControl("", [Validators.required, Validators.minLength(20), Validators.maxLength(2500)]),
        "Nro_Prioridad": new UntypedFormControl("3"),
        "estado_Denuncia": new UntypedFormControl(""),
        "nro_Denuncia": new UntypedFormControl("0"),
        "legajoActual": new UntypedFormControl("0"),
        //"Mail": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),
        "foto": new UntypedFormControl(""),
        "foto2": new UntypedFormControl(""),
        "foto3": new UntypedFormControl(""),
        "ApellidoEmpleado": new UntypedFormControl("")

      });
  }

  ngOnInit() {
    this.foto = "";
    this.foto2 = "";
    this.foto3 = "";
    this.TrabajoService.getUsuario().subscribe(data => this.Empleados = data);
    this.TrabajoService.getPrioridad().subscribe(data => this.Prioridades = data);
    if (this.parametro >= 1) {
      this.TrabajoService.RecuperarDenuncia(this.parametro).subscribe(param => {
        this.Trabajo.controls["Id_Usuario"].setValue(param.idUsuario); //este es elqueviene seleccionado previo si se quiere derivar debe seleccionarse el valolr.
        this.Trabajo.controls["legajoActual"].setValue(param.idUsuario);  //este es el empleado que esta logueado actualmente .
        this.Trabajo.controls["estado_Denuncia"].setValue(param.estado_Denuncia);
        this.Trabajo.controls["nro_Denuncia"].setValue(param.nro_Denuncia);
      });
    } else {
    }
  }
  //Aqui vamos a leer el archivo
  changeFoto() {
    var file = (<HTMLInputElement>document.getElementById("fupFoto")).files?.[0] || new Blob(['test text'], { type: 'text/plain' });
    var fileReader = new FileReader();

    fileReader.onloadend = () => {   //Uso el Arrowfunction sino me marca error con foto.

      this.foto = fileReader.result;
    }
    fileReader.readAsDataURL(file);
    // Foto 2
    var file2 = (<HTMLInputElement>document.getElementById("fupFoto2")).files?.[0] || new Blob(['test text'], { type: 'text/plain' });
    var fileReader2 = new FileReader();

    fileReader2.onloadend = () => {   //Uso el Arrowfunction sino me marca error con foto.

      this.foto2 = fileReader2.result;
    }
    fileReader2.readAsDataURL(file2);
    //Foto3
    var file3 = (<HTMLInputElement>document.getElementById("fupFoto3")).files?.[0] || new Blob(['test text'], { type: 'text/plain' });
    var fileReader3 = new FileReader();

    fileReader3.onloadend = () => {   //Uso el Arrowfunction sino me marca error con foto.

      this.foto3 = fileReader3.result;
    }
    fileReader3.readAsDataURL(file3);
  }

  //En guardar datos si la denuncia no se deriva a nadie se carga el usuario que está logueado, sino se asignara la denuncia al id correspondiente que se seleccione en el combobx
  guardarDatos() {
    if (this.Trabajo.valid == true) {
      if (this.foto != "data:text/plain;base64,dGVzdCB0ZXh0")
        this.Trabajo.controls["foto"].setValue(this.foto); //Seteo la foto antes de guardarla
    if (this.foto2 != "data:text/plain;base64,dGVzdCB0ZXh0")
      this.Trabajo.controls["foto2"].setValue(this.foto2); //Seteo la foto antes de guardarla
    if (this.foto3 != "data:text/plain;base64,dGVzdCB0ZXh0")
      this.Trabajo.controls["foto3"].setValue(this.foto3); //Seteo la foto antes de guardarla
    
      this.TrabajoService.GuardarTrabajo(this.Trabajo.value).subscribe(data => {
        if (data) {
          this.tituloModal = "Registro de Trabajo";
          this.resultadoGuardadoModal = "Se añadió la actividad correctamente.";

        }
        else { this.tituloModal = "ERROR!";
          this.resultadoGuardadoModal = "No se ha podido registrar el trabajo genere un ticket con el error en nuestra pestaña de problemas";
        }
        });


      this.modalService.open(this.myModalInfo);

      if (this.notificacionMail == 1) {
        this.TrabajoService.notificar(this.Trabajo.value).subscribe(data => { });
      }
      this.router.navigate(["/tabla-denuncia"]);



    }
  }

  volver() {
    this.router.navigate(["/tabla-denuncia"]);
  }
  derivarAMesaEntrada() {
    this.tituloModal = "Denuncia derivada a mesa de entrada";
    this.resultadoGuardadoModal = "La denuncia se ha derivadado para que personal de mesa de Ayuda continue con el tratamiento";
    if (this.parametro >= 1) {
      this.denunciaService.devolverAMesa(this.Trabajo.value).subscribe(data => { })
      this.modalService.open(this.myModalInfo);
      this.router.navigate(["/tabla-denuncia"]);
    }
  }

  solucionarDenuncia() {
    this.tituloModal = "Denuncia en proceso de Solucion";
    this.resultadoGuardadoModal = "La denuncia se ha etiquetado para que sea finalizada por personal de mesa de Ayuda";
    if (this.parametro >= 1) {
      this.denunciaService.solucionarDenuncia(this.Trabajo.value).subscribe(data => { })
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
  verCheck() {
    var seleccionados = "";
    var noseleccionados = "";
    var checks = document.getElementsByClassName("check");
    var check: any;
    //this.suma_actual = 0;
    for (var i = 0; i < checks.length; i++) {
      check = checks[i];
      if (check.checked == true) {
        this.notificacionMail = 1;
        //  seleccionados += check.name; //Saco el id de la paginas sleeccionadas en el html hay una prop que se llama name=pagina.idPagina
        // seleccionados += "-"; //separo los id de cada uno con un guion.
        // this.suma_actual = this.suma_actual + parseInt(check.value);
      }
      else {
        if (check.checked == false) {
          // noseleccionados += check.name;
          // noseleccionados += "-";
        }
      }
    }

  }

}
