import { HttpClientModule, HttpClient } from '@angular/common/http'
import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
//import 'rxjs/add/operator/map';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class DatasetFinanzasService {
  urlBase: string = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.urlBase = baseUrl;
  }

  public generarDatasetImpuestos() {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/generaImpuestoInmobiliarioMensual').pipe(map(res => res));

  }

  public ListarFinancieros() {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/ListarFinancieros').pipe(map(res => res));
  }



}



