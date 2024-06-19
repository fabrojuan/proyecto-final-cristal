import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ReclamoService } from 'src/app/services/reclamo.service';
import { ReclamoRechazarDialogComponent } from './reclamo-rechazar-dialog.component';
import { AplicarAccion } from 'src/app/modelos_Interfaces/AplicarAccion';
import { ReclamoAsignarComponent } from '../reclamo-asignar/reclamo-asignar.component';
import { ObservacionesReclamoTablaComponent } from '../observaciones-reclamo-tabla/observaciones-reclamo-tabla.component';
import { TrabajoReclamoFormGenerarComponent } from '../trabajo-reclamo-form-generar/trabajo-reclamo-form-generar.component';
import { TrabajosReclamoTablaComponent } from '../trabajos-reclamo-tabla/trabajos-reclamo-tabla.component';

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
  opcionesReclamo: any;
  
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
      this.reclamoService.getOpcionesReclamo(this.nroReclamo).subscribe(opciones => {
        this.opcionesReclamo = opciones;
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

  mostrarTrabajosReclamo() {
    const modalRef = this.modalService.open(TrabajosReclamoTablaComponent, 
      { animation: false, backdrop: "static", centered: true, keyboard: false, size: "lg" });
    modalRef.componentInstance.nroReclamo = this.nroReclamo;
  }

  asignarAreaReclamo() {
    const modalRef = this.modalService.open(ReclamoAsignarComponent, 
      { animation: false, backdrop: "static", centered: true, keyboard: false, size: "lg" });

    modalRef.componentInstance.eventoAsignacionConfirmado.subscribe((asignacion: any) => {
      modalRef.close();
      this.confirmarAsignacion(asignacion.codArea, asignacion.descripcionMotivoAsignacion);
    })
  }

  cargarTrabajoReclamo() {
    const modalRef = this.modalService.open(TrabajoReclamoFormGenerarComponent, 
      { animation: false, backdrop: "static", centered: true, keyboard: false, size: "lg" });

    modalRef.componentInstance.eventoCargaTrabajo.subscribe((asignacion: any) => {
       modalRef.close();
       this.confirmarCargaTrabajo(asignacion.fechaTrabajo, asignacion.descripcionTrabajo);
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
        this.reclamoService.getOpcionesReclamo(this.nroReclamo).subscribe(opciones => {
          this.opcionesReclamo = opciones;
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
        this.reclamoService.getOpcionesReclamo(this.nroReclamo).subscribe(opciones => {
          this.opcionesReclamo = opciones;
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

  confirmarCargaTrabajo(fechaTrabajo: string, descripcionTrabajo : string) {
    if (fechaTrabajo && fechaTrabajo.length > 0 && descripcionTrabajo && descripcionTrabajo.length > 0) {
      this.reclamoService.guardarTrabajoReclamo(this.nroReclamo, 
                                                {"fechaTrabajo": fechaTrabajo, "descripcion": descripcionTrabajo})
        .subscribe(resp => {
          alert("Se registró correctamente la carga del trabajo");                                         
          return;
        }, error => {
          console.error(error);
          alert("Ocurrió un error al registrar el trabajo del requerimiento");
          return;
        });
    } else {
      alert("Debe indicar una fecha y descripción del trabajo");
    }
  }

  volver() {
    this._router.navigate(["/reclamo-tabla"]);
  }

}
