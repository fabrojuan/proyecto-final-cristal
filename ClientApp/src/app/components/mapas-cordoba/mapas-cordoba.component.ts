import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'mapas-cordoba',
  templateUrl: './mapas-cordoba.component.html',
  styleUrls: ['./mapas-cordoba.component.css']
})
export class MapasCordobaComponent implements OnInit {
  name = 'Set iframe source';
  url: string = "https://gn-idecor.mapascordoba.gob.ar/maps/3/embed?latitud=-64.356560&longitud=-31.590448&zoom=18&layers=true&info=true";
  urlSafe: SafeResourceUrl | undefined;  //añadimos undefined ya que no inicializamos el parametro.

  constructor(public sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.urlSafe = this.sanitizer.bypassSecurityTrustResourceUrl(this.url);
  }

}
