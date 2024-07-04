import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-si-no',
  templateUrl: './modal-si-no.component.html',
  styleUrls: ['./modal-si-no.component.css']
})
export class ModalSiNoComponent implements OnInit {

  @Output() eventoModalSiNoResultado: EventEmitter<string> = new EventEmitter();
  mensajeMostrar : string = "";

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void { }

  opcionSi() {
    this.eventoModalSiNoResultado.emit( "SI" );
  }

  opcionNo() {
    this.activeModal.dismiss();
  }

}
