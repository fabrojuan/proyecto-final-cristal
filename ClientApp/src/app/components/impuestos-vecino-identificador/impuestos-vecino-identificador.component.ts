import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
//import { resolve } from 'url';
import { NgModule } from '@angular/core';

@Component({
  selector: 'impuestos-vecino-identificador',
  templateUrl: './impuestos-vecino-identificador.component.html',
  styleUrls: ['./impuestos-vecino-identificador.component.css']
})
export class ImpuestosVecinoIdentificadorComponent implements OnInit {

  nroCuenta: any = 0;
  constructor() {

  }

  ngOnInit() {
  }

  public volverHome()
  {
  }

}
