import { Component,Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'dialogo-confirmacion',
  templateUrl: './dialogo-confirmacion.component.html'
})

 
export class DialogoConfirmacionComponent {
  @Input() tituloConfirmacion: any;
  @Input() mensajeConfirmacion: any;
  @Input() idArchivo: any; // Propiedad para almacenar el idArchivo

  constructor(public activeModal: NgbActiveModal) { }

  onConfirm() {
    this.activeModal.close(this.idArchivo); // Cierra el modal y devuelve el idArchivo
  }
}
