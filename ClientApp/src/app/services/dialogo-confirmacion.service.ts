import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogoConfirmacionComponent } from '../components/dialogo-confirmacion/dialogo-confirmacion.component';

@Injectable({
  providedIn: 'root'
})
export class DialogoConfirmacionService {

  constructor(private modalService: NgbModal) { }

  public confirm(tituloConfirmacion: string, mensajeConfirmacion: string, idArchivo: number): Promise<number> {
    const modalRef = this.modalService.open(DialogoConfirmacionComponent);
    modalRef.componentInstance.tituloConfirmacion = tituloConfirmacion;
    modalRef.componentInstance.mensajeConfirmacion = mensajeConfirmacion;
    modalRef.componentInstance.idArchivo = idArchivo; // Asigna el idArchivo al modal
    return modalRef.result;
  }
}
