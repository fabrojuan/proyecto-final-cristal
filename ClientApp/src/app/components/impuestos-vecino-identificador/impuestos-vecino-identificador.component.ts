import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, FormBuilder, Validators, UntypedFormGroup } from '@angular/forms';
//import { resolve } from 'url';
import { NgModule } from '@angular/core';
import { LoteService } from 'src/app/services/lote.service';

@Component({
  selector: 'impuestos-vecino-identificador',
  templateUrl: './impuestos-vecino-identificador.component.html',
  styleUrls: ['./impuestos-vecino-identificador.component.css']
})
export class ImpuestosVecinoIdentificadorComponent implements OnInit {

  formulario: UntypedFormGroup;
  isFormSubmitted: boolean=false;
  
  constructor(private _router: Router) {

    this.formulario = new FormGroup(
      {
        "nroCuenta": new FormControl("", [Validators.required])
      }
    );

  }

  ngOnInit() {
  }

  public volverHome()
  {
  }

  get nroCuentaNoValido() {
    return this.isFormSubmitted && this.formulario.controls.nroCuenta.errors;
  }

  irVerImpuestos() {
    this.isFormSubmitted = true;

    if (this.formulario.invalid) {
      Object.values(this.formulario.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    } else {
      this._router.navigate(['/impuestos-vecino-adeuda-tabla', this.formulario.controls.nroCuenta.value]);      
    }
  }

}
