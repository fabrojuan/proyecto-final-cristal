import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { DenunciaService } from '../../services/denuncia.service';
import { UsuarioService } from '../../services/usuario.service';
import espaniolDatatables from 'src/assets/espaniolDatatables.json';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';


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
  p: number = 1;
  dtoptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  cabeceras: string[] = ["Id Denuncia", "Fecha Generada","Tipo Denuncia", "Estado Denuncia", "Prioridad", "Asignada a Empleado"];
  constructor(private denunciaservice: DenunciaService, private usuarioService: UsuarioService, private formBuilder: FormBuilder) {
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
    this.denunciaservice.getTipoDenuncia().subscribe(data => this.TiposDenuncia = data);
    this.usuarioService.getUsuarios().subscribe(data => this.Usuarios = data);
    this.form = this.formBuilder.group({
      descripcion: '',
      tipoDenuncia: 0
    });
    
  }
  //isTipoDenuncia(cabecera: string): boolean {
  //  console.log("thiene la cabecera" + this.cabecera);
  //  return this.cabecera === 'Tipo Denuncia';
   
  //}
}


