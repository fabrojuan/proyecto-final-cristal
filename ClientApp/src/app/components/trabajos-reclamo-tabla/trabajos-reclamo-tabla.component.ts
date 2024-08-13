import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ReclamoService } from 'src/app/services/reclamo.service';

@Component({
  selector: 'app-trabajos-reclamo-tabla',
  templateUrl: './trabajos-reclamo-tabla.component.html',
  styleUrls: ['./trabajos-reclamo-tabla.component.css']
})
export class TrabajosReclamoTablaComponent implements OnInit {

  
  @Input() public nroReclamo: number = 0;
  cabeceras: string[] = ["Fecha", "Descripción", "Área"];
  p: number = 1;
  trabajos: any[] = [];

  constructor(public activeModal: NgbActiveModal,
    private reclamoservice: ReclamoService) { }

  ngOnInit(): void {
    this.reclamoservice.getTrabajosReclamo(this.nroReclamo).subscribe(data => 
      { 
        this.trabajos = data; 
      });
  }

  cerrarMostrarObervaciones() {
    this.activeModal.dismiss();
  }

}
