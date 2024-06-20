import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AreasService } from 'src/app/services/areas.service';

@Component({
  selector: 'app-reclamo-suspender',
  templateUrl: './reclamo-suspender.component.html',
  styleUrls: ['./reclamo-suspender.component.css']
})
export class ReclamoSuspenderComponent implements OnInit {

  @Output() eventoSuspensionConfirmado: EventEmitter<any> = new EventEmitter();
  descripcion : string = "";

  constructor(public activeModal: NgbActiveModal,
              private _areasService: AreasService
  ) { }

  ngOnInit(): void { }

  confirmarSuspension() {
    console.log("Desde dentro del modal. " + this.descripcion);
    this.eventoSuspensionConfirmado.emit( {
      descripcion: this.descripcion   
    });
  }

  cancelarSuspension() {
    this.activeModal.dismiss();
  }

}
