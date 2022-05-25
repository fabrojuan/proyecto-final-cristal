import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  urlBase: string = "";
  usuario: FormGroup;
  error: boolean = false;
  constructor(private usuarioService: UsuarioService, private router: Router, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;
    this.usuario = new FormGroup({
      //Por ahora desojo el validators required solo para pass luego agregar en el usuario  Validators.required
      'NombreUser': new FormControl("", Validators.required),
      'Contrasenia': new FormControl("", Validators.required)
    });
  }

  ngOnInit() {

  }
  login() {
    if (this.usuario.valid == true) {
      this.usuarioService.login(this.usuario.value).subscribe(res => {
        if (res.idUsuario == 0 || res.idUsuario == "") {
          this.error = true;
          this.router.navigate(["/login"]);
        }
        else {
          //Esta Ok
          this.error = false;
          //this.router.navigate(["/bienvenida"]);login
          window.location.href = this.urlBase + "bienvenida";
        }
        console.log(res);

      });
    }
  }

}
