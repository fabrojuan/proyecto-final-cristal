import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { SugerenciaService } from '../../services/sugerencia.service';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'sugerencia-tabla',
  templateUrl: './sugerencia-tabla.component.html',
  styleUrls: ['./sugerencia-tabla.component.css']
})
export class SugerenciaTablaComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  Sugerencias: any;
  DenunciasFiltradas: any;
  p: number = 1;
  cabeceras: string[] = ["Id Sugerencia", "Descripcion", "Fecha Generada", "Tiempo de Caducidad"];
  constructor(private sugerenciaservice: SugerenciaService, private usuarioService: UsuarioService, private formBuilder: FormBuilder) {
    this.form = new FormGroup({
      //'NombreUser': new FormControl("", Validators.required),
      //'Contrasenia': new FormControl("", Validators.required)
    });
  }

  form: FormGroup;  // Lo Comento porque en los grid no hay gestion de los campos
  ngOnInit() {

    this.sugerenciaservice.getSugerencia().subscribe(data => this.Sugerencias = data);
    this.form = this.formBuilder.group({
      descripcion: '',
      tipoDenuncia: 0
    });

  }

}

