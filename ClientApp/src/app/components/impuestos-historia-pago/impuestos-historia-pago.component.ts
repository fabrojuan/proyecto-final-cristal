import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ImpuestoService } from 'src/app/services/impuesto.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-impuestos-historia-pago',
  templateUrl: './impuestos-historia-pago.component.html',
  styleUrls: ['./impuestos-historia-pago.component.css']
})
export class ImpuestosHistoriaPagoComponent implements OnInit {

  parametro: any;
  titulo: any = "";
  impuestos: any;
  p: number = 1;
  suma_actual: number = 0;
  value: any;
  cabeceras: string[] = ["Fecha Pago", "Periodo", "Cuota", "Importe Pagado"];
  impuestosSeleccionados?: string;

  constructor(private impuestoService: ImpuestoService, private router: Router, private activatedRoute: ActivatedRoute,
              public _toastService: ToastService) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"];
    });
  }

  ngOnInit() {
    if (this.parametro >= 1) {
      this.impuestoService.listarHistorialPago(this.parametro).subscribe(data => this.impuestos = data);

    }
  }

  volver() {
    this.router.navigate(["/impuestos-vecino-adeuda-tabla/" + this.parametro]);
  }
  

  verHistorialPago() {
    this.router.navigate(["/impuesto-historial-pago/" + this.parametro]);
  }

}
