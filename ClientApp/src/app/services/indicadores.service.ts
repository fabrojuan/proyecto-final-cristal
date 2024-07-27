import { HttpClientModule, HttpClient } from '@angular/common/http'
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class IndicadoresService {
  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;

  }

  public getDenunciasxEmpleado(): Observable<any>  {
    return this.http.get(this.urlBase + 'api/Denuncia/DenunciasxEmpleado').pipe(map(res => res));
  }

  public getDenunciasCerradas(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Denuncia/DenunciasCerradas').pipe(map(res => res));
  }

  public getLotesCargados(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Lote/LotesCargados').pipe(map(res => res));
  }

  public cantidadDatosAbiertosGenerados(): Observable<any> {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/cantidadDatosAbiertosGenerados').pipe(map(res => res));
  }

  public CantidadDenunciasAbiertas(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Indicadores/CantidadDenunciasAbiertas').pipe(map(res => res));
  }
  public CantidadDenunciasCerradas(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Indicadores/CantidadDenunciasCerradas').pipe(map(res => res));
  }
  public FechaTrabajosEnDenuncias(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Indicadores/FechaTrabajosEnDenuncias').pipe(map(res => res));
  }

 public Denunciasportipo(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Indicadores/DenunciasporTipo').pipe(map(res => res));
  }


 
  /**
   * Reclamos
   */
  public getReclamosCerradosPorMesYTipoCierre(): Observable<any> {
    return this.http.get(this.urlBase + "api/Indicadores/reclamos-cerrados-por-mes-y-tipo-cierre").pipe(map(res => res));
  }

  public getReclamosNuevosPorMes(): Observable<any> {
    return this.http.get(this.urlBase + "api/Indicadores/reclamos-nuevos-por-mes").pipe(map(res => res));
  }

  public getReclamosAbiertosPorEstado(): Observable<any> {
    return this.http.get(this.urlBase + "api/Indicadores/reclamos-abiertos-por-estado").pipe(map(res => res));
  }

  public getTrabajosReclamosPorAreaYMes(): Observable<any> {
    return this.http.get(this.urlBase + "api/Indicadores/trabajos-reclamos-por-area-y-mes").pipe(map(res => res));
  }

}
