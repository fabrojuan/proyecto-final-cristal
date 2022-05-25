import { HttpClientModule, HttpClient } from '@angular/common/http'
import { Injectable, Inject } from '@angular/core';
//import 'rxjs/add/operator/map';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class TrabajoService {
  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;
  }
  public getUsuario(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Trabajo/listarUsuarios').pipe(map(res => res));
  }
  public getPrioridad(): Observable<any>{
    return this.http.get(this.urlBase + 'api/Trabajo/listarPrioridades').pipe(map(res => res));
  }

  public GuardarTrabajo(Trabajo:any) {
    var url = this.urlBase + 'api/Trabajo/guardarTrabajo/';
    return this.http.post(url, Trabajo).pipe(map(res => res));
  }

  public notificar(Trabajo: any): Observable<any> {
    var url = this.urlBase + 'api/Trabajo/notificar/';
    return this.http.post(url, Trabajo).pipe(map(res => res));
  }


  public GuardarTrabajoReclamo(Trabajo: any): Observable<any> {
    var url = this.urlBase + 'api/Trabajo/GuardarTrabajoReclamo/';
    return this.http.post(url, Trabajo).pipe(map(res => res));
  }

  public detalleDenuncia(idDenuncia: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Trabajo/detalleDenuncia/' + idDenuncia).pipe(map(res => res));
  }
  public detalleTrabajoDenuncia(nro_Trabajo: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Trabajo/detalleTrabajoDenuncia/' + nro_Trabajo).pipe(map(res => res));
  }
  public ImagenTrabajoDenuncia(nro_Trabajo: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Trabajo/ImagenTrabajoDenuncia/' + nro_Trabajo).pipe(map(res => res));
  }

  public RecuperarDenuncia(idDenuncia: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Trabajo/RecuperarDenuncia/' + idDenuncia).pipe(map(res => res));
  }
  public RecuperarReclamo(idReclamo: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Trabajo/RecuperarReclamo/' + idReclamo).pipe(map(res => res));
  }
  public ListarTrabajos(idDenuncia: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Trabajo/ListarTrabajos/' + idDenuncia).pipe(map(res => res));
  }
  public ListarTrabajosReclamo(idReclamo: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Trabajo/ListarTrabajosReclamo/' + idReclamo).pipe(map(res => res));
  }
}
