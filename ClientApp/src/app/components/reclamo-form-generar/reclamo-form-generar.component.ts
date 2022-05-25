import { Component, OnInit } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { VecinoService } from '../../services/vecino.service';

@Component({
  selector: 'reclamo-form-generar',
  templateUrl: './reclamo-form-generar.component.html',
  styleUrls: ['./reclamo-form-generar.component.css']
})
export class ReclamoFormGenerarComponent implements OnInit {
  TiposReclamo: any;
  nombreVecino: any;
  idVecino: any;
  Reclamo: FormGroup;
  constructor(private reclamoservice: ReclamoService, private vecinoService: VecinoService) {
    this.Reclamo = new FormGroup(
      {
        "Nro_Reclamo": new FormControl("0"),
        "codTipoReclamo": new FormControl("", [Validators.required]),
        "Descripcion": new FormControl("", [Validators.required, Validators.maxLength(2500)]),
        "Calle": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "entreCalles": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "Altura": new FormControl("", [Validators.required, Validators.maxLength(6)]),
        "Bhabilitado": new FormControl("1"),
        "idVecino": new FormControl("0"),
        "nombreVecino": new FormControl("")
      }
    );
  }

  ngOnInit() {
    this.reclamoservice.getTipoReclamo().subscribe(data => this.TiposReclamo = data);
    this.vecinoService.obtenerSessionNombreVecino().subscribe(nvecino => {

      this.Reclamo.controls["nombreVecino"].setValue(nvecino.valor);
      this.nombreVecino = nvecino.valor;
    });

    this.vecinoService.obtenerSessionidVecino().subscribe(idvecino => {
      // this.Reclamo.controls["idVecino"].setValue(idvecino.valor);
      this.idVecino = idvecino.valor;
    });

  }
  guardarDatos() {
    this.Reclamo.controls["idVecino"].setValue(this.idVecino);
    if (this.Reclamo.valid == true) {
      this.reclamoservice.agregarReclamo(this.Reclamo.value).subscribe(data => { });
    }
  }
  clickMethod() {
    alert("Su reclamo se ha generada correctamente");
    //Luego de presionar click debe redireccionar al home Iniciar aqui la creacion de un modal. 
  }

}







