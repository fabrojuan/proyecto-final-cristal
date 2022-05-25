import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'pagina-tabla',
  templateUrl: './pagina-tabla.component.html',
  styleUrls: ['./pagina-tabla.component.css']
})
export class PaginaTablaComponent implements OnInit {
  @Input() isMantenimiento = true;
  Paginas: any;
  p: number = 1;
  cabeceras: string[] = ["Id Pagina", "Nombre", "Accion"];

  constructor(private usuarioservice: UsuarioService, private router: Router) { }

  ngOnInit() {
    this.usuarioservice.listarTodasPaginas().subscribe(data => this.Paginas = data);

  }
  volverHome() {
    this.router.navigate(["/"]);
  }


  eliminar(idPagina: any) {
    if (confirm("¿Desea Eliminar esta pagina ?") == true) {
      this.usuarioservice.eliminarPagina(idPagina).subscribe(data => {
        this.usuarioservice.listarTodasPaginas().subscribe(data => this.Paginas = data);

      });
    }
  }



}




