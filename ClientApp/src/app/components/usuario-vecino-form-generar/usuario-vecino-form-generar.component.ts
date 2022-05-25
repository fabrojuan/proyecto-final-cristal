import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
//import { resolve } from 'url';

@Component({
  selector: 'usuario-vecino-form-generar',
  templateUrl: './usuario-vecino-form-generar.component.html',
  styleUrls: ['./usuario-vecino-form-generar.component.css']
})
export class UsuarioVecinoFormGenerarComponent implements OnInit {
  Usuario: FormGroup;
  titulo: string = "";
  parametro: any;
  respuesta: any = 0;
  yaExiste: boolean=false;

  constructor(private usuarioService: UsuarioService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"]
      if (this.parametro >= 1) {
        this.titulo = "Editar";
      } else {
        this.titulo = "Registrarse";
      }
    });
    this.Usuario = new FormGroup(
      {
        "IdVecino": new FormControl("0"),
        "NombreUser": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "Contrasenia": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.minLength(8), Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")]),  //pattern=""
        "Contrasenia2": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.minLength(8), this.validarContraseñas.bind(this)]),
        "IdPersona": new FormControl("0"),
        "NombrePersona": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "Apellido": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "BHabilitado": new FormControl("1"),
        "Telefono": new FormControl("", [Validators.required, Validators.maxLength(20), Validators.pattern("[0-9]{9,}")]),
        "Mail": new FormControl("", [Validators.required, Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"), Validators.maxLength(100), this.noRepetirMail.bind(this)]),
        "Domicilio": new FormControl("", [Validators.required, Validators.maxLength(100)]),        "Dni": new FormControl("", [Validators.required, Validators.maxLength(8), Validators.minLength(7), Validators.pattern("[0-9]{7,8}")]),
        "Altura": new FormControl("", [Validators.required, Validators.maxLength(5)]),
        "FechaNac": new FormControl("")
      }
    );
    //
  }

  ngOnInit() {

    if (this.parametro >= 1) {
      this.usuarioService.RecuperarUsuario(this.parametro).subscribe(param => {

        this.Usuario.controls["IdVecino"].setValue(param.idUsuario);
        this.Usuario.controls["NombreUser"].setValue(param.nombreUser);
        this.Usuario.controls["Contrasenia"].setValue(param.contrasenia);
        this.Usuario.controls["NombrePersona"].setValue(param.nombrePersona);
        this.Usuario.controls["Apellido"].setValue(param.apellido);
        this.Usuario.controls["Telefono"].setValue(param.telefono);
        this.Usuario.controls["Dni"].setValue(param.dni);
        this.Usuario.controls["Mail"].setValue(param.mail);
        this.Usuario.controls["Domicilio"].setValue(param.domicilio);
        this.Usuario.controls["Altura"].setValue(param.altura);
      });
    } else {
    }
  }
  guardarDatos() {
    if (this.Usuario.valid == true) {
      this.respuesta = this.usuarioService.GuardarVecino(this.Usuario.value).subscribe(data => { })
      if (this.respuesta == 0) {
        console.log("No se guardo correcto hubo error");
      }
      else {
        console.log("Se guardo Joya!!!");
        this.router.navigate(["/"]);
      }
    }
  }

  validarContraseñas(control: FormControl) {
    if (control.value != "" && control.value != null) {
      if (this.Usuario.controls["Contrasenia"].value != control.value) {
        return ({ noIguales: true })
      }
      else {
        return null;
      }

    }
    return null; //agregue este return validar si siguue funcionando ok

  }

  noRepetirMail(control: FormControl) {
    var promesa = new Promise((resolve, reject) => {
      if (control.value != "" && control.value != null) {
        this.usuarioService.validarCorreo(this.Usuario.controls["IdVecino"].value, control.value).subscribe(data => {
          if (data == 1) {
            resolve({ yaExiste: true });
          }
          else {
            resolve(null);
          }
        })

      }
      else
      {
        resolve({ yaExiste: true });
      }
    });
    return promesa;
  }

  clickMethod() {
    alert("Se registró el usuario correctamente");
    //Luego de presionar click debe redireccionar al home
  }

  volverHome() {
    this.router.navigate(["/"]);
  }



}
