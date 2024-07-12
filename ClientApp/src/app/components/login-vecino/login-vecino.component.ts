import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VecinoService } from '../../services/vecino.service';
import { ToastService } from 'src/app/services/toast.service';


@Component({
  selector: 'login-vecino',
  templateUrl: './login-vecino.component.html',
  styleUrls: ['./login-vecino.component.css']
})
export class LoginVecinoComponent implements OnInit {
  urlBase: string = "";
  vecino: UntypedFormGroup;
  isFormSubmitted: boolean=false;

  constructor(private vecinoService: VecinoService, private router: Router, @Inject('BASE_URL') baseUrl: string,
              public _toastService: ToastService) {

    this.urlBase = baseUrl;
    this.vecino = new UntypedFormGroup({
      'usuarioNombre': new UntypedFormControl("", Validators.required),
      'usuarioContrasenia': new UntypedFormControl("", Validators.required)
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

    this.vecinoService.login(this.vecino.value).subscribe(res => {
      if (res.idVecino == 0 || res.idVecino == "") {
        this._toastService.showError("Usuario y/o contraseña no válidos");
      }
      else {
        window.location.href = this.urlBase /*+ "bienvenida-vecino"*/;
      }

    }, error => {
      console.log(error);
      this._toastService.showError("Usuario y/o contraseńa no válidos");
    });
    
  }

  get usuarioNoValido() {
    return this.isFormSubmitted && this.vecino.controls.usuarioNombre.errors;
  }

  get contraseniaNoValido() {
    return this.isFormSubmitted && this.vecino.controls.usuarioContrasenia.errors;
  }

}
