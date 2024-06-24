import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from 'src/app/services/toast.service';
import { VecinoService } from 'src/app/services/vecino.service';

@Component({
  selector: 'app-vecino-nueva-contrasenia',
  templateUrl: './vecino-nueva-contrasenia.component.html',
  styleUrls: ['./vecino-nueva-contrasenia.component.css']
})
export class VecinoNuevaContraseniaComponent implements OnInit {

  urlBase: string = "";
  vecino: UntypedFormGroup;
  isFormSubmitted: boolean=false;
  uuid: string = "";

  constructor(private vecinoService: VecinoService, private router: Router, @Inject('BASE_URL') baseUrl: string,
              public _toastService: ToastService, private _activatedRouter: ActivatedRoute) {

    this.urlBase = baseUrl;
    this.vecino = new UntypedFormGroup({
      'nuevaContrasenia': new UntypedFormControl("", Validators.required),
      'nuevaContrasenia2': new UntypedFormControl("", Validators.required),
      'uuid': new UntypedFormControl("")
    });

    this._activatedRouter.params.subscribe(params => {
      this.uuid = params['uuid'];

      this.vecinoService.validarRecuperacionCuenta(this.uuid).subscribe(resp => {
      }, 
      error => {
        this.router.navigate(["/pagina-no-encontrada"]);        
      });

      this.vecino.controls.uuid.setValue(this.uuid);

    });

  }

  ngOnInit() { }
  
  login() {

    this.isFormSubmitted = true;

    if (this.vecino.invalid) {
      Object.values(this.vecino.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    }

    if (this.vecino.controls.nuevaContrasenia.value != this.vecino.controls.nuevaContrasenia2.value) {
      return;
    }

    this.vecinoService.resetearContrasenia(this.vecino.value)
      .subscribe(res => {
        this._toastService.showOk("La contraseña se cambió con éxito");
        this.vecinoService.guardarToken(res);
        this.router.navigate(["/bienvenida-vecino"]);   

      }, error => {
        this._toastService.showError("Ocurrió un error y no se pudo realizar la acción");
      });
    
  }

  get nuevaContraseniaNoValido() {
    return this.isFormSubmitted && this.vecino.controls.nuevaContrasenia.errors;
  }

  get nuevaContrasenia2NoValido() {
    return this.isFormSubmitted && 
      (this.vecino.controls.nuevaContrasenia2.errors || this.vecino.controls.nuevaContrasenia.value != this.vecino.controls.nuevaContrasenia2.value);
  }


}
