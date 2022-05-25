import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class DenunciaService {
  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;

  }
  
  public getTipoDenuncia(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Denuncia/listarTiposDenuncia').pipe(map(res => res));
  }

  public getEstadoDenuncia(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Denuncia/listarEstadosDenuncia').pipe(map(res => res));
  }

  public devolverAMesa(Trabajo: any){
    var url = this.urlBase + 'api/Denuncia/devolverAMEsa';
    return this.http.post(url, Trabajo).pipe(map(res => res));
  }

  public solucionarDenuncia(Trabajo:any) {
    var url = this.urlBase + 'api/Denuncia/solucionarDenuncia';
    return this.http.post(url, Trabajo).pipe(map(res => res));
  }



  public getDenuncia() {
    return this.http.get(this.urlBase + 'api/Denuncia/listarDenuncias').pipe(map(res => res));
  }
  public agregarDenuncia(Denuncia: any)
  {
    var url = this.urlBase + 'api/Denuncia/guardarDenuncia/';
    return this.http.post(url,Denuncia).pipe(map(res => res));
  }


  public DerivaPriorizaDenuncia(Denuncia :any) {
    var url = this.urlBase + 'api/Denuncia/DerivaPriorizaDenuncia';
    return this.http.post(url, Denuncia).pipe(map(res => res));

  }
  //funcion para filtrar denuncias en la tabla
  public filtrardenuncia(descripcion: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Denuncia/filtrarDenuncia/' + descripcion).pipe(map(res => res));
  }






}




