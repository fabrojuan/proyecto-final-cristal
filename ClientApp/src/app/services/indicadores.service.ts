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
}
