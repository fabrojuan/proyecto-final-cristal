import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/services/toast.service';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  urlBase: string = "";
  usuario: UntypedFormGroup;
  error: boolean = false;
  isFormSubmitted: boolean=false;

  constructor(private usuarioService: UsuarioService, private router: Router, @Inject('BASE_URL') baseUrl: string,
              public _toastService: ToastService) {
    this.urlBase = baseUrl;
    this.usuario = new UntypedFormGroup({
      //Por ahora desojo el validators required solo para pass luego agregar en el usuario  Validators.required
      'usuarioNombre': new UntypedFormControl("", Validators.required),
      'usuarioContrasenia': new UntypedFormControl("", Validators.required)
    });
  }

  ngOnInit() {

  }

  login() {

    this.isFormSubmitted = true;

    if (this.usuario.invalid) {
      Object.values(this.usuario.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    }

    this.usuarioService.login(this.usuario.value).subscribe(res => {
      if (res.idUsuario == 0 || res.idUsuario == "") {
        this._toastService.showError("Usuario y/o contraseńa no válidos");
        return;
      }
      else {
        //Esta Ok
        window.location.href = this.urlBase + "bienvenida";
      }
    }, error => {
      this._toastService.showError("Usuario y/o contraseńa no válidos");
    });
  
  }

  get usuarioNoValido() {
    return this.isFormSubmitted && this.usuario.controls.usuarioNombre.errors;
  }

  get contraseniaNoValido() {
    return this.isFormSubmitted && this.usuario.controls.usuarioContrasenia.errors;
  }

}
