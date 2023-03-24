import { Component, OnInit, Input } from '@angular/core';
import { UsuarioService } from '../../services/usuario.service';
import { Router } from '@angular/router';

@Component({
  selector: 'usuario-tabla',
  templateUrl: './usuario-tabla.component.html',
  styleUrls: ['./usuario-tabla.component.css']
})
export class UsuarioTablaComponent implements OnInit {
  //@Input() usuarios: any;
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  usuarios: any;
  p: number = 1;
  cabeceras: string[] = ["Id Usuario", "Nombre User", "Fecha Alta", "Rol"];
  constructor(private usuarioservice: UsuarioService, private router: Router) {
  }


  ngOnInit() {
    this.usuarioservice.getUsuario().subscribe(data => this.usuarios = data);
    //console.log(this.usuarios);
  }
  volverHome() {
    this.router.navigate(["/bienvenida"]);
  }

}


