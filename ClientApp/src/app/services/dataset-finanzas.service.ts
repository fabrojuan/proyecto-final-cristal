import { HttpClientModule, HttpClient } from '@angular/common/http'
import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
//import 'rxjs/add/operator/map';
import { catchError, map } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class DatasetFinanzasService {
  urlBase: string = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.urlBase = baseUrl;
  }

  public generarDatasetImpuestos(): Observable<any>  {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/generaImpuestoInmobiliarioMensual').pipe(map(res => res));
   
  }

  public ListarFinancieros() {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/ListarFinancieros').pipe(map(res => res));
  }
  //public ListarFinancierosExcel() {
  //  return this.http.get(this.urlBase + 'api/DatosAbiertos/ListarFinancierosExcel').pipe(map(res => res));
  //}

  public SanitizarUrls() {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/SanitizarUrls').pipe(map(res => res));
  }
  public ListarArchivosDisco() {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/ListarArchivosDisco').pipe(map(res => res));
  }
  public DescargarArchivo(nombreArchivo: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/DescargarArchivo/' + nombreArchivo, { responseType: 'blob' });
  }
    
public DescargarArchivoExcel(nombreArchivo: any): Observable<any> {
  return this.http.get(this.urlBase + 'api/DatosAbiertos/DescargarArchivoExcel/' + nombreArchivo, { responseType: 'blob' });

  }
  public DescargarArchivoPDF(nombreArchivo: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/DescargarArchivoPDF/' + nombreArchivo, { responseType: 'blob' });

  }
  eliminarDataset(idArchivo: any): Observable<any> {
    return this.http.get(this.urlBase + 'api/DatosAbiertos/eliminarDataset/' + idArchivo).pipe(map(res => res));
  }
  //aca va el mime type del excel
  ExportarExcel() {
    this.http.get(this.urlBase + 'api/DatosAbiertos/generarregistrosImpuestosDeben', { responseType: 'text' }).subscribe(data => {
      var a = document.createElement("a");
      a.href = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + data;
      a.click();
    });
  }
  ExportarPDF() {
    this.http.get(this.urlBase + 'api/DatosAbiertos/generarregistrosImpuestosDebenPDF', { responseType: 'text' }).subscribe(data => {
      var a = document.createElement("a");
      a.href = "data:application / pdf; base64," + data;
      a.download = "ImpuestosAdeudados.pdf";
      a.click();
    });
  }
  

}




function handleErrorDownload(invalidResponse: any, any: any) {
    throw new Error('Function not implemented.');
}
