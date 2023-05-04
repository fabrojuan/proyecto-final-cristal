import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReclamoService } from 'src/app/services/reclamo.service';

@Component({
  selector: 'app-reclamo-cabecera',
  templateUrl: './reclamo-cabecera.component.html',
  styleUrls: ['./reclamo-cabecera.component.css']
})
export class ReclamoCabeceraComponent implements OnInit {

  reclamo: any = {};

  constructor(private _reclamoService: ReclamoService,
              private _activatedRouter: ActivatedRoute) { 

    this._activatedRouter.params.subscribe(params => {
      if (params['id'] != null) {
        _reclamoService.getReclamo(params['id']).subscribe(rec => this.reclamo = rec);
      }
    });
    
  }

  ngOnInit(): void {
  }

}
