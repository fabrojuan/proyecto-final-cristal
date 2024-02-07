import { Component, OnInit } from '@angular/core';

import { DenunciaService } from '../../services/denuncia.service';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'tipo-denuncia-tabla',
  templateUrl: './tipo-denuncia-tabla.component.html',
  styleUrls: ['./tipo-denuncia-tabla.component.css']
})
export class TipoDenunciaTablaComponent implements OnInit {

  tiposDenuncia: any[] = [];
  codTipoDenunciaEliminar!: number;
  modalRef: any;
  p: number = 1;

  constructor(private _denunciaService: DenunciaService,
    private _router: Router,
    private _modalService: NgbModal,
    public _toastService: ToastService) { }

  ngOnInit(): void {
    this._denunciaService.getTipoDenuncia().subscribe(
      data => {
        this.tiposDenuncia = data;
      }
    );
  }

  public eliminarTipoDenuncia(): void {
    this._denunciaService.eliminarTipoDenuncia(this.codTipoDenunciaEliminar).subscribe(
      data => {
      },
      error => {
        this._toastService.show(error.error, { classname: 'bg-danger text-light', delay: 5000 });
      },
      () => {
        this.modalRef.close();
        this.recargarTabla();
        this._toastService.show('Registro eliminado con éxito.', { classname: 'bg-success text-light', delay: 5000 });
      }
    );
  }

  verTipoDenuncia(codTipoDenuncia: number): void {
    this._router.navigate(['/tipo-denuncia-form', codTipoDenuncia]);
  }

  nuevoTipoDenuncia(): void {
    this._router.navigate(['/tipo-denuncia-form']);
  }

  openModalEliminacion(content: any, codTipoDenunciaEliminar: number) {
    this.codTipoDenunciaEliminar = codTipoDenunciaEliminar;
    this.modalRef = this._modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  cancelarModalEliminar() {
    this.modalRef.close();
  }

  private recargarTabla(): void {
    this._denunciaService.getTipoDenuncia().subscribe(
      data => {
        this.tiposDenuncia = data;
      }
    );
  }

}



