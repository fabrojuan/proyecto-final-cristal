import { HttpClientModule, HttpClient } from '@angular/common/http'
import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { SolicitudGeneracionImpuestos } from '../modelos/SolicitudGeneracionImpuestos';
import { ResultadoEjecucionProceso } from '../modelos/ResultadoEjecucionProceso';


@Injectable({
  providedIn: 'root'
})

export class ImpuestoService {
  urlBase: string = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.urlBase = baseUrl;
  }
  public getUltimaFechaInteres(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Impuestos/getUltimaFechaInteres').pipe(map(res => res));
  }

  getUltimaFechaBoleta() {
    return this.http.get(this.urlBase + 'api/Impuestos/getUltimaFechaBoleta').pipe(map(res => res));
  }

  public ListarImpuestosAdeudados(idLote:any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Impuestos/ListarImpuestosAdeudados/' + idLote).pipe(map(res => res));
  }

  public generarImpuestos(solicitud: SolicitudGeneracionImpuestos): Observable<ResultadoEjecucionProceso> {
    return this.http.post<ResultadoEjecucionProceso>(this.urlBase + 'api/Impuestos/SP_GeneracionImpuestos',
                          solicitud)
  }

  public generarInteresesMensuales(): Observable<ResultadoEjecucionProceso> {
    return this.http.get<ResultadoEjecucionProceso>(this.urlBase + 'api/Impuestos/SP_GeneracionInteresesMensuales')

  }

  public confirmarBoletas(): Observable<ResultadoEjecucionProceso> {
    return this.http.get<ResultadoEjecucionProceso>(this.urlBase + 'api/Impuestos/SP_LimpiezaBoletas')
  } 

  public guardarBoleta(FGimpuestos :any): Observable<any> {
    var url = this.urlBase + 'api/Impuestos/guardarBoleta/';
    return this.http.post(url, FGimpuestos).pipe(map(res => res));

  }

  public obtenerUrlMobbexx(): Observable<any> {
    //return this.http.get(this.urlBase + 'api/Impuesto/obtenerUrlMobbexx').map(res => res.json());
    return this.http.get(this.urlBase + 'api/Impuestos/obtenerUrlMobbexx').pipe(map((res: any) => {
      var myJSON = decodeURIComponent(res.json());
      var myObject = JSON.parse(myJSON);
      console.log("Se hace +" + myObject);
      this.router.navigate(myObject);
    }));


  }
  public obtenerUrlMobbexx2(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Impuestos/obtenerUrlMobbexx2').pipe(map(res => res));

  }


}



