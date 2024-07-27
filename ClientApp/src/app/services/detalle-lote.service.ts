import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DetalleLoteComponent } from '../components/detalle-lote/detalle-lote.component';

@Injectable({
  providedIn: 'root'
})
export class DetalleLoteService {

  constructor(private modalService: NgbModal) { }

  public open(tituloInformacion: string, mensajeInformacion: string, idLote: any) {
    const modalRef = this.modalService.open(DetalleLoteComponent);
    modalRef.componentInstance.tituloInformacion = tituloInformacion;
    modalRef.componentInstance.mensajeInformacion = mensajeInformacion;
    modalRef.componentInstance.idLote = idLote;
    return modalRef.result;
  }
}
