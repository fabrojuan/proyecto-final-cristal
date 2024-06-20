import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Area } from 'src/app/modelos_Interfaces/Area';
import { ClaveValor } from 'src/app/modelos_Interfaces/ClaveValor';
import { AreasService } from 'src/app/services/areas.service';

@Component({
  selector: 'app-reclamo-finalizar',
  templateUrl: './reclamo-finalizar.component.html',
  styleUrls: ['./reclamo-finalizar.component.css']
})
export class ReclamoFinalizarComponent implements OnInit {

  @Output() eventoFinalizacionConfirmado: EventEmitter<any> = new EventEmitter();
  descripcion : string = "";
  resultados: ClaveValor[] = [{clave : "CANCELAR", valor : "Cancelado"}, {clave : "SOLUCIONAR", valor : "Solucionado"}];
  resultadoSeleccionado: string = "SOLUCIONAR";

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  confirmarAsignacion() {
    console.log("Desde dentro del modal. " + this.descripcion);
    this.eventoFinalizacionConfirmado.emit( {
      resultado: this.resultadoSeleccionado,
      descripcion: this.descripcion   
    });
  }

  cancelarAsignacion() {
    this.activeModal.dismiss();
  }

}

