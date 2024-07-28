import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { LoteService } from '../../services/lote.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';


@Component({
  selector: 'detalle-lote',
  templateUrl: './detalle-lote.component.html',
  styleUrls: ['./detalle-lote.component.css']
})
export class DetalleLoteComponent implements OnInit {
  @Input() tituloInformacion: any; // Título del modal
  @Input() mensajeInformacion: any; // Mensaje del modal
  @Input() idLote: any; // nro de lote
  Lote: UntypedFormGroup;
  asfaltadobox: boolean;
  esquinabox: boolean;
  loteDeudorbox: boolean;
  constructor(public activeModal: NgbActiveModal, private loteService: LoteService) {
    this.asfaltadobox = true; //
    this.esquinabox = true; // 
    this.loteDeudorbox = true; // 


    this.Lote = new UntypedFormGroup(
      {
        "altura": new UntypedFormControl("0"),
        "calle": new UntypedFormControl("0"),
        "supEdificada": new UntypedFormControl("0"),
        "nomenclaturaCatastral": new UntypedFormControl("0"),
        "estadoDeuda": new UntypedFormControl(""),
        "dniTitular": new UntypedFormControl("0"),
        "esquina": new UntypedFormControl("0"),
        "asfaltado": new UntypedFormControl(''),
        "nombreLote": new UntypedFormControl(""),
        "propietarioLote": new UntypedFormControl("0"),

      } );

  }

  ngOnInit(){
    this.loteService.mostrarLoteById(this.idLote).subscribe(param => {
      this.Lote.controls["altura"].setValue(param.altura);
      this.Lote.controls["supEdificada"].setValue(param.supEdificada);
      this.Lote.controls["calle"].setValue(param.calle);
      this.Lote.controls["nomenclaturaCatastral"].setValue(param.nomenclaturaCatastral);
      //this.Lote.controls["DniTitular"].setValue(param.DniTitular);
      this.asfaltadobox = this.convertirABooleano(param.asfaltado);
      this.Lote.controls["nombreLote"].setValue(param.nombreLote);
      this.esquinabox = this.convertirABooleano(param.esquina);
      this.loteDeudorbox = this.convertirABooleano(param.estadoDeuda);
      this.Lote.controls["asfaltado"].setValue(this.asfaltadobox);
      this.Lote.controls["esquina"].setValue(this.esquinabox);
      this.Lote.controls["estadoDeuda"].setValue(this.loteDeudorbox);
    });

  }
// Función de conversión
convertirABooleano(valor: any): boolean {
  if (typeof valor === 'string') {
    return valor.toLowerCase() === 'true';
  } else if (typeof valor === 'number') {
    return valor !== 0;
  } else {
    return !!valor;
  }
}

}
