import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { VecinoService } from '../../services/vecino.service';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-reclamo-form-generar-empleado',
  templateUrl: './reclamo-form-generar-empleado.component.html',
  styleUrls: ['./reclamo-form-generar-empleado.component.css']
})
export class ReclamoFormGenerarEmpleadoComponent implements OnInit {

  TiposReclamo: any;
  foto1: any;
  foto2: any;
  Reclamo: FormGroup;
  @ViewChild('fileUploader1') fileUploader1: ElementRef | undefined;
  @ViewChild('fileUploader2') fileUploader2: ElementRef | undefined;

  constructor(private reclamoservice: ReclamoService, private vecinoService: VecinoService,
              public _toastService: ToastService  ) {
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
        "telefonoVecino": new FormControl("", [Validators.required, Validators.maxLength(50)])
      }
    );
  }

  ngOnInit() {
    this.reclamoservice.getTipoReclamo().subscribe(data => this.TiposReclamo = data);

    this.foto1 = "";
    this.foto2 = "";
  }

  guardarDatos() {

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
        this._toastService.show(error.error, { classname: 'bg-danger text-light', delay: 5000 });
      },
      () => {
        this.limpiarFormulario();
        this._toastService.show(`Se registró con éxito el reclamo nro: ${nroReclamoGenerado}`, { classname: 'bg-success text-light', delay: 5000 });
      }
    );
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
    return this.Reclamo.get('codTipoReclamo')?.invalid && this.Reclamo.get('codTipoReclamo')?.touched;
  }

  get calleNoValido() {
    return this.Reclamo.get('calle')?.invalid && this.Reclamo.get('calle')?.touched;
  }

  get alturaNoValido() {
    return this.Reclamo.get('altura')?.invalid && this.Reclamo.get('altura')?.touched;
  }

  get entreCallesNoValido() {
    return this.Reclamo.get('entreCalles')?.invalid && this.Reclamo.get('entreCalles')?.touched;
  }

  get descripcionNoValido() {
    return this.Reclamo.get('descripcion')?.invalid && this.Reclamo.get('descripcion')?.touched;
  }

  get nomApeVecinoNoValido() {
    return this.Reclamo.get('nomApeVecino')?.invalid && this.Reclamo.get('nomApeVecino')?.touched;
  }

  get mailVecinoNoValido() {
    return this.Reclamo.get('mailVecino')?.invalid && this.Reclamo.get('mailVecino')?.touched;
  }

  get telefonoVecinoNoValido() {
    return this.Reclamo.get('telefonoVecino')?.invalid && this.Reclamo.get('telefonoVecino')?.touched;
  }

}
