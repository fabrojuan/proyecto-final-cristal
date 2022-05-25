import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class PruebaGraficaService {
  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;

  }

  public ListarPruebasIniciales(idDenuncia: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/PruebaGrafica/ListarPruebasIniciales/' + idDenuncia).pipe(map(res => res));
  }

  //public agregarDenuncia(Denuncia) { Borrar este metodo que aparenteemente no se usa
  //  var url = this.urlBase + 'api/Denuncia/guardarDenuncia';
  //  return this.http.post(url, Denuncia).map(res => res.json());
  //}
}

