import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { DatasetFinanzasService } from '../../services/dataset-finanzas.service';
import { UsuarioService } from '../../services/usuario.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'datos-finanzas-economicos',
  templateUrl: './datos-finanzas-economicos.component.html',
  styleUrls: ['./datos-finanzas-economicos.component.css']
})
export class DatosFinanzasEconomicosComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  financieros: any;
  financierosExcel: any;
  p: number = 1;
  cabeceras: string[] = ["Nombre de Archivo", "Ubicación"];
  mostrarTablaCSV: boolean = false;
  mostrarTablaPDF: boolean = false;
  mostrarTablaXLS: boolean = false;
  bannerInicial: boolean = true;
  url: any;
  urlSafe: SafeResourceUrl | undefined;
  urlsSeguras: any;
  ubicacion :any="/ClientApp/src / assets / DatosAbiertos / ";
  constructor(private http: HttpClient,public sanitizer: DomSanitizer, private datasetfinanzasService: DatasetFinanzasService, private usuarioService: UsuarioService, private formBuilder: UntypedFormBuilder) {
  }
  ngOnInit() {

    this.datasetfinanzasService.ListarFinancieros().subscribe(data => this.financieros = data);
    //this.datasetfinanzasService.ListarFinancierosExcel().subscribe(dataXls => this.financierosExcel = dataXls);
    //El siguiente metodo funciona y en la vista de desarrollo no hay errores con las urls.
    this.datasetfinanzasService.SanitizarUrls().subscribe({
      next: (data: Object) => {
        this.urlsSeguras = (data as any[]).map(url => this.sanitizer.bypassSecurityTrustResourceUrl(url));
        //console.log(this.urlsSeguras);
      },
      error: (err) => {
        // Manejar el error
      }
    });

    // this.datasetfinanzasService.ListarArchivosDisco().subscribe((data: any[]) => { //en el servicio definir que solo devueva ubicacio+nombre de archivo
    //  // Iterar sobre los datos y marcar cada URL como segura
    //this.urlsSeguras = data.map(url => this.sanitizer.bypassSecurityTrustResourceUrl(url));
    //});
  }
  formatoCSV() {
    this.mostrarTablaPDF = false;
    this.mostrarTablaXLS = false;
    this.mostrarTablaCSV = true;
    this.bannerInicial = false;
}
  formatoPDF() {
    this.mostrarTablaPDF = true;
    this.mostrarTablaXLS = false;
    this.mostrarTablaCSV = false;
    this.bannerInicial = false;
  }
  formatoXLS() {
    this.mostrarTablaPDF = false;
    this.mostrarTablaXLS = true;
    this.mostrarTablaCSV = false;
    this.bannerInicial = false;
  }
  DescargarArchivo(nombreArchivo: string) {

    //this.datasetfinanzasService.DescargarArchivo(nombreArchivo).subscribe(data => {
    //  this.datasetfinanzasService.ListarFinancieros().subscribe(data => this.financieros = data);  //Refrescaremos la info del ngoninit
    //});
      this.datasetfinanzasService.DescargarArchivo(nombreArchivo).subscribe((response: Blob | ArrayBuffer) => {
        const url = window.URL.createObjectURL(new Blob([response], { type: 'application/octet-stream' }));
        const linkElement = document.createElement('a');
        linkElement.setAttribute('href', url);
        linkElement.setAttribute('download', nombreArchivo);
        document.body.appendChild(linkElement);
        linkElement.click();
        document.body.removeChild(linkElement);
        window.URL.revokeObjectURL(url);
      });
  }
  DescargarArchivoExcel(nombreArchivo: string) {

     this.datasetfinanzasService.DescargarArchivoExcel(nombreArchivo).subscribe((response: Blob | ArrayBuffer) => {
       const url = window.URL.createObjectURL(new Blob([response], { type: 'application/octet-stream' }));
       const linkElement = document.createElement('a');
       linkElement.setAttribute('href', url);
       linkElement.setAttribute('download', nombreArchivo);
       document.body.appendChild(linkElement);
       linkElement.click();
       document.body.removeChild(linkElement);
       window.URL.revokeObjectURL(url);
     });
  }
  DescargarArchivoPDF(nombreArchivo: string) {

    this.datasetfinanzasService.DescargarArchivoPDF(nombreArchivo).subscribe((response: Blob | ArrayBuffer) => {
      const url = window.URL.createObjectURL(new Blob([response], { type: 'application/octet-stream' }));
      const linkElement = document.createElement('a');
      linkElement.setAttribute('href', url);
      linkElement.setAttribute('download', nombreArchivo);
      document.body.appendChild(linkElement);
      linkElement.click();
      document.body.removeChild(linkElement);
      window.URL.revokeObjectURL(url);
    });
  }

  cambiarColor(event: any) {
    (event.target as HTMLElement).style.color = 'blue'; // Cambia el color a rojo cuando el mouse entra
  }

  volverColor(event: any) {
    (event.target as HTMLElement).style.color = ''; // Vuelve al color original cuando el mouse sale
  }

}


