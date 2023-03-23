import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { LoteService } from '../../services/lote.service';
import { VecinoService } from '../../services/vecino.service';
import { Router } from '@angular/router';

@Component({
  selector: 'personas-tabla',
  templateUrl: './personas-tabla.component.html',
  styleUrls: ['./personas-tabla.component.css']

})
export class PersonasTablaComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  Personas: any;
  //Usuarios: any;
  //TiposDenuncia: any;
  //DenunciasFiltradas: any;
  p: number = 1;
  cabeceras: string[] = ["Nombre", "Apellido", "Dni", "Telefono", "Mail", "Calle", "Nro"];
  constructor(private loteservice: LoteService, private vecinoService: VecinoService, private formBuilder: FormBuilder, private router: Router) {
    this.form = new FormGroup({

    });
  }

  form: FormGroup;  // Lo Comento porque en los grid no hay gestion de los campos
  ngOnInit() {

    
    this.vecinoService.getvecino().subscribe(data => this.Personas = data);
    //this.denunciaservice.getTipoDenuncia().subscribe(data => this.TiposDenuncia = data);
    //this.usuarioService.getUsuario().subscribe(data => this.Usuarios = data);
    this.form = this.formBuilder.group({
      //descripcion: '',
     // tipoDenuncia: 0
    });

  }
  volverHome() {
    this.router.navigate(["/bienvenida"]);
  }

}



