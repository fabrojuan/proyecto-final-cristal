import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VecinoService } from '../../services/vecino.service';


@Component({
  selector: 'login-vecino',
  templateUrl: './login-vecino.component.html',
  styleUrls: ['./login-vecino.component.css']
})
export class LoginVecinoComponent implements OnInit {
  urlBase: string = "";
  vecino: FormGroup;
  error: boolean = false;
  constructor(private vecinoService: VecinoService, private router: Router, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;
    this.vecino = new FormGroup({
      'NombreUser': new FormControl("", Validators.required),
      'Contrasenia': new FormControl("", Validators.required)
    });
  }

  ngOnInit() {

  }
  login() {
    if (this.vecino.valid == true) {
      this.vecinoService.login(this.vecino.value).subscribe(res => {
        if (res.idVecino == 0 || res.idVecino == "") {
          this.error = true;
          this.router.navigate(["/login-vecino"]);
        }
        else {
          //Esta Ok
          this.error = false;
          //this.router.navigate(["/bienvenida"]);login
          window.location.href = this.urlBase + "bienvenida-vecino";
        }
        console.log(res);

      });
    }
  }

}
