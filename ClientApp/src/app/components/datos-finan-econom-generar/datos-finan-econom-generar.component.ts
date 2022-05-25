import { Component, OnInit } from '@angular/core';
import { ImpuestoService } from '../../services/impuesto.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DatasetFinanzasService } from '../../services/dataset-finanzas.service';

@Component({
  selector: 'datos-finan-econom-generar',
  templateUrl: './datos-finan-econom-generar.component.html',
  styleUrls: ['./datos-finan-econom-generar.component.css']
})
export class DatosFinanEconomGenerarComponent implements OnInit {

  impuestos: any;
  constructor(private datasetImpuestoService: DatasetFinanzasService, private router: Router) { }


  generarDatasetImpuestos() {
    console.log("Lllamo a la funcion.");
    this.datasetImpuestoService.generarDatasetImpuestos().subscribe(data => {
      this.impuestos = data;

    });
    console.log('Los impuestos son', this.impuestos);

  }

  volver() {
    this.router.navigate(["/bienvenida"]);
  }
  ngOnInit() {
  }

}
