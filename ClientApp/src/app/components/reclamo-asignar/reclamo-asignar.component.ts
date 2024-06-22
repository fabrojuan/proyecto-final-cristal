import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Area } from 'src/app/modelos_Interfaces/Area';
import { AreasService } from 'src/app/services/areas.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-reclamo-asignar',
  templateUrl: './reclamo-asignar.component.html',
  styleUrls: []
})
export class ReclamoAsignarComponent implements OnInit {

  @Output() eventoAsignacionConfirmado: EventEmitter<any> = new EventEmitter();
  descripcionMotivoAsignacion : string = "";
  areas: Area[] = []; 
  areaSelected: number = 0;

  constructor(public activeModal: NgbActiveModal,
              private _areasService: AreasService,
              public _toastService: ToastService
  ) { }

  ngOnInit(): void {
    this._areasService.getAreas().subscribe(
      data => {
        this.areas = data;
        console.log(data);
        console.log(this.areas);
      }
    );
  }

  confirmarAsignacion() {

    if (this.areaSelected == 0) {
      this._toastService.showError("Debe asignar un área");
      return;
    }

    this.eventoAsignacionConfirmado.emit( {
      codArea: this.areaSelected,
      descripcionMotivoAsignacion: this.descripcionMotivoAsignacion   
    });
  }

  cancelarAsignacion() {
    this.activeModal.dismiss();
  }

}
