import { Component, OnInit } from '@angular/core';
import { Prioridad } from 'src/app/modelos_Interfaces/Prioridad';
import { ReclamoService } from 'src/app/services/reclamo.service';

@Component({
  selector: 'app-reclamo-cambiar-Prioridad',
  templateUrl: './reclamo-cambiar-Prioridad.component.html',
  styleUrls: ['./reclamo-cambiar-Prioridad.component.css']
})  
export class ReclamoCambiarPrioridadComponent implements OnInit {

  listaPrioridades: Prioridad[] = [];
  PrioridadSeleccionada: Prioridad = {
    nroPrioridad: 0,
    nombrePrioridad: ''
  };

  constructor(private _reclamoService: ReclamoService) {
    this._reclamoService.getPrioridades().subscribe(Prioridades => this.listaPrioridades = Prioridades);
   }

  ngOnInit(): void {

  }

}
