import { Component, OnInit, Input } from '@angular/core';
import { UsuarioService } from '../../services/usuario.service';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalSiNoComponent } from '../modal-si-no/modal-si-no.component';
import { ToastService } from 'src/app/services/toast.service';

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
  cabeceras: string[] = ["Código", "Usuario", "Fecha Alta", "Rol"];
  idUsuarioEliminar: number = 0;

  constructor(private usuarioservice: UsuarioService, private router: Router,
              private modalService: NgbModal, public _toastService: ToastService) {  }


  ngOnInit() {
    this.usuarioservice.getUsuariosEmpleados().subscribe(data => this.usuarios = data);
    //console.log(this.usuarios);
  }
  volverHome() {
    this.router.navigate(["/bienvenida"]);
  }

  preguntarEliminarUsuario(idUsuario: number) {
    this.idUsuarioEliminar = idUsuario;

    const modalRef = this.modalService.open(ModalSiNoComponent, 
      { animation: false, backdrop: "static", centered: true, keyboard: false });

    modalRef.componentInstance.mensajeMostrar = "Está seguro que desea dar de baja al empleado?";
      
    modalRef.componentInstance.eventoModalSiNoResultado.subscribe((opcion: string) => {
      modalRef.close();

      if (opcion == "SI") {

        this.usuarioservice.borrarUsuario(this.idUsuarioEliminar)
          .subscribe(res => {
            this._toastService.showOk("El empleado fue dado de baja exitosamente");
            this.usuarioservice.getUsuariosEmpleados().subscribe(data => this.usuarios = data);
          }, error => {
            this._toastService.showError("No se pudo dar de baja al empleado");
          });

      }

    });

  }

}


