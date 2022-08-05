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

  public getTipoReclamo(): Observable<any> {
    return this.http.get(this.urlBase + 'api/reclamos/tipos-reclamo').pipe(map(res => res));
  }
  public agregarReclamo(Reclamo: any) {
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

