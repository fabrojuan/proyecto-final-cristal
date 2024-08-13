import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ObservacionReclamo } from 'src/app/modelos_Interfaces/ObservacionReclamo';
import { ReclamoService } from 'src/app/services/reclamo.service';

@Component({
  selector: 'app-observaciones-reclamo-tabla',
  templateUrl: './observaciones-reclamo-tabla.component.html',
  styleUrls: ['./observaciones-reclamo-tabla.component.css']
})
export class ObservacionesReclamoTablaComponent implements OnInit {

  @Input() public nroReclamo: number = 0;
  cabeceras: string[] = ["Fecha", "Hora", "Usuario", "Descripción"];
  p: number = 1;
  observaciones: ObservacionReclamo[] = [];

  constructor(public activeModal: NgbActiveModal,
              private reclamoservice: ReclamoService) { }

  ngOnInit(): void {

    this.reclamoservice.getObservacionesReclamo(this.nroReclamo).subscribe(data => 
      { 
        this.observaciones = data; 
      });

  }

  cerrarMostrarObervaciones() {
    this.activeModal.dismiss();
  }

}
