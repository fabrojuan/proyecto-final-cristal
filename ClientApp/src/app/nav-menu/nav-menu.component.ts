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
  login: boolean = false;//ojo con esta linea
  loginVecino: boolean = false;//ojo con esta linea
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
        this.login = true;
        // Llamar a Listar Paginas....
        this.usuarioService.listarPaginas().subscribe(dato => {
          this.menus = dato;
        });

      }
      else {
        this.login = false;
        //this.router.navigate(["/login"]);
      }
    });


    //Mañana meter aca el login del vecino sino no tendre nunca el login = a Treu pero cambiar nombres
    this.vecinoService.ObtenerSession().subscribe(data => {
      if (data) {
        this.loginVecino = true;
        // Llamar a Listar Paginas....
        //this.usuarioService.listarPaginas().subscribe(dato => {
        //  this.menus = dato;
        //});

      }
      else {
        this.loginVecino = false;
        //this.router.navigate(["/login"]);
      }
    });





  }
  //No Reparado
  cerrarSession() {
    this.usuarioService.cerrarSession().subscribe((res: any) => {
      if (res.valor == "OK") {
        this.login = false;
      }
      else {
        this.login = true;

      }


    });
  }
  //No reparado
  cerrarSessionVecino() {
    this.vecinoService.cerrarSessionVecino().subscribe((res: any) => {
      if (res.valor == "OK") {
        this.loginVecino = false;

      }
      else {
        this.loginVecino = true;

      }

    });
  }


}
