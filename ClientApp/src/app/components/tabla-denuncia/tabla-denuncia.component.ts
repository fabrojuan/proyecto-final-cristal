import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { DenunciaService } from '../../services/denuncia.service';
import { UsuarioService } from '../../services/usuario.service';
@Component({
  selector: 'tabla-denuncia',
  templateUrl: './tabla-denuncia.component.html',
  styleUrls: ['./tabla-denuncia.component.css']
})
export class TablaDenunciaComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  Denuncias: any;
  Usuarios: any;
  TiposDenuncia: any;
  DenunciasFiltradas: any;
  p: number = 1;
  cabeceras: string[] = ["Id Denuncia", "Fecha Generada", "Tipo Denuncia", "Estado Denuncia", "Prioridad", "Asignada a Empleado"];
  constructor(private denunciaservice: DenunciaService, private usuarioService: UsuarioService, private formBuilder: FormBuilder) {
    this.form = new FormGroup({
      //'NombreUser': new FormControl("", Validators.required),
      //'Contrasenia': new FormControl("", Validators.required)
    });
  }
   
  form: FormGroup;  // Lo Comento porque en los grid no hay gestion de los campos
  ngOnInit() {

    this.denunciaservice.getDenuncia().subscribe(data => this.Denuncias = data);
    this.denunciaservice.getTipoDenuncia().subscribe(data => this.TiposDenuncia = data);
    this.usuarioService.getUsuario().subscribe(data => this.Usuarios = data);
    this.form = this.formBuilder.group({
      descripcion: '',
      tipoDenuncia: 0
    });

  }

}


