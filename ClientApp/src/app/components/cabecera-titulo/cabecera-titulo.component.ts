import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'cabecera-titulo',
  templateUrl: './cabecera-titulo.component.html',
  styleUrls: ['./cabecera-titulo.component.css']
})
export class CabeceraTituloComponent implements OnInit {

  @Input() titulo: string = "";
  @Input() rutaImagen: string = "";
  @Input() link: string = "";
  @Input() tooltipIcono: string = "";
  linkFinal: string = ""

  constructor() { }

  ngOnInit(): void {
    this.linkFinal = "/" + this.link
  }

}
