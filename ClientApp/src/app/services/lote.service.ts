import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})


export class LoteService {

  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;

  }

  public getTipoLote(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Lote/listarTiposLote').pipe(map(res => res));
  }

  public getLote() {
    return this.http.get(this.urlBase + 'api/Lote').pipe(map(res => res));
  }
  public agregarLote(Lote: any) {
    var url = this.urlBase + 'api/Lote/guardarLote/';
    return this.http.post(url, Lote).pipe(map(res => res));
  }



}
