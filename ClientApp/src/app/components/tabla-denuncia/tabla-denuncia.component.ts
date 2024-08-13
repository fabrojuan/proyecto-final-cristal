import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { DenunciaService } from '../../services/denuncia.service';
import { UsuarioService } from '../../services/usuario.service';
import espaniolDatatables from 'src/assets/espaniolDatatables.json';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpParams } from '@angular/common/http'; //para los filtros
import { Router } from '@angular/router';



@Component({
  selector: 'tabla-denuncia',
  templateUrl: './tabla-denuncia.component.html',
  styleUrls: ['./tabla-denuncia.component.css']
})
export class TablaDenunciaComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  Denuncias: any;
  public form: FormGroup;
  Usuarios: any;
  TiposDenuncia: any;
  DenunciasFiltradas: any;
  EstadosDenuncia: any;
  p: number = 1;
  //filtros
  nroReclamoFiltro: string = ''; //este debo reemplazarlo por algo de la denuncia aca y en el html
  tipoDenunciaSeleccionado: number = 0;
  estadoDenunciaSeleccionado: number = 0;
  nomApeVecinoFiltro: string = '';
  //fin filtros

  dtoptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  cabeceras: string[] = ["Id Denuncia", "Fecha Generada","Tipo Denuncia", "Estado Denuncia", "Prioridad", "Asignada a Empleado"];
  constructor(private denunciaservice: DenunciaService, private router: Router, private usuarioService: UsuarioService, private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group(
      {
        Tipo_Denuncia: new FormControl(""),
      codTipoDenuncia: new FormControl("")
    });
  }
   
  //form: UntypedFormGroup;  // Lo Comento porque en los grid no hay gestion de los campos
  ngOnInit() {

    this.dtoptions = {
      paging: false,
      lengthChange: false,
      pagingType: 'full_numbers',
      language: espaniolDatatables
    };
    this.denunciaservice.getDenuncia().subscribe(data => {
      this.Denuncias = data
      this.dtTrigger.next(null);
    });
    this.denunciaservice.getEstadoDenuncia().subscribe(data => this.EstadosDenuncia = data);
    this.denunciaservice.getTipoDenuncia().subscribe(data => this.TiposDenuncia = data);
    this.usuarioService.getUsuarios().subscribe(data => this.Usuarios = data);
    this.form = this.formBuilder.group({
      descripcion: '',
      tipoDenuncia: 0
    });
    
  }

  //FILTROSSSSS
  aplicarFiltrado() {
    let queryParams = new HttpParams();

    if (this.estadoDenunciaSeleccionado && this.estadoDenunciaSeleccionado != 0) {
      queryParams = queryParams.append("estado", this.estadoDenunciaSeleccionado);
    }

    if (this.tipoDenunciaSeleccionado && this.tipoDenunciaSeleccionado != 0) {
      queryParams = queryParams.append("tipo", this.tipoDenunciaSeleccionado);
    }

    //if (this.nroReclamoFiltro && this.nroReclamoFiltro.length != 0) {
    //  queryParams = queryParams.append("numero", this.nroReclamoFiltro);
    //}

    //if (this.nomApeVecinoFiltro && this.nomApeVecinoFiltro.length != 0) {
    //  queryParams = queryParams.append("nom_ape_vecino", this.nomApeVecinoFiltro);
    //}

    this.denunciaservice.getDenunciasConFiltros(queryParams).subscribe(data => {
      this.Denuncias = data;
      this.DenunciasFiltradas = data;
    });


  }

  volver() {
    //this.mostrarModal = false; // Ocultar el modal al navegar
    this.router.navigate(["/bienvenida"]);
  }
  //reeemplazar por algun filtro de denuncia tambien
  aplicarFiltroNroReclamo(event: any) {
    this.aplicarFiltrado();
  }
}


