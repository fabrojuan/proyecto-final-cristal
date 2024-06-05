import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { LoteService } from '../../services/lote.service';
import { VecinoService } from '../../services/vecino.service';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import espaniolDatatables from 'src/assets/espaniolDatatables.json';

@Component({
  selector: 'lote-tabla',
  templateUrl: './lote-tabla.component.html',
  styleUrls: ['./lote-tabla.component.css']

})
export class LoteTablaComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  Lotes: any;
  Usuarios: any;
  TiposDenuncia: any;
  DenunciasFiltradas: any;
  p: number = 1;
  dtoptions: DataTables.Settings = {};

  dtTrigger: Subject<any> = new Subject<any>();

  cabeceras: string[] = ["Id Lote", "Nro Lote" ,"Manzana" , "Superficie Terreno", "Base Imponible", "Valuacion Total", "Propietario"];
  constructor(private loteservice: LoteService, private vecinoService: VecinoService, private formBuilder: UntypedFormBuilder, private router: Router) {
    this.form = new UntypedFormGroup({
     
    });
  }

  form: UntypedFormGroup;  // Lo Comento porque en los grid no hay gestion de los campos
  ngOnInit() {
    this.dtoptions = {
      paging: false,
      lengthChange: true,
      pagingType: 'full_numbers',
      language: espaniolDatatables
    };

    this.loteservice.getLote().subscribe(data => {
      this.Lotes = data;
      this.dtTrigger.next(null);
    });
     
    
    //this.form = this.formBuilder.group({
    //  descripcion: '',
    //  tipoDenuncia: 0
    //});

  }
  volverHome() {
    this.router.navigate(["/lotesypersonas"]);
  }

}


