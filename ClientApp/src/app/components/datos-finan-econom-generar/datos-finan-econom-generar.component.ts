import { Component, OnInit } from '@angular/core';
import { ImpuestoService } from '../../services/impuesto.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DatasetFinanzasService } from '../../services/dataset-finanzas.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogoInformacionService } from '../../services/dialogo-informacion.service';


@Component({
  selector: 'datos-finan-econom-generar',
  templateUrl: './datos-finan-econom-generar.component.html',
  styleUrls: ['./datos-finan-econom-generar.component.css']
})
export class DatosFinanEconomGenerarComponent implements OnInit {

  impuestos: any;
  constructor(private datasetImpuestoService: DatasetFinanzasService, private dialogoInformacionService: DialogoInformacionService, private router: Router) { }


  generarDatasetImpuestos() {
    this.datasetImpuestoService.generarDatasetImpuestos().subscribe(data => {
      this.impuestos = data;

    });
    this.datasetImpuestoService.ExportarExcel();
    this.datasetImpuestoService.ExportarPDF();
    /*console.log('Los impuestos son', this.impuestos);*/
    this.dialogoInformacionService.open('Dataset Generado!', 'Se Generó el dataset de Impuestos en los siguientes 3 formatos: PDF, XLS y CSV.');
  }

  volver() {
    this.router.navigate(["/bienvenida"]);
  }

  volverAtras() {
    this.router.navigate(["/generacion-datasets"]);
  }

  ngOnInit() {
  }

  //ExportarExcel() {
  //  this.datasetImpuestoService.ExportarExcel();
  //}

  //ExportarPDF() {
  //  this.datasetImpuestoService.ExportarPDF();
  //}







}
