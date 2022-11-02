import { Component, OnInit } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-tipo-denuncia-form',
  templateUrl: './tipo-denuncia-form.component.html',
  styleUrls: ['./tipo-denuncia-form.component.css']
})
export class TipoDenunciaFormComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  modoUso: string = "ALTA";

  constructor(private _denunciaService: DenunciaService,
    private _activatedRouter: ActivatedRoute,
    private _router: Router,
    private _fb: FormBuilder,
    public _toastService: ToastService) {

    this.crearFormulario();

    this._activatedRouter.params.subscribe(params => {

      if (params['id'] == null) {
        this.modoUso = "ALTA";
        return;
      }

      this.modoUso = "EDICION";
      this._denunciaService.getTipoDenunciaByCodigo(params['id']).subscribe(
        data => {
          this.form.reset({
            codigo: data.cod_Tipo_Denuncia,
            nombre: data.nombre,
            descripcion: data.descripcion,
            tiempoMaxResolucion: data.tiempo_Max_Tratamiento,
            fechaAlta: data.fechaAlta,
            fechaModificacion: data.fechaModificacion,
            usuarioAlta: data.usuarioAlta,
            usuarioModificacion: data.usuarioModificacion
          });
        }
      );

    });
  }

  ngOnInit(): void {

  }

  volver(): void {
    this._router.navigate(['/tipo-denuncia-tabla']);
  }

  crearFormulario() {
    this.form = this._fb.group({
      codigo: [''],
      nombre: ['', [Validators.required, Validators.maxLength(90)]],
      descripcion: ['', [Validators.required, Validators.maxLength(250)]],
      tiempoMaxResolucion: ['0', [Validators.required, Validators.min(0)]],
      fechaAlta: [''],
      fechaModificacion: [''],
      usuarioAlta: [''],
      usuarioModificacion: ['']
    });
  }

  guardar() {
    if (this.form.invalid) {
      Object.values(this.form.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    }

    const tipoDenuncia: any = {
      cod_Tipo_Denuncia: Number(this.form.get("codigo")?.value),
      nombre: this.form.get("nombre")?.value,
      descripcion: this.form.get("descripcion")?.value,
      tiempo_Max_Tratamiento: this.form.get("tiempoMaxResolucion")?.value
    }

    if (this.modoUso == "ALTA") {
      this._denunciaService.agregarTipoDenuncia(tipoDenuncia).subscribe(
        data => {
        },
        error => {
          this._toastService.show(error.error, { classname: 'bg-danger text-light', delay: 5000 });
        },
        () => {
          this.form.reset();
          this._toastService.show('Registro creado con éxito.', { classname: 'bg-success text-light', delay: 5000 });
          this._router.navigate(['/tipo-denuncia-tabla']);
        }
      );
    }

    if (this.modoUso == "EDICION") {
      this._denunciaService.modificarTipoDenuncia(tipoDenuncia).subscribe(
        data => {
        },
        error => {
          this._toastService.show(error.error, { classname: 'bg-danger text-light', delay: 5000 });
        },
        () => {
          this.form.reset();
          this._toastService.show('Registro guardado con éxito.', { classname: 'bg-success text-light', delay: 5000 });
          this._router.navigate(['/tipo-denuncia-tabla']);
        }
      );
    }


  }

  get nombreNoValido() {
    return this.form.get('nombre')?.invalid && this.form.get('nombre')?.touched;
  }

  get descripcionNoValido() {
    return this.form.get('descripcion')?.invalid && this.form.get('descripcion')?.touched;
  }

  get tiempoMaxResolucionNoValido() {
    return this.form.get('tiempoMaxResolucion')?.invalid && this.form.get('tiempoMaxResolucion')?.touched;
  }

  get esModoEdicion() {
    return this.modoUso === "EDICION";
  }

}
