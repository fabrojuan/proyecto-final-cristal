import { HttpClientModule, HttpClient } from '@angular/common/http'
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class SugerenciaService {
  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;

  }

  public agregarSugerencia(Sugerencia: any) {

    var url = this.urlBase + 'api/Sugerencia/guardarSugerencia';
    return this.http.post(url, Sugerencia).pipe(map(res => res));
  }

  public actualizarSugerencia(sugerencia: any) {

    var url = this.urlBase + 'api/Sugerencia/actualizarSugerencia';
    return this.http.put(url + "/" + sugerencia.idSugerencia, sugerencia).pipe(map(res => res));
  }

  public getSugerencia() {
    return this.http.get(this.urlBase + 'api/Sugerencia/listarSugerencias').pipe(map(res => res));
  }

  public getSugerenciaById(idSugerencia: number): Observable<any>  {
    return this.http.get(this.urlBase + 'api/Sugerencia/' + idSugerencia).pipe(map(res => res));
  }



}

//  public getEstadoDenuncia() {
//    return this.http.get(this.urlBase + 'api/Denuncia/listarEstadosDenuncia').map(res => res.json());
//  }

//}
