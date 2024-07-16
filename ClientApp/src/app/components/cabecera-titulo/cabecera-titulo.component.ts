import { Component, Input, OnInit } from '@angular/core';
import { TipoImagenEnum, Titulo } from 'src/app/modelos_Interfaces/Titulo';
import { TitulosServiceService } from 'src/app/services/titulos-service.service';

@Component({
  selector: 'app-cabecera-titulo',
  templateUrl: './cabecera-titulo.component.html',
  styleUrls: ['./cabecera-titulo.component.css']
})
export class CabeceraTituloComponent implements OnInit {

  @Input() nombreComponente: string = "";
  configuracionTitulo?: Titulo;

  constructor(private tituloService: TitulosServiceService) { }

  ngOnInit(): void {
    this.configuracionTitulo = this.tituloService.getConfiguracion(this.nombreComponente);
  }

  esIcono(): boolean {
    return this.configuracionTitulo?.tipoImagen == TipoImagenEnum.ICONO;
  }

  esFoto(): boolean {
    return this.configuracionTitulo?.tipoImagen == TipoImagenEnum.FOTO;
  }

}
