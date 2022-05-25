import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';
import { DenunciaService } from '../../services/denuncia.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'trabajo-form-generar',
  templateUrl: './trabajo-form-generar.component.html',
  styleUrls: ['./trabajo-form-generar.component.css']
})
export class TrabajoFormGenerarComponent implements OnInit {
  Trabajo: FormGroup;
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

  constructor(private TrabajoService: TrabajoService, private denunciaService: DenunciaService, private modalService: NgbModal, private router: Router, private activatedRoute: ActivatedRoute) {
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
        "Id_Usuario": new FormControl("0"),
        "Descripcion": new FormControl("", [Validators.required, Validators.minLength(50), Validators.maxLength(2500)]),
        "Nro_Prioridad": new FormControl("3"),
        "estado_Denuncia": new FormControl(""),
        "nro_Denuncia": new FormControl("0"),
        "legajoActual": new FormControl("0"),
        //"Mail": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),
        "foto": new FormControl(""),
        "foto2": new FormControl(""),
        "foto3": new FormControl("")

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

      this.Trabajo.controls["foto"].setValue(this.foto); //Seteo la foto antes de guardarla
      this.Trabajo.controls["foto2"].setValue(this.foto2); //Seteo la foto antes de guardarla
      this.Trabajo.controls["foto3"].setValue(this.foto3); //Seteo la foto antes de guardarla

      this.TrabajoService.GuardarTrabajo(this.Trabajo.value).subscribe(data => { 
      if (data) {
        console.log(data);
        this.resultadoGuardadoModal = "Se añadió la actividad correctamente.";

      }
      else
        this.resultadoGuardadoModal = "No se ha podido registrar el trabajo genere un ticket con el error en nuestra pestaña de problemas";
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
    if (this.parametro >= 1) {
      this.denunciaService.devolverAMesa(this.Trabajo.value).subscribe(data => { })
      alert("La Denuncia ha sido derivada a Mesa de Entrada");
      this.router.navigate(["/tabla-denuncia"]);
    }
  }

  solucionarDenuncia() {
    if (this.parametro >= 1) {
      this.denunciaService.solucionarDenuncia(this.Trabajo.value).subscribe(data => { })
      alert("Denuncia Finalizada");
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
