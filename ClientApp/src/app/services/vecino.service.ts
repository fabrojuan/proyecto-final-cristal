import { HttpClientModule, HttpClient } from '@angular/common/http'
import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class VecinoService {
  urlBase: string = "";
  //,private router: Router
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.urlBase = baseUrl;

  }

  public getRol() {
    return this.http.get(this.urlBase + 'api/vecino/listarRol').pipe(map(res => res));
  }

  public getvecino() {
    return this.http.get(this.urlBase + 'api/Vecino/listarvecinos')
      .pipe(map(res => res));
  }
  public getFiltrarvecinoPorTipo(idTipo: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Vecino/filtrarvecinoPorTipo/' + idTipo);
  }

  public Recuperarvecino(idvecino: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/vecino/recuperarvecino/' + idvecino).pipe(map(res => res));//.catch(this.errorHandler);
  }

  public Guardarvecino(vecino: any): Observable<any> {
    var url = this.urlBase + 'api/Vecino/guardarvecino/';
    return this.http.post(url, vecino).pipe(map(res => res));
  }

  public GuardarVecino(vecino:any) :Observable<any> {
    var url = this.urlBase + 'api/Vecino/guardarVecino/';
    return this.http.post(url, vecino).pipe(map(res => res));
  }



  //SOlo utilizado en el login
  public ObtenerVariableSession() {
    return this.http.get(this.urlBase + 'api/vecino/obtenerVariableSession').pipe(map((res: any) => {
      var data = res;
      var inf = data.valor;
      if (inf == "") {
        this.router.navigate(["/error-pagina-login"]);
        return false;
      }
      else {
        return true;
      }

    }));
  }

  public ObtenerSession(): Observable<any> {
    return this.http.get(this.urlBase + 'api/vecino/obtenerVariableSession').pipe(map((res: any) => {
      var data = res;
      var inf = data.valor;
      if (inf == "") {
        return false;
      }
      else {
        return true;
      }

    }));
  }


  public obtenerSessionidVecino(): Observable<any> {
    return this.http.get(this.urlBase + 'api/vecino/obtenerVariableSession').pipe(map(res => res));
  }
  public obtenerSessionNombreVecino(): Observable<any> {
    return this.http.get(this.urlBase + 'api/vecino/obtenerSessionNombreVecino').pipe(map(res => res));

  }

  //   ************** LOGIN *****************
  public login(vecino:any): Observable<any> {
    return this.http.post(this.urlBase + "api/Vecino/login/", vecino).pipe(map(res => res));
  }


  public cerrarSessionVecino() {
    return this.http.get(this.urlBase + "api/Vecino/cerrarSessionVecino").pipe(map(res => res));
  }

//  //   *************** FIN LOGIN ***************
}
