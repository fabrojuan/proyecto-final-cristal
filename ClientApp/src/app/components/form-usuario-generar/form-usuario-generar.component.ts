import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { ToastService } from 'src/app/services/toast.service';
import moment from 'moment';
import { AreasService } from 'src/app/services/areas.service';
import { Area } from 'src/app/modelos_Interfaces/Area';

@Component({
  selector: 'form-usuario-generar',
  templateUrl: './form-usuario-generar.component.html',
  styleUrls: ['./form-usuario-generar.component.css']
})
export class FormUsuarioGenerarComponent implements OnInit {
  Usuario: UntypedFormGroup;
  titulo: string = "";
  parametro: any;
  TiposRol: any;
  respuesta: any = 0;
  isFormSubmitted: boolean = false;
  areas: Area[] = [];

  constructor(private usuarioService: UsuarioService, private router: Router, private activatedRoute: ActivatedRoute, 
              public _toastService: ToastService, private areaService: AreasService
  ) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"]
      if (this.parametro >= 1) {
        this.titulo = "Editar";
      } else {
        this.titulo = "Añadir";
      }
    });
    this.Usuario = new UntypedFormGroup(
      {
        "IdUsuario": new UntypedFormControl("0"),
        "NombreUser": new UntypedFormControl("", [Validators.required, Validators.maxLength(100)]),
        "Contrasenia": new UntypedFormControl("", [Validators.required, Validators.maxLength(100), Validators.minLength(8)]),
        "IdPersona": new UntypedFormControl("0"),
        "NombrePersona": new UntypedFormControl("", [Validators.required, Validators.maxLength(100)]),
        "Apellido": new UntypedFormControl("", [Validators.required, Validators.maxLength(100)]),
        "BHabilitado": new UntypedFormControl("1"),
        "Telefono": new UntypedFormControl("", [Validators.required, Validators.maxLength(50)]),
        "Mail": new UntypedFormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),
        "Domicilio": new UntypedFormControl("", [Validators.required, Validators.maxLength(100)]),
        "Dni": new UntypedFormControl("", [Validators.required, Validators.maxLength(10)]),
        "Altura": new UntypedFormControl("", [Validators.required, Validators.maxLength(5)]),
        "FechaNac": new UntypedFormControl("", [Validators.required]),
        "TiposRol": new UntypedFormControl("", [Validators.required]),
        "NroArea": new UntypedFormControl("", [Validators.required])
      });
  }

  ngOnInit() {

    this.usuarioService.listarRoles().subscribe(data => this.TiposRol = data);
    this.areaService.getAreas().subscribe(data => this.areas = data);

    if (this.parametro >= 1) {
      this.usuarioService.RecuperarUsuario(this.parametro).subscribe(param => {

        this.Usuario.controls["IdUsuario"].setValue(param.idUsuario);
        this.Usuario.controls["NombreUser"].setValue(param.nombreUser);

        this.Usuario.controls["Contrasenia"].setValue(param.contrasenia);
        this.Usuario.controls['Contrasenia'].setValidators([])
        this.Usuario.controls['Contrasenia'].updateValueAndValidity()

        this.Usuario.controls["NombrePersona"].setValue(param.nombrePersona);
        this.Usuario.controls["Apellido"].setValue(param.apellido);
        this.Usuario.controls["Telefono"].setValue(param.telefono);
        this.Usuario.controls["Dni"].setValue(param.dni);
        this.Usuario.controls["Mail"].setValue(param.mail);
        this.Usuario.controls["Domicilio"].setValue(param.domicilio);
        this.Usuario.controls["Altura"].setValue(param.altura);

        let fechaNac : Date = new Date(param.fechaNac);
        let formattedFechaNac = (moment(fechaNac)).format('YYYY-MM-DD');
        this.Usuario.controls["FechaNac"].setValue(formattedFechaNac);

        this.Usuario.controls["TiposRol"].setValue(param.tiposRol);  
        this.Usuario.controls["NroArea"].setValue(param.nroArea);
      });
    } else {
    }
  }
  guardarDatos() {

    this.isFormSubmitted = true;

    if (this.Usuario.invalid) {
      Object.values(this.Usuario.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    }

    this.usuarioService.GuardarUsuario(this.Usuario.value).subscribe(data => { 
      this._toastService.showOk("Se registró el usuario correctamente");
      this.router.navigate(["/usuario-tabla"]);
    },
    error => {
      this._toastService.showError(error.error);
    });


    // if (this.Usuario.valid == true) {
    //   this.respuesta = this.usuarioService.GuardarUsuario(this.Usuario.value).subscribe(data => { });
    //   if (this.respuesta == 0) {
    //     console.log("No se guardo correcto hubo error")
    //   }
    //   else {
    //     console.log("Se guardo Joya!!!");
    //     this.router.navigate(["/usuario-tabla"]);

    //   }
    // }
  }

  clickMethod() {
    alert("Se registró el usuario correctamente");
    //Luego de presionar click debe redireccionar al home

  }

  volver() {
    this.router.navigate(["/usuario-tabla"]);
  }

  get nombrePersonaNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.NombrePersona.errors;
  }

  get apellidoNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.Apellido.errors;
  }

  get telefonoNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.Telefono.errors;
  }

  get dniNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.Dni.errors;
  }

  get fechaNacimientoNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.FechaNac.errors;
  }

  get domicilioNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.Domicilio.errors;
  }

  get alturaNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.Altura.errors;
  }

  get mailNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.Mail.errors;
  }

  get nombreUsuarioNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.NombreUser.errors;
  }

  get rolNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.TiposRol.errors;
  }

  get contraseniaNoValido() {
    // Si es una edicion y no esta mandando la contrasenia esta bien, se deja la que ya tenia
    // if (this.isFormSubmitted && this.Usuario.controls.IdUsuario.value != 0 
    //   && (this.Usuario.controls.Contrasenia.value == null || this.Usuario.controls.Contrasenia.value == "")) {
    //   return false;
    // }

    return this.isFormSubmitted && this.Usuario.controls.Contrasenia.errors;
  }

  get areaNoValido() {
    return this.isFormSubmitted && this.Usuario.controls.NroArea.errors;
  }

}
