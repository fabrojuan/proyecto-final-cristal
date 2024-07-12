import { Component, OnInit, Input } from '@angular/core';
import { UsuarioService } from '../../services/usuario.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastService } from 'src/app/services/toast.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalSiNoComponent } from '../modal-si-no/modal-si-no.component';

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
  cabeceras: string[] = [/*"Id",*/ "Código", "Nombre", "Tipo", "Habilitado"];
  idRolEliminar: number = 0;

  constructor(private usuarioservice: UsuarioService, private router: Router,
              public _toastService: ToastService, private modalService: NgbModal
  ) { }

  ngOnInit() {
    this.usuarioservice.listarRoles().subscribe(data => this.roles = data);
    //console.log(this.usuarios);

  }

  EliminarRol(iidRol: any) {

    this.idRolEliminar = iidRol;

    const modalRef = this.modalService.open(ModalSiNoComponent, 
      { animation: false, backdrop: "static", centered: true, keyboard: false });

    modalRef.componentInstance.mensajeMostrar = "Está seguro que desea eliminar el rol?";
      
    modalRef.componentInstance.eventoModalSiNoResultado.subscribe((opcion: string) => {

      modalRef.close();
      if (opcion == "SI") {

        this.usuarioservice.eliminarRol(iidRol).subscribe(data => {
          this.usuarioservice.listarRoles().subscribe(data => this.roles = data);
          this._toastService.showOk("El rol fue eliminado exitosamente");
        }, error => {
          this._toastService.showError("No se pudo eliminar el rol");
        });

      }

    });

    
  }




  volverHome() {
    this.router.navigate(["/"]);
  }

  gethabilitadoDescripcion(bhabilitado: number) : string{
    if (bhabilitado == 1) {
      return "Si";
    }
    return "No";
  }

}


