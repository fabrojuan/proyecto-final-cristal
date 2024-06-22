import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-reclamo-rechazar-dialog',
  templateUrl: './reclamo-rechazar-dialog.component.html',
  styleUrls: ['./reclamo-rechazar-dialog.component.css']
})
export class ReclamoRechazarDialogComponent implements OnInit {

  @Output() eventoRechazoConfirmado: EventEmitter<any> = new EventEmitter();
  descripcionMotivoRechazo : string = "";

  constructor(public activeModal: NgbActiveModal, public _toastService: ToastService) { }

  ngOnInit(): void {
  }

  confirmarRechazo() {
    if (this.descripcionMotivoRechazo.length == 0) {
      this._toastService.showError("Debe ingresar el motivo del rechazo");
    } else {
      this.eventoRechazoConfirmado.emit(this.descripcionMotivoRechazo);
    }    
  }

  cancelarRechazo() {
    this.activeModal.dismiss();
  }

  //cerrarModalSinAccion() {
  //  console.log("aca no ha pasado nada");
  //}

  //onDismiss(reason : String):void {
  //  console.log("onDismissssssss");
  //  this.activeModal.dismiss(reason);
  //}

}
