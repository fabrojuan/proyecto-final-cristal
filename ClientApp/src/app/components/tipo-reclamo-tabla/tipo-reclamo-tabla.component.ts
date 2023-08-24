import { Component, OnInit } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-tipo-reclamo-tabla',
  templateUrl: './tipo-reclamo-tabla.component.html',
  styleUrls: ['./tipo-reclamo-tabla.component.css']
})
export class TipoReclamoTablaComponent implements OnInit {

  tiposReclamo: any[] = [];
  codTipoReclamoEliminar!: number;
  modalRef: any;
  p: number = 1;
  mensajeWarning:String|undefined = "";
  procesoEjecutadoOk:boolean = false;
  procesoEjecutadoNoOk:boolean = false;
  mensajeOk:String = "";

  constructor(private _reclamoService: ReclamoService,
    private _router: Router,
    private _modalService: NgbModal) { }

  ngOnInit(): void {
    this._reclamoService.getTipoReclamo().subscribe(
      data => {
        this.tiposReclamo = data;
      }    
    );
  }

  public eliminarTipoReclamo(): void {
    this.procesoEjecutadoOk = false;
    this.procesoEjecutadoNoOk = false;
    this._reclamoService.eliminarTipoReclamo(this.codTipoReclamoEliminar).subscribe(
      data => {
      },
      error => {
        this.procesoEjecutadoNoOk = true;
        this.mensajeWarning = error.error;
      },
      () => {
        this.procesoEjecutadoOk = true;
        this.modalRef.close();
        this.recargarTabla();
        this.mensajeOk = 'Registro eliminado con éxito.';
      }
    );
  }

  verTipoReclamo(codTipoReclamo: number): void  {
    this._router.navigate(['/tipo-reclamo-form', codTipoReclamo]);
  }

  nuevoTipoReclamo(): void {
    this._router.navigate(['/tipo-reclamo-form']);
  }

  openModalEliminacion(content: any, codTipoReclamoEliminar: number) {
    this.codTipoReclamoEliminar = codTipoReclamoEliminar;
    this.modalRef = this._modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  cancelarModalEliminar() {
    this.modalRef.close();
  }

  private recargarTabla(): void {
    this._reclamoService.getTipoReclamo().subscribe(
      data => {
        this.tiposReclamo = data;
      }
    );
  }

}
