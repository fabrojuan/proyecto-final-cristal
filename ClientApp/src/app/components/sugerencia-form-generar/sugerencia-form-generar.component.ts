import { Component, OnInit } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { SugerenciaService } from '../../services/sugerencia.service';
import { Router, RouterModule } from '@angular/router';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'sugerencia-form-generar',
  templateUrl: './sugerencia-form-generar.component.html',
  styleUrls: ['./sugerencia-form-generar.component.css']
})
export class SugerenciaFormGenerarComponent implements OnInit {
  TiposReclamo: any;
  nombreVecino: any;
  idVecino: any;
  Sugerencia: UntypedFormGroup;
  isFormSubmitted: boolean = false;

  constructor(private sugerenciaservice: SugerenciaService, private router: Router,
              public _toastService: ToastService
  ) {
    this.Sugerencia = new UntypedFormGroup(
      {
        "Descripcion": new UntypedFormControl("", [Validators.required, Validators.minLength(50)]),

      }
    );
  }

  ngOnInit() {
    //this.sugerenciaservice.getTipoSugerencia().subscribe(data => this.TiposSugerencia = data);


  }
  guardarDatos() {

    this.isFormSubmitted = true;

    if (this.Sugerencia.invalid) {
      Object.values(this.Sugerencia.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    }

    this.sugerenciaservice.agregarSugerencia(this.Sugerencia.value).subscribe(data => {
      this._toastService.showOk("La sugerencia ha sido guardado con éxito");
      this.router.navigate(["/"]);
    }, error => {
      this._toastService.showError("Ocurrió un error y no se pudo guardar la sugerencia");
    });
      

  }
  
  clickMethod() {
    alert("La sugerencia se ha generada correctamente, agradecemos su compromiso para mejorar ");
  }

  get descripcionNoValido() {
    return this.isFormSubmitted && this.Sugerencia.controls.Descripcion.errors;
  }

  cancelar() {
    this.isFormSubmitted = false;
    this.router.navigate(["/"]);
  }


}
