import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { LoteService } from '../../services/lote.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'lote-form-generar',
  templateUrl: './lote-form-generar.component.html',
  styleUrls: ['./lote-form-generar.component.css']
})
export class LoteFormGenerarComponent implements OnInit {
  TiposLote: any;
  public Lote: FormGroup;
  Rpta: number = 0;
  resultadoGuardadoModal: any = "";
  @ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any> | undefined;
  //Esta linea anterior es para el modal.

  constructor(private loteservice: LoteService, private router: Router, private modalService: NgbModal, private formBuilder: FormBuilder) {
    this.Lote = this.formBuilder.group(
      {
        "codTipoLote": new FormControl("", [Validators.required]),
        "Nomenclatura": new FormControl("", [Validators.required]),
        'Manzana': new FormControl("", [Validators.required, Validators.maxLength(2500)]),
        'NroLote': new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "Calle": new FormControl(""),
        "Altura": new FormControl(""),
        "Bhabilitado": new FormControl("1"),
        "Sup_Terreno": new FormControl(""),  //, [Validators.required,Validators.maxLength(100),Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]
        "Superficie_Edificada": new FormControl(""),
       }
    );
  }

  ngOnInit() {
    this.loteservice.getTipoLote().subscribe(data => this.TiposLote = data);
  }
  guardarDatos() {
    if (this.Lote.valid == true) {

      console.log(this.Lote.value);
      this.loteservice.agregarLote(this.Lote.value).subscribe(data => {
        if (data) {
          console.log(data);
          this.resultadoGuardadoModal = "Se ha generado la Lote correctamente, pronto le notificaremos acerca de la misma.";

        }
        else
          this.resultadoGuardadoModal = "La Lote no se ha podido registrar genere un ticket con el error en nuestra pestaña de problemas";
      });


      this.modalService.open(this.myModalInfo);
      this.router.navigate(["/"]);
    }
  }
  volverLoteTabla() {
    this.router.navigate(["/lote-tabla"]);
  }

}

