import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ReclamoService } from 'src/app/services/reclamo.service';
import { ReclamoRechazarDialogComponent } from './reclamo-rechazar-dialog.component';
import { AplicarAccion } from 'src/app/modelos_Interfaces/AplicarAccion';

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


  guardarCambioEstadoReclamo() {}

  asignarAreaReclamo() {}

  confirmarRechazo(descripcionMotivoRechazo : string) {
    if (descripcionMotivoRechazo) {
      let aplicarAccion: AplicarAccion = {};
      aplicarAccion.codAccion = "RECHAZAR";
      aplicarAccion.observacion = descripcionMotivoRechazo;
      this.reclamoService.aplicarAccion(this.nroReclamo, aplicarAccion).subscribe(resp => {
        alert("Se registró correctamente el rechazo del reclamo");
        return;
      }, error => {
        console.error(error);
        alert("Ocurrió un error al registrar el rechazo del reclamo");
        return;
      });
      
    } else {
      alert("Debe ingresar el motivo del rechazo");
    }
    
  }

  mostrarBotonRechazar() : boolean {
    if (this.reclamo.codEstado == 1) {
      return true;
    }
    return false;
  }

}
