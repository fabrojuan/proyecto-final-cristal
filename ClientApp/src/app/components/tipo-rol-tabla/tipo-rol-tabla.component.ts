import { Component, OnInit, Input } from '@angular/core';
import { UsuarioService } from '../../services/usuario.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'tipo-rol-tabla',
  templateUrl: './tipo-rol-tabla.component.html',
  styleUrls: ['./tipo-rol-tabla.component.css']
})
export class TipoRolTablaComponent implements OnInit {
  //@Input() usuarios: any;
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  roles: any;
  p: number = 1;
  cabeceras: string[] = ["Id Rol", "Nombre Rol", "Habilitado"];
  constructor(private usuarioservice: UsuarioService, private router: Router) {
  }

  ngOnInit() {
    this.usuarioservice.listarRoles().subscribe(data => this.roles = data);
    //console.log(this.usuarios);

  }
  EliminarRol(iidRol: any) {
    this.usuarioservice.eliminarRol(iidRol).subscribe(data => {
      this.usuarioservice.listarRoles().subscribe(data => this.roles = data);  //Refrescaremos la info del ngoninit
    })
  }




  volverHome() {
    this.router.navigate(["/"]);
  }
}


