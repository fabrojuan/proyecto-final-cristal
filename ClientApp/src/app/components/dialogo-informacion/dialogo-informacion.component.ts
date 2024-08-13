import { Component, OnInit,Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'dialogo-informacion',
  templateUrl: './dialogo-informacion.component.html',
  styleUrls: ['./dialogo-informacion.component.css']
})
export class DialogoInformacionComponent implements OnInit {
  @Input() tituloInformacion: any; // Título del modal
  @Input() mensajeInformacion: any; // Mensaje del modal
  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

}
