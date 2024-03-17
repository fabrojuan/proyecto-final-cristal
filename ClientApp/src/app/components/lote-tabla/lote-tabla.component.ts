import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { LoteService } from '../../services/lote.service';
import { VecinoService } from '../../services/vecino.service';
import { Router } from '@angular/router';

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
  cabeceras: string[] = ["Id Lote", "Manzana", "Nro Lote", "Superficie Terreno", "Base Imponible", "Valuacion Total", "Propietario"];
  constructor(private loteservice: LoteService, private vecinoService: VecinoService, private formBuilder: UntypedFormBuilder, private router: Router) {
    this.form = new UntypedFormGroup({
     
    });
  }

  form: UntypedFormGroup;  // Lo Comento porque en los grid no hay gestion de los campos
  ngOnInit() {

    this.loteservice.getLote().subscribe(data => this.Lotes = data);
    //this.denunciaservice.getTipoDenuncia().subscribe(data => this.TiposDenuncia = data);
    //this.usuarioService.getUsuario().subscribe(data => this.Usuarios = data);
    this.form = this.formBuilder.group({
      descripcion: '',
      tipoDenuncia: 0
    });

  }
  volverHome() {
    this.router.navigate(["/"]);
  }

}


