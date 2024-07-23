import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-reclamo-enviar-finalizar',
  templateUrl: './reclamo-enviar-finalizar.component.html',
  styleUrls: ['./reclamo-enviar-finalizar.component.css']
})
export class ReclamoEnviarFinalizarComponent implements OnInit {

  @Output() eventoEnviarCierreConfirmado: EventEmitter<string> = new EventEmitter();
  descripcion : string = "";

  constructor(public activeModal: NgbActiveModal,
              public _toastService: ToastService
  ) { }

  ngOnInit(): void { }

  confirmarSuspension() {

    if (this.descripcion && this.descripcion.length > 0) {
      this.eventoEnviarCierreConfirmado.emit( 
        this.descripcion   
      );
    } else {
      this._toastService.showError("Debe cargar una descripción");
    }    
  }

  cancelarSuspension() {
    this.activeModal.dismiss();
  }

}
