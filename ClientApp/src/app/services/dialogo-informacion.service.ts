import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogoInformacionComponent } from '../components/dialogo-informacion/dialogo-informacion.component';


@Injectable({
  providedIn: 'root'
})
export class DialogoInformacionService {
  constructor(private modalService: NgbModal) { }

  public open(tituloInformacion: string, mensajeInformacion: string) {
    const modalRef = this.modalService.open(DialogoInformacionComponent);
    modalRef.componentInstance.tituloInformacion = tituloInformacion;
    modalRef.componentInstance.mensajeInformacion = mensajeInformacion;
    return modalRef.result;
  }
}
