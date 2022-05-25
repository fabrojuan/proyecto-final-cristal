import { Component, OnInit, Input } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';

@Component({
  selector: 'denuncia-tipo-tabla',  //antes era: tabla-tipo-denuncia
  templateUrl: './denuncia-tipo-tabla.component.html',
  styleUrls: ['./denuncia-tipo-tabla.component.css']
})
export class DenunciaTipoTablaComponent implements OnInit {

  TiposDenuncia: any;
  cabeceras: string[] = ["Id Tipo", "Nombre Tipo", "Tiempo Maximo Tratamiento en Hs", "Descripción"];
  constructor(private denunciaservice: DenunciaService) {
  }


  ngOnInit() {
    this.denunciaservice.getTipoDenuncia().subscribe(data => this.TiposDenuncia = data);

  }
}
