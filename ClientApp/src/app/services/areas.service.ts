import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Area } from '../modelos_Interfaces/Area';

@Injectable({
  providedIn: 'root'
})
export class AreasService {

  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;
  }

  public getAreas(): Observable<Array<Area>> {
    return this.http.get<Array<Area>>(this.urlBase + 'api/areas').pipe(map(res => res));
  }

}
