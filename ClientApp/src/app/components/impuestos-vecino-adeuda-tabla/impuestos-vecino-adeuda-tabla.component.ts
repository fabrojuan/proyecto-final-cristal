import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImpuestoService } from '../../services/impuesto.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'impuestos-vecino-adeuda-tabla',
  templateUrl: './impuestos-vecino-adeuda-tabla.component.html',
  styleUrls: []
})
export class ImpuestosVecinoAdeudaTablaComponent implements OnInit {
  parametro: any;
  impuestos: any[] = [];
  p: number = 1;
  suma_actual: number = 0;
  value: any;
  cabeceras: string[] = ["Periodo", "Cuota", "Vencimiento", "Nominal", "Recargo", "Importe Final"];
  idsImpuestosSeleccionados?: string;
  opcionSeleccionarTodo: boolean = false;

  constructor(private impuestoService: ImpuestoService, private router: Router, private activatedRoute: ActivatedRoute,
              public _toastService: ToastService) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"]
    });
  }

  ngOnInit() {
    if (this.parametro >= 1) {
      this.impuestoService.ListarImpuestosAdeudados(this.parametro).subscribe(data => this.impuestos = data);
    }
  }

  volver() {
    this.router.navigate(["/impuestos-vecino-identificador"]);
  }

  seleccionarImpuesto() {
    this.recalcularImporteTotalAPagar();
  }

  seleccionarTodos() {
    this.impuestos.forEach(item => item.estaSeleccionado = this.opcionSeleccionarTodo);
    this.recalcularImporteTotalAPagar();
  }

  recalcularImporteTotalAPagar() {
    let sumaImpuestosSeleccionados : number = 0;    
    this.impuestos.filter(item => item.estaSeleccionado).forEach(item => sumaImpuestosSeleccionados += item.importeFinal);
    this.suma_actual = sumaImpuestosSeleccionados;
  }
  
  guardarDatos() {

    if (this.suma_actual == 0) {
      this._toastService.showError("Debe seleccionar algún impuesto");
      return;
    }

    if (this.impuestos.filter(item => item.estaSeleccionado && item.mes == 0).length > 0
        && this.impuestos.filter(item => item.estaSeleccionado && item.mes != 0).length > 0) {
        this._toastService.showError("No se puede pagar la cuota Única y las mensuales juntas");
        return;
      }
   

    this.idsImpuestosSeleccionados = this.impuestos.filter(item => item.estaSeleccionado).map(item => item.idImpuesto).join("-");
    this.impuestoService.guardarBoleta({"Valores": this.idsImpuestosSeleccionados, "idLote": this.parametro}).subscribe(data => {
      this.router.navigate(["/"]);
    })
    this.router.navigate(["/impuesto-pago-send"]);

  }

  verHistorialPago() {
    this.router.navigate(["/impuesto-historial-pago/" + this.parametro]);
  }

}
