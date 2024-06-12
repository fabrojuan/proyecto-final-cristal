import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { VecinoService } from '../../services/vecino.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'reclamo-form-generar',
  templateUrl: './reclamo-form-generar.component.html',
  styleUrls: ['./reclamo-form-generar.component.css']
})
export class ReclamoFormGenerarComponent implements OnInit {
  TiposReclamo: any;
  foto1: any;
  foto2: any;
  isFormSubmitted: boolean=false;
  Reclamo: UntypedFormGroup;
  @ViewChild('fileUploader1') fileUploader1: ElementRef | undefined;
  @ViewChild('fileUploader2') fileUploader2: ElementRef | undefined;
  mensajeUsuario: string | undefined;
  mostrarMensajeUsuario: boolean | undefined;
  esMensajeOk: boolean | undefined;

  constructor(private reclamoservice: ReclamoService, private vecinoService: VecinoService,
              public _toastService: ToastService  ) {
    this.Reclamo = new UntypedFormGroup(
      {
        "Nro_Reclamo": new UntypedFormControl("0"),
        "codTipoReclamo": new UntypedFormControl("", [Validators.required]),
        "Descripcion": new UntypedFormControl("", [Validators.required, Validators.maxLength(200)]),
        "Calle": new UntypedFormControl("", [Validators.required, Validators.maxLength(50)]),
        "entreCalles": new UntypedFormControl("", [Validators.required, Validators.maxLength(50)]),
        "Altura": new UntypedFormControl("", [Validators.required, Validators.maxLength(6)]),
        "foto1": new UntypedFormControl(""),
        "foto2": new UntypedFormControl(""),
        "interno": new UntypedFormControl("N")
      }
    );
  }

  ngOnInit() {
    this.reclamoservice.getTipoReclamo().subscribe(data => this.TiposReclamo = data);

    this.foto1 = "";
    this.foto2 = "";
  }

  guardarDatos() {

    this.isFormSubmitted = true;
    this.mensajeUsuario = "";
    this.mostrarMensajeUsuario = false;
    this.esMensajeOk = true;

    if (this.Reclamo.invalid) {
      Object.values(this.Reclamo.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    }

    //Seteo la foto antes de guardarla
    this.Reclamo.controls["foto1"].setValue(this.foto1); 
    this.Reclamo.controls["foto2"].setValue(this.foto2);

    var nroReclamoGenerado: number = 0;

    this.reclamoservice.agregarReclamo(this.Reclamo.value).subscribe(
      data => {
        nroReclamoGenerado = data.nroReclamo;
      },
      error => {
        this.mostrarMensajeError(error.error);
      },
      () => {
        this.limpiarFormulario();
        this.mostrarMensajeOk(`Se registró con éxito el requerimiento nro: ${nroReclamoGenerado}`);
      }
    );
  }

  mostrarMensajeError(mensaje:string) {
    this.mensajeUsuario = mensaje;
    this.mostrarMensajeUsuario = true;
    this.esMensajeOk = false;
  }

  mostrarMensajeOk(mensaje:string) {
    this.mensajeUsuario = mensaje;
    this.mostrarMensajeUsuario = true;
    this.esMensajeOk = true;
  }

  changeFoto1(event: any) {
    if (event.target.files && event.target.files[0]) {
      const fotoSeleccionada: File = event.target.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(fotoSeleccionada);
      reader.onload = () => {
        this.foto1 = reader.result;
      };
    }
    else {
      this.foto1 = "";
    }
  }

  changeFoto2(event: any) {
    if (event.target.files && event.target.files[0]) {
      const fotoSeleccionada: File = event.target.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(fotoSeleccionada);
      reader.onload = () => {
        this.foto2 = reader.result;
      };
    }
    else {
      this.foto2 = "";
    }
  }

  cancelar() {
    this.isFormSubmitted = false;
    this.limpiarFormulario();
  }

  private limpiarFormulario() {
    this.isFormSubmitted = false;
    this.Reclamo.reset();

    this.foto1 = "";
    this.foto2 = "";

    if (this.fileUploader1) {
      this.fileUploader1.nativeElement.value = null;
    }

    if (this.fileUploader2) {
      this.fileUploader2.nativeElement.value = null;
    }
  }

  get codTipoReclamoNoValido() {
    return this.isFormSubmitted && this.Reclamo.controls.codTipoReclamo.errors;
  }

  get calleNoValido() {
    return this.isFormSubmitted && this.Reclamo.controls.Calle.errors;
  }

  get alturaNoValido() {
    return this.isFormSubmitted && this.Reclamo.controls.Altura.errors;
  }

  get entreCallesNoValido() {
    return this.isFormSubmitted && this.Reclamo.controls.entreCalles.errors;
  }

  get descripcionNoValido() {
    return this.isFormSubmitted && this.Reclamo.controls.Descripcion.errors;
  }

}







