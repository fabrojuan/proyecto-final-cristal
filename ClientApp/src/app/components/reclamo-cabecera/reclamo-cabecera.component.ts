import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Area } from 'src/app/modelos_Interfaces/Area';
import { AreasService } from 'src/app/services/areas.service';
import { ReclamoService } from 'src/app/services/reclamo.service';

@Component({
  selector: 'app-reclamo-cabecera',
  templateUrl: './reclamo-cabecera.component.html',
  styleUrls: ['./reclamo-cabecera.component.css']
})
export class ReclamoCabeceraComponent implements OnInit {

  @Input() reclamo: any = {};
  @Input() rolUsuario: string = "VECINO";
  areas: Area[] = [];

  constructor(private _reclamoService: ReclamoService,
              private _activatedRouter: ActivatedRoute,
              private _areasService: AreasService) {}

  

  ngOnInit(): void {
    this._areasService.getAreas().subscribe(
      data => {
        this.areas = data;
      }
    );
  }

  getAreaDescripcion(nroArea: number): string {
    return this.areas.filter(area => area.nroArea == nroArea)[0].nombre || "Sin Valor";
  }

}
