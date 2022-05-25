import { Component, OnInit } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';

@Component({
  selector: 'tabla-estado-denuncia',
  templateUrl: './denuncia-estado-tabla.component.html',
  styleUrls: ['./denuncia-estado-tabla.component.css']
})
export class TablaEstadoDenunciaComponent implements OnInit {
  EstadosDenuncia: any;
  cabeceras: string[] = ["Id Estado", "Nombre Estado", "Descripcion"];
  constructor(private denunciaservice: DenunciaService) {
  }

  ngOnInit() {
    this.denunciaservice.getEstadoDenuncia().subscribe(data => this.EstadosDenuncia = data);

  }

}


