import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { SugerenciaService } from '../../services/sugerencia.service';
import { UsuarioService } from '../../services/usuario.service';
import { Subject } from 'rxjs';
import espaniolDatatables from 'src/assets/espaniolDatatables.json';
import { ToastService } from 'src/app/services/toast.service';
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
  cabeceras: string[] = ["Número", "Descripción", "Fecha", "Estado"];
  constructor(private sugerenciaservice: SugerenciaService, private usuarioService: UsuarioService, private formBuilder: UntypedFormBuilder,
              public _toastService: ToastService
  ) {
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
   
    this.recargarTablaSugerencias();
    
    this.form = this.formBuilder.group({
      descripcion: '',
      tipoDenuncia: 0
    });

  }

   mostrarOpcionCrearRequerimiento(estado: string) {
    return estado == "Considerado";
   }

   mostrarOpcionConsiderarSugerencia(estado: string) {
    return estado == "Registrado" || estado == "Descartado" 
   }

   mostrarOpcionDescartarSugerencia(estado: string) {
    return estado == "Registrado" || estado == "Considerado"
   }

   considerarSugerencia(idSugerencia: number) {
    let sugerencia: any;
    this.sugerenciaservice.getSugerenciaById(idSugerencia)
      .subscribe(data => {
        sugerencia = data;
        sugerencia.codestado = 2;

        this.sugerenciaservice.actualizarSugerencia(sugerencia).subscribe(resp => {
          this._toastService.showOk("Se actualizó el estado de la sugerencia con éxito");
          this.recargarTablaSugerencias();
        }, error => {
          this._toastService.showError("Ocurrió un error al actualizar el estado de la sugerencia");
        })

      }, error => {
        this._toastService.showError("Ocurrió un error al obtener los datos de la sugerencia");
      })
   }

   descartarSugerencia(idSugerencia: number) {
    let sugerencia: any;
    this.sugerenciaservice.getSugerenciaById(idSugerencia)
      .subscribe(data => {
        sugerencia = data;
        sugerencia.codestado = 3;

        this.sugerenciaservice.actualizarSugerencia(sugerencia).subscribe(resp => {
          this._toastService.showOk("Se actualizó el estado de la sugerencia con éxito");
          this.recargarTablaSugerencias();
        }, error => {
          this._toastService.showError("Ocurrió un error al actualizar el estado de la sugerencia");
        })

      }, error => {
        this._toastService.showError("Ocurrió un error al obtener los datos de la sugerencia");
      })
   }

   recargarTablaSugerencias() {
      this.sugerenciaservice.getSugerencia().subscribe(data => {
        console.log(data);
        this.Sugerencias = data;
        this.dtTrigger.next(null);
      });
   }
 
}
