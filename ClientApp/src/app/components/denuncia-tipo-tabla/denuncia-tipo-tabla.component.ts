import { Component, OnInit, Input } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';

@Component({
  selector: 'denuncia-tipo-tabla',  //antes era: tabla-tipo-denuncia
  templateUrl: './denuncia-tipo-tabla.component.html',
  styleUrls: ['./denuncia-tipo-tabla.component.css']
})
export class DenunciaTipoTablaComponent implements OnInit {

  TiposDenuncia: any;
  cabeceras: string[] = ["Cód.", "Nombre", "Tiempo Máx. Tratamiento en Hs", "Descripción"];
  p: number = 1;
  constructor(private denunciaservice: DenunciaService) {
  }


  ngOnInit() {
    this.denunciaservice.getTipoDenuncia().subscribe(data => this.TiposDenuncia = data);

  }
}
