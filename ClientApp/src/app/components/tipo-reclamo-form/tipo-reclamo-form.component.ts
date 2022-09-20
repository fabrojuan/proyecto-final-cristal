import { Component, OnInit } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-tipo-reclamo-form',
  templateUrl: './tipo-reclamo-form.component.html',
  styleUrls: ['./tipo-reclamo-form.component.css']
})
export class TipoReclamoFormComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  modoUso: string = "ALTA";

  constructor(private _reclamoService: ReclamoService,
    private _activatedRouter: ActivatedRoute,
    private _router: Router,
    private _fb: FormBuilder) {

    this.crearFormulario();

    this._activatedRouter.params.subscribe(params => {

      if (params['id'] == null) {
        this.modoUso = "ALTA";
        return;
      }

      this.modoUso = "EDICION";
      this._reclamoService.getTipoReclamoByCodigo(params['id']).subscribe(
        data => {
          this.form.reset({
            codigo: data.cod_Tipo_Reclamo,
            nombre: data.nombre,
            descripcion: data.descripcion,
            tiempoMaxResolucion: data.tiempo_Max_Tratamiento,
            fechaAlta: data.fechaAlta,
            fechaModificacion: data.fechaModificacion
          });
        }
      );     

    });
  }

  ngOnInit(): void {
    
  }

  volver(): void {
    this._router.navigate(['/tipo-reclamo-tabla']);
  }

  crearFormulario() {
    this.form = this._fb.group({
      codigo: [''],
      nombre: ['', [Validators.required, Validators.maxLength(90)]],
      descripcion: ['', [Validators.required, Validators.maxLength(250)]],
      tiempoMaxResolucion: ['0', [Validators.required, Validators.min(0)]],
      fechaAlta: [''],
      fechaModificacion: ['']
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

    const tipoReclamo: any = {
      cod_Tipo_Reclamo: Number(this.form.get("codigo")?.value),
      nombre: this.form.get("nombre")?.value,
      descripcion: this.form.get("descripcion")?.value,
      tiempo_Max_Tratamiento: this.form.get("tiempoMaxResolucion")?.value
    }

    if (this.modoUso == "ALTA") {
      this._reclamoService.agregarTipoReclamo(tipoReclamo).subscribe(
        data => {
        },
        error => {
          alert(error.error);
        },
        () => {
          this.form.reset();
          this._router.navigate(['/tipo-reclamo-tabla']);
        }
      );
    }

    if (this.modoUso == "EDICION") {
      this._reclamoService.modificarTipoReclamo(tipoReclamo).subscribe(
        data => {
        },
        error => {
          alert(error.error);
        },
        () => {
          this.form.reset();
          this._router.navigate(['/tipo-reclamo-tabla']);
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
