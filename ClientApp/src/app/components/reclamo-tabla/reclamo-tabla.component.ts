import { Component, OnInit, Input } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { ClaveValor } from 'src/app/modelos_Interfaces/ClaveValor';
import { EstadoReclamo } from 'src/app/modelos_Interfaces/EstadoReclamo';
import { HttpParams } from '@angular/common/http';
import { Area } from 'src/app/modelos_Interfaces/Area';
import { AreasService } from 'src/app/services/areas.service';

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
  cabeceras: string[] = ["Número", "Fecha Generado", "Estado", "Tipo", "Prioridad", "Área"];
  listaPrioridades: ClaveValor[] = [];
  prioridadSeleccionada: ClaveValor = {clave : "0", valor : "Todas"};
  TiposReclamo: any;
  tipoReclamoSeleccionado: number = 0;
  estadosReclamo: EstadoReclamo[] = [];
  estadoReclamoSeleccionado: number = 0;
  nroReclamoFiltro: string = '';
  nomApeVecinoFiltro: string = '';
  areaSeleccionada: number = 0;
  areas: Area[] = [];

  constructor(private reclamoservice: ReclamoService, private areaService: AreasService) {
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

      this.reclamoservice.getTipoReclamo().subscribe(data => this.TiposReclamo = data);

      this.reclamoservice.getEstados().subscribe(data => this.estadosReclamo = data);

      this.areaService.getAreas().subscribe(data => this.areas = data);

  }
  

  filtrarPrioridad() {
    if (this.prioridadSeleccionada.clave == "0") {
      this.ReclamosFiltrados = this.Reclamos;
    } else {
      this.ReclamosFiltrados = this.Reclamos.filter(p => p.nroPrioridad == this.prioridadSeleccionada.clave);
    }
  }

  aplicarFiltrado() {
    let queryParams = new HttpParams();

    if (this.estadoReclamoSeleccionado && this.estadoReclamoSeleccionado != 0) {
      queryParams = queryParams.append("estado", this.estadoReclamoSeleccionado);
    }

    if (this.tipoReclamoSeleccionado && this.tipoReclamoSeleccionado != 0) {
      queryParams = queryParams.append("tipo", this.tipoReclamoSeleccionado);
    }

    if (this.nroReclamoFiltro && this.nroReclamoFiltro.length != 0) {
      queryParams = queryParams.append("numero", this.nroReclamoFiltro);
    }

    if (this.nomApeVecinoFiltro && this.nomApeVecinoFiltro.length != 0) {
      queryParams = queryParams.append("nom_ape_vecino", this.nomApeVecinoFiltro);
    }

    if (this.areaSeleccionada && this.areaSeleccionada != 0) {
      queryParams = queryParams.append("area", this.areaSeleccionada);
    }


    this.reclamoservice.getReclamosConFiltros(queryParams).subscribe(data => 
      { 
        this.Reclamos = data; 
        this.ReclamosFiltrados = data;
      });

    
  }

  aplicarFiltroNroReclamo(event: any) {
      this.aplicarFiltrado(); 
  }

}



