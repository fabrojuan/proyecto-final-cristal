import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-trabajo-reclamo-form-generar',
  templateUrl: './trabajo-reclamo-form-generar.component.html',
  styleUrls: []
})
export class TrabajoReclamoFormGenerarComponent implements OnInit {

  @Output() eventoCargaTrabajo: EventEmitter<any> = new EventEmitter();
  descripcionTrabajo : string = "";
  fechaTrabajo: string = "";
  fechaHastaMaxTrabajo: Date = new Date();

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void { }

  confirmarAsignacion() {
    console.log("Desde dentro del modal. " + this.descripcionTrabajo + " " + this.fechaTrabajo);
    this.eventoCargaTrabajo.emit( {
      fechaTrabajo: this.fechaTrabajo,
      descripcionTrabajo: this.descripcionTrabajo   
    });
  }

  cancelarAsignacion() {
    this.activeModal.dismiss();
  }

}
