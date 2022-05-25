import { Component, OnInit } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IndicadoresService } from '../../services/indicadores.service';

@Component({
  selector: 'bienvenida-vecino',
  templateUrl: './bienvenida-vecino.component.html',
  styleUrls: ['./bienvenida-vecino.component.css']
})
export class BienvenidaVecinoComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
