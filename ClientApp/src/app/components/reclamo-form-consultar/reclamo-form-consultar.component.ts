import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ReclamoService } from 'src/app/services/reclamo.service';
import { ReclamoRechazarDialogComponent } from './reclamo-rechazar-dialog.component';
import { AplicarAccion } from 'src/app/modelos_Interfaces/AplicarAccion';
import { ReclamoAsignarComponent } from '../reclamo-asignar/reclamo-asignar.component';
import { ObservacionesReclamoTablaComponent } from '../observaciones-reclamo-tabla/observaciones-reclamo-tabla.component';

@Component({
  selector: 'app-reclamo-form-consultar',
  templateUrl: './reclamo-form-consultar.component.html',
  styleUrls: ['./reclamo-form-consultar.component.css']
})
export class ReclamoFormConsultarComponent implements OnInit {

  nroReclamo: number = 0;
  reclamo : any = {}
  @ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any> | undefined;
  cambioEstadoReclamoForm: FormGroup;
  isCambioEstadoReclamoFormSubmitted: boolean=false;
  
  constructor(private _activatedRouter: ActivatedRoute,
              private _router: Router,
              private modalService: NgbModal,
              private reclamoService: ReclamoService) { 

    this._activatedRouter.params.subscribe(params => {
      this.nroReclamo = params['id'];
      this.reclamoService.getReclamo(this.nroReclamo).subscribe(datosReclamo => {
        console.log(datosReclamo);
        this.reclamo = datosReclamo;
      });
    });
    
    this.cambioEstadoReclamoForm = new FormGroup(
      {
        "nroReclamo": new FormControl(""),
        "codEstadoReclamoDestino": new FormControl(""),
        "observacionCambioEstado": new FormControl("", [Validators.required, Validators.maxLength(250)])
      }
    );

  }

  ngOnInit(): void {
  }

  rechazarReclamo() {

    const modalRef = this.modalService.open(ReclamoRechazarDialogComponent, 
      { animation: false, backdrop: "static", centered: true, keyboard: false, size: "lg" });
      
    modalRef.componentInstance.eventoRechazoConfirmado.subscribe((descripcionMotivoRechazo: string) => {
      modalRef.close();
      this.confirmarRechazo(descripcionMotivoRechazo);
    })

  }

  mostrarObservacionesReclamo() {
    const modalRef = this.modalService.open(ObservacionesReclamoTablaComponent, 
      { animation: false, backdrop: "static", centered: true, keyboard: false, size: "lg" });
    modalRef.componentInstance.nroReclamo = this.nroReclamo;
  }


  guardarCambioEstadoReclamo() {}

  asignarAreaReclamo() {
    const modalRef = this.modalService.open(ReclamoAsignarComponent, 
      { animation: false, backdrop: "static", centered: true, keyboard: false, size: "lg" });

    modalRef.componentInstance.eventoAsignacionConfirmado.subscribe((asignacion: any) => {
      modalRef.close();
      this.confirmarAsignacion(asignacion.codArea, asignacion.descripcionMotivoAsignacion);
    })
  }

  confirmarRechazo(descripcionMotivoRechazo : string) {
    if (descripcionMotivoRechazo) {
      let aplicarAccion: AplicarAccion = {};
      aplicarAccion.codAccion = "RECHAZAR";
      aplicarAccion.observacion = descripcionMotivoRechazo;
      this.reclamoService.aplicarAccion(this.nroReclamo, aplicarAccion).subscribe(resp => {
        alert("Se registró correctamente el rechazo del requerimiento");

        this.reclamoService.getReclamo(this.nroReclamo).subscribe(datosReclamo => {
          this.reclamo = datosReclamo;
        });

        return;
      }, error => {
        console.error(error);
        alert("Ocurrió un error al registrar el rechazo del requerimiento");
        return;
      });
      
    } else {
      alert("Debe ingresar el motivo del rechazo");
    }
    
  }

  confirmarAsignacion(nroArea: number, descripcionMotivoAsignacion : string) {
    if (nroArea) {

      if (nroArea == this.reclamo.nroArea) {
        alert("No se puede asignar el requerimiento a la misma área que tiene actualmente.");
        return;
      }

      console.log("Reclamo asignado al area " + nroArea + " por el siguiente motivo: " + descripcionMotivoAsignacion);
      let aplicarAccion: AplicarAccion = {};
      aplicarAccion.codAccion = "ASIGNAR";
      aplicarAccion.codArea = nroArea;
      aplicarAccion.observacion = descripcionMotivoAsignacion;
      this.reclamoService.aplicarAccion(this.nroReclamo, aplicarAccion).subscribe(resp => {
        alert("Se registró correctamente la asignación del requerimiento");

        this.reclamoService.getReclamo(this.nroReclamo).subscribe(datosReclamo => {
          this.reclamo = datosReclamo;
        });

        return;
      }, error => {
        console.error(error);
        alert("Ocurrió un error al registrar el rechazo del requerimiento");
        return;
      });


    } else {
      alert("Debe asignar una área");
    }

    
    
  }

  mostrarBotonRechazar() : boolean {
    if (this.reclamo.codEstadoReclamo == 1) {
      return true;
    }
    return false;
  }

  volver() {
    this._router.navigate(["/reclamo-tabla"]);
  }

}
