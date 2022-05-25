import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImpuestoService } from '../../services/impuesto.service';

@Component({
  selector: 'impuestos-vecino-adeuda-tabla',
  templateUrl: './impuestos-vecino-adeuda-tabla.component.html',
  styleUrls: ['./impuestos-vecino-adeuda-tabla.component.css']
})
export class ImpuestosVecinoAdeudaTablaComponent implements OnInit {
  parametro: any;
  titulo: any = "";
  impuestos: any;
  p: number = 1;
  FGimpuestos: any;
  suma_actual: number = 0;
  value: any;
  cabeceras: string[] = ["Mes", "Año", "Importe Base", "Interés Acumulado", "Importe Final", "Seleccionado"];
  constructor(private impuestoService: ImpuestoService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"]
      if (this.parametro >= 1) {
        this.titulo = "Editar";
        console.log('El id de lote es' + this.parametro);

      } else {
        this.titulo = "Añadir";
      }
    });
    this.FGimpuestos = new FormGroup(
      {
        "Valores": new FormControl(""),

      }
    );

  }

  ngOnInit() {
    if (this.parametro >= 1) {
      this.impuestoService.ListarImpuestosAdeudados(this.parametro).subscribe(data => this.impuestos = data);

    }
  }

  volver() {
    this.router.navigate(["/impuestos-vecino-identificador"]);
  }

  verCheck() {
    var seleccionados = "";
    var noseleccionados = "";
    var checks = document.getElementsByClassName("check");
    var check:any;  //Agrego un any a la variable check y no jodió mas.
    this.suma_actual = 0;
    for (var i = 0; i < checks.length; i++) {
      check = checks[i];
      if (check.checked == true) {
        seleccionados += check.name; //Saco el id de la paginas sleeccionadas en el html hay una prop que se llama name=pagina.idPagina
        seleccionados += "-"; //separo los id de cada uno con un guion.
        this.suma_actual = this.suma_actual + parseInt(check.value);
      }
      else {
        if (check.checked == false) {
          noseleccionados += check.name;
          noseleccionados += "-";
        }
      }
    }

    if (seleccionados != "") {
      seleccionados = seleccionados.substring(0, seleccionados.length - 1)  //Aqui elimino el utlimo caracter de seleccionados que eá un - y el ultimo tiene que ser un nro.
      this.FGimpuestos.controls["Valores"].setValue(seleccionados);
    }
  }
  guardarDatos() {
    if (this.FGimpuestos.valid == true) {
      //  this.respuesta =
      this.impuestoService.guardarBoleta(this.FGimpuestos.value).subscribe(data => {
        this.router.navigate(["/"]);
      })
      this.router.navigate(["/impuesto-pago-send"]);
      //  if (this.respuesta == 0) {
      //    console.log("No se guardo correcto hubo error");
    }
    else {
      // console.log("Se guardo Joya!!!");
      this.router.navigate(["/"]);
    }
  }



}
