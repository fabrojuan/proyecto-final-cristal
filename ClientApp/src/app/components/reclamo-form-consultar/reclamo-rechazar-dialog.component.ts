import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-reclamo-rechazar-dialog',
  templateUrl: './reclamo-rechazar-dialog.component.html',
  styleUrls: ['./reclamo-rechazar-dialog.component.css']
})
export class ReclamoRechazarDialogComponent implements OnInit {

  @Output() eventoRechazoConfirmado: EventEmitter<any> = new EventEmitter();
  descripcionMotivoRechazo : string = "";

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  confirmarRechazo() {
    console.log("Desde dentro del modal. " + this.descripcionMotivoRechazo);
    this.eventoRechazoConfirmado.emit(this.descripcionMotivoRechazo);
    //this.activeModal.close(this.descripcionMotivoRechazo);
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
