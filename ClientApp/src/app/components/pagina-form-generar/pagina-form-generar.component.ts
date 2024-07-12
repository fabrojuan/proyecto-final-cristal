import { Component, OnInit } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'pagina-form-generar',
  templateUrl: './pagina-form-generar.component.html',
  styleUrls: ['./pagina-form-generar.component.css']
})
export class PaginaFormGenerarComponent implements OnInit {
  titulo: string = '';
  parametro: any;
  TiposReclamo: any;
  nombreVecino: any;
  idVecino: any;
  Pagina: UntypedFormGroup;
  isFormSubmitted: boolean = false;

  constructor(private usuarioService: UsuarioService, private router: Router, private activatedRoute: ActivatedRoute,
              public _toastService: ToastService
  ) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"]
      if (this.parametro >= 1) {
        this.titulo = "Editar";
      } else {
        this.titulo = "Añadir";
      }
    });
    this.Pagina = new UntypedFormGroup(
      {
        "idPagina": new UntypedFormControl("0"),
        "Mensaje": new UntypedFormControl("", [Validators.required, Validators.maxLength(70)]),
        "Accion": new UntypedFormControl("", [Validators.required, Validators.maxLength(100)]),
        "Bhabilitado": new UntypedFormControl("1"),
        "Bvisible": new UntypedFormControl("1"),




      }
    );
  }


  ngOnInit() {

    this.usuarioService.recuperarPagina(this.parametro).subscribe(data => {
      if (data.accion == null) {
        // this.router.navigate(["/error-pagina-login"]);   //DEBE iR A pAGINA NO EXISTE
      }
      else {
        this.Pagina.controls["idPagina"].setValue(data.idPagina);
        this.Pagina.controls["Accion"].setValue(data.accion);
        this.Pagina.controls["Mensaje"].setValue(data.mensaje);
        this.Pagina.controls["Bvisible"].setValue(data.bvisible);
        this.Pagina.controls["Bhabilitado"].setValue(data.bhabilitado);

      }

    });

  }

  guardarDatos() {

    this.isFormSubmitted = true;

    if (this.Pagina.invalid) {
      Object.values(this.Pagina.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    }

    this.usuarioService.guardarPagina(this.Pagina.value).subscribe(data => {
      this._toastService.showOk("La página se guardó exitosamente");
      this.router.navigate(["/pagina-tabla"]);
    }, error => {
      this._toastService.showError("Ocurrió un error y no se pudo guardar la página");
    });

  }


  cancelar() {
    this.router.navigate(["/pagina-tabla"]);
  }

  esEdicionDePagina() {
    return this.Pagina.controls.idPagina.value != 0;
  }

  get nombreNoValido() {
    return this.isFormSubmitted && this.Pagina.controls.Mensaje.errors;
  }

  get rutaNoValido() {
    return this.isFormSubmitted && this.Pagina.controls.Accion.errors;
  }

}




