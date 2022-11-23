import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import {lote} from '../modelos_Interfaces/lote';

@Injectable({
  providedIn: 'root'
})


export class LoteService {
  private sessionIdLoteDueño!: BehaviorSubject<number> //| undefined  //Creacion session local storage
 // idLoteDuenio: number = 0;
  rpta: any;
  urlBase: string = "";
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;
    //this.sessionIdLoteDueño = new BehaviorSubject(JSON.parse(localStorage.getItem('idLoteDuenio') || '{}'));
  }


  public getTipoLote(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Lote/listarTiposLote').pipe(map(res => res));
  }

  public getLote() {
    return this.http.get(this.urlBase + 'api/Lote').pipe(map(res => res));
  }
  
  public getLotePreExistente(idLote: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Lote/RecuperarLotePreExistente/' + idLote).pipe(map(res => res));//.catch(this.errorHandler);
  }

  public getPersonaPreExistente(dniTitular: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Persona/RecuperarPersonaPreExistente/' + dniTitular).pipe(map(res => res));//.catch(this.errorHandler);
  }



  //Haremos uso de este metodo cuando estemos seguros que ya hemos creado un lote y se usara solo en la llamada a añadir dueño.
  public get localStorageIdLote(): number {
    return this.sessionIdLoteDueño.value;
  }
  public agregarLote(Lote: any): Observable<any> {
    var url = this.urlBase + 'api/Lote/GuardarLote';
    return this.http.post<lote>(url, Lote).pipe(map(res => {

      // al lado del post iba.

      const idLoteDuenio: number = res.idLote;

      localStorage.setItem('idLoteDuenio', JSON.stringify(idLoteDuenio));   //Añado la session a local Storage no trabajo con cookie  s
      this.sessionIdLoteDueño.next(idLoteDuenio);  //esto no entiendo bien para que es aun.
      return res;
    }));
    };
  



}
