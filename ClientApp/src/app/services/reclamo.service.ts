import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

//import { r } from '@angular/core/src/render3';

@Injectable({
  providedIn: 'root'
})
export class ReclamoService {
  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;

  }

  /**
   *
   * Tipo de Reclamo
   *
   */
  public getTipoReclamo(): Observable<any> {
    return this.http.get(this.urlBase + 'api/reclamos/tipos-reclamo').pipe(map(res => res));    
  }

  public getTipoReclamoByCodigo(codTipoReclamo: number): Observable<any> {
    return this.http.get(this.urlBase + 'api/reclamos/tipos-reclamo/' + codTipoReclamo).pipe(map(res => res));
  }

  public eliminarTipoReclamo(codTipoReclamo: number): Observable<any> {
    return this.http.delete(this.urlBase + 'api/reclamos/tipos-reclamo/' + codTipoReclamo).pipe(map(res => res));
  }

  public agregarTipoReclamo(tipoReclamo: any): Observable<any> {
    return this.http.post(this.urlBase + 'api/reclamos/tipos-reclamo', tipoReclamo).pipe(map(res => res));
  }

  public modificarTipoReclamo(tipoReclamo: any): Observable<any> {
    return this.http.put(this.urlBase + 'api/reclamos/tipos-reclamo', tipoReclamo).pipe(map(res => res));
  }

  /**
   * 
   * Reclamo
   *
   */

  public agregarReclamo(Reclamo: any): Observable<any> {
    var url = this.urlBase + 'api/reclamos';
    return this.http.post(url, Reclamo).pipe(map(res => res));
  }
  public getReclamo(): Observable<any>{
    return this.http.get(this.urlBase + 'api/reclamos').pipe(map(res => res));
  }

  

}






//  public getEstadoDenuncia() {
//    return this.http.get(this.urlBase + 'api/Denuncia/listarEstadosDenuncia').map(res => res.json());
//  }




//}

