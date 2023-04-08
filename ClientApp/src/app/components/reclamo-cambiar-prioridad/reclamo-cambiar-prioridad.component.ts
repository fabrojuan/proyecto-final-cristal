import { Component, OnInit } from '@angular/core';
import { Prioridad } from 'src/app/modelos_Interfaces/Prioridad';
import { ReclamoService } from 'src/app/services/reclamo.service';

@Component({
  selector: 'app-reclamo-cambiar-prioridad',
  templateUrl: './reclamo-cambiar-prioridad.component.html',
  styleUrls: ['./reclamo-cambiar-prioridad.component.css']
})  
export class ReclamoCambiarPrioridadComponent implements OnInit {

  listaPrioridades: Prioridad[] = [];
  prioridadSeleccionada: Prioridad = {
    nroPrioridad: 0,
    nombrePrioridad: ''
  };

  constructor(private _reclamoService: ReclamoService) {
    this._reclamoService.getPrioridades().subscribe(prioridades => this.listaPrioridades = prioridades);
   }

  ngOnInit(): void {

  }

}
