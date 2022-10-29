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
        "Descripcion": new FormControl("", [Validators.required, Validators.maxLength(200)]),
        "Calle": new FormControl("", [Validators.required, Validators.maxLength(50)]),
        "entreCalles": new FormControl("", [Validators.required, Validators.maxLength(50)]),
        "Altura": new FormControl("", [Validators.required, Validators.maxLength(6)]),
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
    if (this.Reclamo.invalid) {
      Object.values(this.Reclamo.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    }

    alert("Su reclamo se ha generada correctamente");
    //Luego de presionar click debe redireccionar al home Iniciar aqui la creacion de un modal. 
  }

  get codTipoReclamoNoValido() {
    return this.Reclamo.get('codTipoReclamo')?.invalid && this.Reclamo.get('codTipoReclamo')?.touched;
  }

  get calleNoValido() {
    return this.Reclamo.get('Calle')?.invalid && this.Reclamo.get('Calle')?.touched;
  }

  get alturaNoValido() {
    return this.Reclamo.get('Altura')?.invalid && this.Reclamo.get('Altura')?.touched;
  }

  get entreCallesNoValido() {
    return this.Reclamo.get('entreCalles')?.invalid && this.Reclamo.get('entreCalles')?.touched;
  }

  get descripcionNoValido() {
    return this.Reclamo.get('Descripcion')?.invalid && this.Reclamo.get('Descripcion')?.touched;
  }

}







