import { Component, OnInit } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { Router, ActivatedRoute } from '@angular/router';

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
  Pagina: FormGroup;
  constructor(private usuarioService: UsuarioService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"]
      if (this.parametro >= 1) {
        this.titulo = "Editar";
      } else {
        this.titulo = "Añadir";
      }
    });
    this.Pagina = new FormGroup(
      {
        "idPagina": new FormControl("0"),
        "Mensaje": new FormControl("", [Validators.required, Validators.maxLength(70)]),
        "Accion": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "Bhabilitado": new FormControl("1"),
        "Bvisible": new FormControl("1"),




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

      }

    });

  }

  guardarDatos() {
    //this.Pagina.controls["idPagina"].setValue(this.idPagina);
    if (this.Pagina.valid == true) {
      this.usuarioService.guardarPagina(this.Pagina.value).subscribe(data => {
        this.router.navigate(["/pagina-tabla"]);
      });
    }
  }


  clickMethod() {
    alert("La pagina se ha generada correctamente");
    //Luego de presionar click debe redireccionar al home
  }


}




