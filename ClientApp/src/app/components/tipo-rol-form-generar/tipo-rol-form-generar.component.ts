import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
//import { resolve } from 'url';

@Component({
  selector: 'tipo-rol-form-generar',
  templateUrl: './tipo-rol-form-generar.component.html',
  styleUrls: ['./tipo-rol-form-generar.component.css']
})
export class TipoRolFormGenerarComponent implements OnInit {
  Rol: FormGroup;
  titulo: string = "";
  parametro: any;
  respuesta: any = 0;
  paginas: any;
  constructor(private usuarioService: UsuarioService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"]
      if (this.parametro >= 1) {
        this.titulo = "Editar";
        this.usuarioService.listarPaginasRecuperar(this.parametro).subscribe(res => { })
      } else {
        this.titulo = "Nuevo";
      }
    });
    this.Rol = new FormGroup(
      {
        "IidRol": new FormControl("0"),
        "NombreRol": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "BHabilitado": new FormControl("0", [Validators.required, Validators.maxLength(1), Validators.pattern("[0-1]{1,}")]),
        "Valores": new FormControl(""),

      }
    );
    this.usuarioService.listarPaginasTipoRol().subscribe(data => this.paginas = data);  //El constructor siempre e ejecuta antes que el oninit y esta info es estatica.

  }
  ngOnInit() {

    if (this.parametro >= 1) {
      this.usuarioService.listarPaginasRecuperar(this.parametro).subscribe(res => {

        this.Rol.controls["IidRol"].setValue(res.iidRol);
        this.Rol.controls["NombreRol"].setValue(res.nombreRol);
        this.Rol.controls["BHabilitado"].setValue(res.bHabilitado);
        var listaPaginas = res.listaPagina.map((p:any) => p.idPagina);
        //var listaPaginas = res.listaPagina.pipe(map((p: any) => p.idPagina));    //.pipe(map((res: any)
        //Pintaremos la info.

        //usaremos un tieput para que se carguen todos los checks parametrizados.
        setTimeout(() => {
          var checks = document.getElementsByClassName("check");
          var ncheck = checks.length;
          var check: any;
          for (var i = 0; i < ncheck; i++) {
            check = checks[i];
            var indice = listaPaginas.indexOf(check.name * 1); //en esta linea check.name es la prop del html que nos toma el iidpagina, lo multiplico por uno para obtener el int de ese valor es ocmo hacer un parse
            if (indice > -1) //si está en la lista sera mayor a -1
            {
              check.checked = true;  //Entonces chequeamos la pagina como true aurtomaticamente
            }
          }

        }, 500);

      });
    }
    //this.usuarioService.listarPaginasTipoRol(this.parametro).subscribe(param => {   //aca esta el problema

    //      this.Rol.controls["IidRol"].setValue(param.IidRol);
    //      this.Rol.controls["NombreRol"].setValue(param.NombreRol);
    //      this.Rol.controls["BHabilitado"].setValue(param.BHabilitado);
    //    });


  }
  guardarDatos() {
    if (this.Rol.valid == true) {
      //  this.respuesta =
      this.usuarioService.guardarROL(this.Rol.value).subscribe(data => {
        this.router.navigate(["/tipo-rol-form-generar"]);
      })
      //  if (this.respuesta == 0) {
      //    console.log("No se guardo correcto hubo error");
    }
    else {
      console.log("Se guardo Joya!!!");
      this.router.navigate(["/"]);
    }
  }

  verCheck() {
    var seleccionados = "";
    var noseleccionados = "";
    var checks = document.getElementsByClassName("check");
    var check: any;
    for (var i = 0; i < checks.length; i++) {
      check = checks[i];
      if (check.checked == true) {
        seleccionados += check.name; //Saco el id de la paginas sleeccionadas en el html hay una prop que se llama name=pagina.idPagina
        seleccionados += "-"; //separo los id de cada uno con un guion.
      }
      else {
        if (check.checked == false) {
          noseleccionados += check.name;
          noseleccionados += "-";
        }
      }
    }
    if (seleccionados != "") {
      seleccionados = seleccionados.substring(0, seleccionados.length - 1)  //Aqui elimino el utlimo caracter de seleccionados que eá un - y el ultimo tiene que ser un nro.
      this.Rol.controls["Valores"].setValue(seleccionados);
    }
  }

  clickMethod() {
    alert("Se registró el usuario correctamente");
    //Luego de presionar click debe redireccionar al home
  }

  volverHome() {
    this.router.navigate(["/"]);
  }

}









