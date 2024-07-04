import { Component, OnInit } from '@angular/core';
import { UsuarioService } from '../services/usuario.service';   //llamo a este serv para saber si hay alguienlogueado por eso implementé oninit tambien.
import { VecinoService } from '../services/vecino.service';   //llamo a este serv para saber si hay alguienlogueado por eso implementé oninit tambien.

import { Router } from '@angular/router';


@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  menus: any; //array de menus
  constructor(private usuarioService: UsuarioService, private vecinoService: VecinoService, private router: Router) { }
  isExpanded = false;
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnInit() {
    this.usuarioService.ObtenerSession().subscribe(data => {
      if (data) {
        // Llamar a Listar Paginas....
        this.usuarioService.listarPaginas().subscribe(dato => {
          this.menus = dato;
        });

      }
    });


    //Mañana meter aca el login del vecino sino no tendre nunca el login = a Treu pero cambiar nombres
    this.vecinoService.ObtenerSession().subscribe(data => {
    });





  }

  //No Reparado
  cerrarSession() {
    this.usuarioService.cerrarSession();
    sessionStorage.clear();
    this.router.navigate(["/"]);
  }
  //No reparado

  mostrarUOcultarMenuLateral() {
    const body = document.getElementsByTagName("body")[0];
    body.classList.toggle('toggle-sidebar');
  }

  cerrarSessionVecino() {
    this.vecinoService.cerrarSessionVecino().subscribe((res: any) => {
      if (res.valor == "OK") {
        this.router.navigate(["/"]);
      }
    });
  }

  esUsuarioLogueado(): boolean {
    return this.usuarioService.isLoggedIn();
  }

  esVecinoLogueado(): boolean {
    return this.vecinoService.isLoggedIn();
  }


}
