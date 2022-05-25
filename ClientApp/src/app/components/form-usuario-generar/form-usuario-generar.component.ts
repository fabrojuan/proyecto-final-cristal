import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'form-usuario-generar',
  templateUrl: './form-usuario-generar.component.html',
  styleUrls: ['./form-usuario-generar.component.css']
})
export class FormUsuarioGenerarComponent implements OnInit {
  Usuario: FormGroup;
  titulo: string = "";
  parametro: any;
  TiposRol: any;
  respuesta: any = 0;
  constructor(private usuarioService: UsuarioService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"]
      if (this.parametro >= 1) {
        this.titulo = "Editar";
      } else {
        this.titulo = "Añadir";
      }
    });
    this.Usuario = new FormGroup(
      {
        "IdUsuario": new FormControl("0"),
        "NombreUser": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "Contrasenia": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.minLength(8)]),
        "IdPersona": new FormControl("0"),
        "NombrePersona": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "Apellido": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "BHabilitado": new FormControl("1"),
        "Telefono": new FormControl("", [Validators.required, Validators.maxLength(50)]),
        "Mail": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),
        "Domicilio": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "Dni": new FormControl("", [Validators.required, Validators.maxLength(10)]),
        "Altura": new FormControl("", [Validators.required, Validators.maxLength(5)]),
        //"FechaNac": new FormControl("")
        "TiposRol": new FormControl("", [Validators.required])
      });
  }

  ngOnInit() {
    //Aqui recuperamos la info para luego editarla.
    // DEbo traer el combo de a quien derivar...
    this.usuarioService.getRol().subscribe(data => this.TiposRol = data);
    if (this.parametro >= 1) {
      this.usuarioService.RecuperarUsuario(this.parametro).subscribe(param => {

        this.Usuario.controls["IdUsuario"].setValue(param.idUsuario);
        this.Usuario.controls["NombreUser"].setValue(param.nombreUser);
        this.Usuario.controls["Contrasenia"].setValue(param.contrasenia);
        this.Usuario.controls["NombrePersona"].setValue(param.nombrePersona);
        this.Usuario.controls["Apellido"].setValue(param.apellido);
        this.Usuario.controls["Telefono"].setValue(param.telefono);
        this.Usuario.controls["Dni"].setValue(param.dni);
        this.Usuario.controls["Mail"].setValue(param.mail);
        this.Usuario.controls["Domicilio"].setValue(param.domicilio);
        this.Usuario.controls["Altura"].setValue(param.altura);
        this.Usuario.controls["TiposRol"].setValue(param.tiposRol);  // SACAR ROL PORQUE VA COMO VECINO POR DEFECTO
      });
    } else {
    }
  }
  guardarDatos() {
    if (this.Usuario.valid == true) {
      this.respuesta = this.usuarioService.GuardarUsuario(this.Usuario.value).subscribe(data => { });
      if (this.respuesta == 0) {
        console.log("No se guardo correcto hubo error")
      }
      else {
        console.log("Se guardo Joya!!!");
        this.router.navigate(["/usuario-tabla"]);

      }
    }
  }
  clickMethod() {
    alert("Se registró el usuario correctamente");
    //Luego de presionar click debe redireccionar al home

  }

  volver() {
    this.router.navigate(["/usuario-tabla"]);
  }



}
