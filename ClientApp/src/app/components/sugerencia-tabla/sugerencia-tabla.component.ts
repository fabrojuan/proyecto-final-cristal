import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { SugerenciaService } from '../../services/sugerencia.service';
import { UsuarioService } from '../../services/usuario.service';
import { Subject } from 'rxjs';
import espaniolDatatables from 'src/assets/espaniolDatatables.json';
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
  dtoptions: DataTables.Settings = {};
  
  dtTrigger: Subject<any> = new Subject<any>();
  cabeceras: string[] = ["Id Sugerencia", "Descripcion", "Fecha Generada", "Estado"];
  constructor(private sugerenciaservice: SugerenciaService, private usuarioService: UsuarioService, private formBuilder: UntypedFormBuilder) {
    this.form = new UntypedFormGroup({
      //'NombreUser': new FormControl("", Validators.required),
      //'Contrasenia': new FormControl("", Validators.required)
    });
  }

  form: UntypedFormGroup;  // Lo Comento porque en los grid no hay gestion de los campos
  ngOnInit() {
    this.dtoptions = {
      paging: false,
      lengthChange: false,
      pagingType: 'full_numbers',
      language: espaniolDatatables
    };
   
    this.sugerenciaservice.getSugerencia().subscribe(data => {
      this.Sugerencias = data;
      this.dtTrigger.next(null);
    });
    this.form = this.formBuilder.group({
      descripcion: '',
      tipoDenuncia: 0
    });

  }
 
}
