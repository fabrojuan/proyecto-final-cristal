import { HttpClientModule, HttpClient } from '@angular/common/http'
import { Injectable, Inject } from '@angular/core';

//import { Http } from '@angular/http'; Deprecado 1
//import 'rxjs/add/operator/map';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { Observable, of, throwError } from 'rxjs';
import * as moment from 'moment';


@Injectable({
  providedIn: 'root'
})

export class UsuarioService {
  urlBase: string = "";
  //,private router: Router
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.urlBase = baseUrl;

  }

  //Modificacion de Roles
  public listarRoles(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Rol/listarRoles').pipe(map(res => res));
  }

  //Listado de paginasTipo Rol
  public listarPaginasTipoRol(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Rol/listarPaginasTipoRol').pipe(map(res => res));
  }
  public listarPaginasRecuperar(idRol: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Rol/listarPaginasRecuperar/' + idRol).pipe(map(res => res));
  }
  public listarTodasPaginas(): Observable<any> {
    return this.http.get(this.urlBase + 'api/Pagina/listarTodasPaginas').pipe(map(res => res));
  }

  public guardarPagina(oPaginaCLS: any) :Observable<any> {

    var url = this.urlBase + 'api/Pagina/guardarPagina';
    return this.http.post(url, oPaginaCLS).pipe(map(res => res));

  }
  public recuperarPagina(idPagina:any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Pagina/recuperarPagina/ ' + idPagina).pipe(map(res => res));
  }
  public eliminarPagina(idPagina:any): Observable<any> {
    return this.http.get(this.urlBase + 'api/Pagina/eliminarPagina/ ' + idPagina).pipe(map(res => res));
  }



  public guardarROL(oRolCLS: any) {

    var url = this.urlBase + 'api/Rol/guardarROL/';
    return this.http.post(url, oRolCLS).pipe(map(res => res));

  }

  public eliminarRol(idRol:any ): Observable<any> {
    return this.http.get(this.urlBase + 'api/Rol/eliminarRol/' + idRol).pipe(map(res => res));
  }
  public getUsuarios(): Observable<any> {
    return this.http.get(this.urlBase + 'api/usuarios')
      .pipe(map(res => res));
  }
  public getFiltrarUsuarioPorTipo(idTipo:any): Observable<any> {
    return this.http.get(this.urlBase + 'api/usuarios/filtrarUsuarioPorTipo/' + idTipo);
  }

  public RecuperarUsuario(idUsuario:any): Observable<any> {
    return this.http.get(this.urlBase + 'api/usuarios/' + idUsuario).pipe(map(res => res));//.catch(this.errorHandler);
  }

  public GuardarUsuario(Usuario:any) {
    var url = this.urlBase + 'api/usuarios';
    return this.http.post(url, Usuario).pipe(map(res => res));
  }

  public borrarUsuario(idUsuario: number): Observable<any> {
    return this.http.delete(this.urlBase + 'api/usuarios/' + idUsuario).pipe(map(res => res));
  }

  public validarCorreo(id:any, correo:any): Observable<any> {
    return this.http.get(this.urlBase + "api/vecinos/validarCorreo/" + id + "/" + correo).pipe(map(res => res));
  }

  public GuardarVecino(Usuario:any) {
    var url = this.urlBase + 'api/vecinos/';
    return this.http.post(url, Usuario).pipe(map(res => res));
  }

  public listarPaginas(): Observable<any> {
    return this.http.get(this.urlBase + 'api/usuarios/paginas')
      .pipe(map(res => res));
  }


  public ObtenerVariableSession(next: any): Observable<any> {

    return this.http.get(this.urlBase + 'api/usuarios/paginas').pipe(map((res: any) => {

      var data = res;

      //Aca trajimos el parametro next para que tomemos la ruta de la base continuar mañana.
      var pagina = next["url"][0].path;
      if (data != null && data != "") {
        var paginas = data.map((pagina: { accion: any; }) => pagina.accion); //Estaba llamando mal la accion de esta forma funca
        //var paginas = data.lista.pipe(map((pagina: any) => pagina.accion));
        if (paginas.indexOf(pagina) > -1 && pagina != "Login") {
          return true;
        }
        else {
          this.router.navigate(["/error-pagina-login"]);
          return false;
        }
      }
      return false;        // si algo falla aca falta un return true

    }));
  }

  public ObtenerSession() {
    return this.http.get(this.urlBase + 'api/usuarios/paginas').pipe(map((res: any) => {
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


  ////   ************** LOGIN *****************
  public login(usuario: any): Observable<any> {
    return this.http.post(this.urlBase + "api/usuarios/login/", usuario).pipe(
      map(res => {
        this.guardarToken(res);
        return res;
      })
    );
  }

  private guardarToken(authResult: any) {
    const expiresAt = moment().add(authResult.expiresAt, 'seconds');
    localStorage.setItem('tokenId', authResult.tokenId);
    localStorage.setItem("expiresAt", JSON.stringify(expiresAt.valueOf()));
  }

  private borrarToken() {
    localStorage.removeItem("tokenId");
    localStorage.removeItem("expiresAt");
  }

  public isLoggedIn(): boolean {
    try {
      return moment().isBefore(this.getExpiration());
    } catch (e) {
      return false;
    }    
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem("expiresAt") || "";
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }


  public cerrarSession() {
    this.borrarToken();
    return true;
  }

  //   *************** FIN LOGIN ***************

}
