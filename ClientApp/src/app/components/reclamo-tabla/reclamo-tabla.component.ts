import { Component, OnInit, Input } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { ClaveValor } from 'src/app/modelos_Interfaces/ClaveValor';

@Component({
  selector: 'reclamo-tabla',
  templateUrl: './reclamo-tabla.component.html',
  styleUrls: ['./reclamo-tabla.component.css']
})
export class ReclamoTablaComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  Reclamos: any[] = [];
  ReclamosFiltrados: any[] = [];
  p: number = 1;
  cabeceras: string[] = ["Número", "Fecha Generado", "Estado", "Tipo", "Prioridad", "Asignado a"];
  listaPrioridades: ClaveValor[] = [];
  prioridadSeleccionada: ClaveValor = {clave : "0", valor : "Todas"};

  constructor(private reclamoservice: ReclamoService) {
  }

  ngOnInit() {
    this.listaPrioridades.push({clave : "0", valor : "Todas"});
    this.listaPrioridades.push({clave : "1", valor : "Alta"});
    this.listaPrioridades.push({clave : "2", valor : "Media"});
    this.listaPrioridades.push({clave : "3", valor : "Baja"});
    this.listaPrioridades.push({clave : "4", valor : "Sin Priorizar"});

    this.reclamoservice.getReclamos().subscribe(data => 
      { 
        this.Reclamos = data; 
        this.ReclamosFiltrados = data;
      });
  }

  filtrarPrioridad() {
    if (this.prioridadSeleccionada.clave == "0") {
      this.ReclamosFiltrados = this.Reclamos;
    } else {
      this.ReclamosFiltrados = this.Reclamos.filter(p => p.nroPrioridad == this.prioridadSeleccionada.clave);
    }
  }

}



