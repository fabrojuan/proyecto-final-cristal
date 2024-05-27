import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Area } from 'src/app/modelos_Interfaces/Area';
import { AreasService } from 'src/app/services/areas.service';
import { ReclamoService } from 'src/app/services/reclamo.service';
import { SugerenciaService } from 'src/app/services/sugerencia.service';
import { VecinoService } from 'src/app/services/vecino.service';

@Component({
  selector: 'app-reclamo-form-generar-sugerencia',
  templateUrl: './reclamo-form-generar-sugerencia.component.html',
  styleUrls: ['./reclamo-form-generar-sugerencia.component.css']
})
export class ReclamoFormGenerarSugerenciaComponent implements OnInit {

  TiposReclamo: any;
  foto1: any;
  foto2: any;
  Reclamo: FormGroup;
  @ViewChild('fileUploader1') fileUploader1: ElementRef | undefined;
  @ViewChild('fileUploader2') fileUploader2: ElementRef | undefined;
  isFormSubmitted: boolean=false;
  mensajeUsuario:String = "";
  mostrarMensajeUsuario:boolean = false;
  esMensajeOk:boolean = true;
  areas: Area[] = []; 
  idSugerenciaOrigen?:number;

  constructor(private reclamoservice: ReclamoService, 
              private vecinoService: VecinoService,
              private _areasService: AreasService,
              private _activatedRouter: ActivatedRoute,
              private sugerenciaService: SugerenciaService
  ) {
    this.Reclamo = new FormGroup(
      {
        "codTipoReclamo": new FormControl("", [Validators.required]),
        "descripcion": new FormControl("", [Validators.required, Validators.maxLength(200)]),
        "calle": new FormControl("", [Validators.required, Validators.maxLength(50)]),
        "entreCalles": new FormControl("", [Validators.required, Validators.maxLength(50)]),
        "altura": new FormControl("", [Validators.required, Validators.maxLength(6)]),
        "foto1": new FormControl(""),
        "foto2": new FormControl(""),
        "nomApeVecino": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "mailVecino": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),
        "telefonoVecino": new FormControl("", [Validators.required, Validators.maxLength(50)]),
        "nroArea": new FormControl(1)
      }
    );

    this._activatedRouter.params.subscribe(params => {
      this.idSugerenciaOrigen = params['id_sugerencia'];

      //this.nroReclamo = params['id_sugerencia'];
      //this.reclamoService.getReclamo(this.nroReclamo).subscribe(datosReclamo => {
      //  console.log(datosReclamo);
      //  this.reclamo = datosReclamo;
      //});
    });

  }

  ngOnInit() {
    this.reclamoservice.getTipoReclamo().subscribe(data => this.TiposReclamo = data);

    this.foto1 = "";
    this.foto2 = "";

    this._areasService.getAreas().subscribe(
      data => {
        this.areas = data;
      }
    );
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
        this.isFormSubmitted = false;
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

  get descripcionNoValido() {
    return this.isFormSubmitted && this.Reclamo.controls.descripcion.errors;
  }


}
