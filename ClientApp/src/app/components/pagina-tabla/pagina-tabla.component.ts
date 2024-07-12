import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import { ModalSiNoComponent } from '../modal-si-no/modal-si-no.component';
import { ToastService } from 'src/app/services/toast.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'pagina-tabla',
  templateUrl: './pagina-tabla.component.html',
  styleUrls: ['./pagina-tabla.component.css']
})
export class PaginaTablaComponent implements OnInit {
  @Input() isMantenimiento = true;
  Paginas: any;
  p: number = 1;
  cabeceras: string[] = ["Número", "Nombre", "Ruta"];
  idPaginaEliminar: number = 0;

  constructor(private usuarioservice: UsuarioService, private router: Router, 
              public _toastService: ToastService, private modalService: NgbModal) { }

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

  preguntarEliminarPagina(idPagina: number) {
    this.idPaginaEliminar = idPagina;

    const modalRef = this.modalService.open(ModalSiNoComponent, 
      { animation: false, backdrop: "static", centered: true, keyboard: false });

    modalRef.componentInstance.mensajeMostrar = "Está seguro que desea eliminar la página?";
      
    modalRef.componentInstance.eventoModalSiNoResultado.subscribe((opcion: string) => {
      modalRef.close();

      if (opcion == "SI") {

        this.usuarioservice.eliminarPagina(idPagina).subscribe(data => {
          this.usuarioservice.listarTodasPaginas().subscribe(data => this.Paginas = data);  
          this._toastService.showOk("La página fue dada de baja exitosamente");
        }, error => {
          this._toastService.showError("No se pudo eliminar la página");
        });

      }

    });

  }



}




