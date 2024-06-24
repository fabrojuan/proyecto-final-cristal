import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/services/toast.service';
import { VecinoService } from 'src/app/services/vecino.service';

@Component({
  selector: 'app-vecino-olvido-contrasenia',
  templateUrl: './vecino-olvido-contrasenia.component.html',
  styleUrls: ['./vecino-olvido-contrasenia.component.css']
})
export class VecinoOlvidoContraseniaComponent implements OnInit {

  urlBase: string = "";
  vecino: UntypedFormGroup;
  isFormSubmitted: boolean=false;

  constructor(private vecinoService: VecinoService, private router: Router, @Inject('BASE_URL') baseUrl: string,
              public _toastService: ToastService) {

    this.urlBase = baseUrl;
    this.vecino = new UntypedFormGroup({
      'usuarioNombre': new UntypedFormControl("", Validators.required)
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

    

    this.vecinoService.recuperarCuenta(this.vecino.controls.usuarioNombre.value).subscribe(res => {
      this._toastService.showOk("Se enviará un correo a la dirección de email. Por favor siga las instrucciones");
    }, 
    error => {
      console.log(error);
      this._toastService.showError(error.error);
    });

    // this.vecinoService.login(this.vecino.value).subscribe(res => {
    //   if (res.idVecino == 0 || res.idVecino == "") {
    //     this._toastService.showError("Usuario y/o contraseña no válido");
    //   }
    //   else {
    //     window.location.href = this.urlBase + "bienvenida-vecino";
    //   }

    // }, error => {
    //   console.log(error);
    //   this._toastService.showError("Email y/o contraseńa no válidos");
    // });
    
  }

  get usuarioNoValido() {
    return this.isFormSubmitted && this.vecino.controls.usuarioNombre.errors;
  }

}
