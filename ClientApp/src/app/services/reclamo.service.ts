import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Prioridad } from '../modelos_Interfaces/Prioridad';
import { ModificarReclamoRequest } from '../modelos_Interfaces/ModificarReclamoRequest';
import { AplicarAccion } from '../modelos_Interfaces/AplicarAccion';
import { ObservacionReclamo } from '../modelos_Interfaces/ObservacionReclamo';

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

  public getReclamos(): Observable<any>{
    return this.http.get(this.urlBase + 'api/reclamos').pipe(map(res => res));
  }

  public getReclamo(nroReclamo: number): Observable<any> {
    return this.http.get(this.urlBase + 'api/reclamos/' + nroReclamo).pipe(map(res => res));
  }

  public modificarReclamo(nroReclamo: number, reclamo: ModificarReclamoRequest): Observable<any> {
    return this.http.put(this.urlBase + 'api/reclamos/' + nroReclamo, reclamo).pipe(map(res => res));
  }

  public getPrioridades(): Observable<Array<Prioridad>> {
    return this.http.get<Array<Prioridad>>(this.urlBase + 'api/reclamos/prioridades').pipe(map(res => res));
  }

  /**
  * Reclamo / Acciones
  */

  public aplicarAccion(nroReclamo: number, accion: AplicarAccion): Observable<any> {
    const url = this.urlBase + 'api/reclamos/' + nroReclamo + "/acciones";
    return this.http.post<any>(url, accion);
  }

  /**
   * Observaciones Reclamos
   */
  public getObservacionesReclamo(nroReclamo: number): Observable<Array<ObservacionReclamo>> {
    return this.http.get<Array<ObservacionReclamo>>(this.urlBase + 'api/reclamos/' + nroReclamo + '/observaciones').pipe(map(res => res));
  }

}