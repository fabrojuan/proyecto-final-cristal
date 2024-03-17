import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { DenunciaService } from '../../services/denuncia.service';
import { UsuarioService } from '../../services/usuario.service';
@Component({
  selector: 'historico-denuncia-tabla',
  templateUrl: './historico-denuncia-tabla.component.html',
  styleUrls: ['./historico-denuncia-tabla.component.css']
}) export class HistoricoDenunciaTablaComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  Denuncias: any;
  Usuarios: any;
  TiposDenuncia: any;
  DenunciasFiltradas: any;
  p: number = 1;
  cabeceras: string[] = ["Id Denuncia", "Fecha Generada", "Tipo Denuncia", "Estado Denuncia", "Prioridad", "Cerrada por"];
  constructor(private denunciaservice: DenunciaService, private usuarioService: UsuarioService, private formBuilder: UntypedFormBuilder) {
    this.form = new UntypedFormGroup({
      //'NombreUser': new FormControl("", Validators.required),
      //'Contrasenia': new FormControl("", Validators.required)
    });
  }

  form: UntypedFormGroup;  // Lo Comento porque en los grid no hay gestion de los campos
  ngOnInit() {

    this.denunciaservice.ListarDenunciasCerradas().subscribe(data => this.Denuncias = data);
    this.denunciaservice.getTipoDenuncia().subscribe(data => this.TiposDenuncia = data);
    this.usuarioService.getUsuarios().subscribe(data => this.Usuarios = data);
    this.form = this.formBuilder.group({
      descripcion: '',
      tipoDenuncia: 0
    });

  }

}

