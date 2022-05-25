import { Component, OnInit, Input } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';

@Component({
  selector: 'reclamo-tabla',
  templateUrl: './reclamo-tabla.component.html',
  styleUrls: ['./reclamo-tabla.component.css']
})
export class ReclamoTablaComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  Reclamos: any;
  p: number = 1;
  cabeceras: string[] = ["Nro_Reclamo", "Fecha Generado", "Tipo_Reclamo", "Estado_Reclamo", "Prioridad", "Asignada a"];
  constructor(private reclamoservice: ReclamoService) {
  }

  ngOnInit() {
    this.reclamoservice.getReclamo().subscribe(data => this.Reclamos = data);

  }

}



