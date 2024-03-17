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
  //dtoptions: DataTables.LanguageSettings = {
  // espaniolDatatables  };
  
  dtTrigger: Subject<any> = new Subject<any>();
  cabeceras: string[] = ["Id Sugerencia", "Descripcion", "Fecha Generada", "Tiempo de Caducidad"];
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

  //const espaniolDatatables2: DataTables.LanguageSettings = {
  //  processing: "Procesando...",
  //  lengthMenu: "Mostrar _MENU_ registros",
  //  zeroRecords: "No se encontraron resultados",
  //  emptyTable: "Ningún dato disponible en esta tabla",
  //  info: "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
  //  infoEmpty: "Mostrando registros del 0 al 0 de un total de 0 registros",
  //  infoFiltered: "(filtrado de un total de _MAX_ registros)",
  //  search: "Buscar:",
  //  thousands: ",",
  //  loadingRecords: "Cargando...",
  //  paginate: {
  //    first: "Primero",
  //    last: "Último",
  //    next: "Siguiente",
  //    previous: "Anterior"
  //  },
  //  aria: {
  //    sortAscending: ": Activar para ordenar la columna de manera ascendente",
  //    sortDescending: ": Activar para ordenar la columna de manera descendent"
  //  },
  //  //buttons: {
  //  //  oncopy: "Copiar",
  //  //  colvis: "Visibilidad"

  //  //}
  //};
 
}

  
