import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-trabajo-reclamo-form-generar',
  templateUrl: './trabajo-reclamo-form-generar.component.html',
  styleUrls: []
})
export class TrabajoReclamoFormGenerarComponent implements OnInit {

  @Output() eventoCargaTrabajo: EventEmitter<any> = new EventEmitter();
  descripcionTrabajo : string = "";
  fechaTrabajo: string = "";
  fechaHastaMaxTrabajo?: string;
  lang?: string;

  constructor(public activeModal: NgbActiveModal, public _toastService: ToastService) {
    const hoy = new Date();
    this.fechaHastaMaxTrabajo = hoy.toISOString().split('T')[0];
    this.lang = 'es';
   }

  ngOnInit(): void { }

  confirmarAsignacion() {
    if (this.fechaTrabajo && this.fechaTrabajo.length > 0 && this.descripcionTrabajo && this.descripcionTrabajo.length > 0) {
      this.eventoCargaTrabajo.emit( {
        fechaTrabajo: this.fechaTrabajo,
        descripcionTrabajo: this.descripcionTrabajo   
      });
    } else {
      this._toastService.showError("Debe indicar una fecha y descripción del trabajo");
    }
    
  }

  cancelarAsignacion() {
    this.activeModal.dismiss();
  }

}
