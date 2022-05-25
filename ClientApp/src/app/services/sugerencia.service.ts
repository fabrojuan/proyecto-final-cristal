import { HttpClientModule, HttpClient } from '@angular/common/http'
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()

export class SugerenciaService {
  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;

  }

  public agregarSugerencia(Sugerencia: any) {

    var url = this.urlBase + 'api/Sugerencia/guardarSugerencia';
    return this.http.post(url, Sugerencia).pipe(map(res => res));
  }
  public getSugerencia() {
    return this.http.get(this.urlBase + 'api/Sugerencia/listarSugerencias').pipe(map(res => res));
  }



}

//  public getEstadoDenuncia() {
//    return this.http.get(this.urlBase + 'api/Denuncia/listarEstadosDenuncia').map(res => res.json());
//  }

//}
