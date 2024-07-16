import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AreasService } from 'src/app/services/areas.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-reclamo-suspender',
  templateUrl: './reclamo-suspender.component.html',
  styleUrls: ['./reclamo-suspender.component.css']
})
export class ReclamoSuspenderComponent implements OnInit {

  @Output() eventoSuspensionConfirmado: EventEmitter<any> = new EventEmitter();
  descripcion : string = "";

  constructor(public activeModal: NgbActiveModal,
              private _areasService: AreasService,
              public _toastService: ToastService
  ) { }

  ngOnInit(): void { }

  confirmarSuspension() {

    if (this.descripcion && this.descripcion.length > 0) {
      this.eventoSuspensionConfirmado.emit( {
        descripcion: this.descripcion   
      });
    } else {
      this._toastService.showError("Debe cargar una descripción");
    }    
  }

  cancelarSuspension() {
    this.activeModal.dismiss();
  }

}
