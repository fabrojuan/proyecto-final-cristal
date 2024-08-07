import { Injectable } from '@angular/core';
import { TipoImagenEnum, Titulo } from '../modelos_Interfaces/Titulo';

@Injectable({
  providedIn: 'root'
})
export class TitulosServiceService {

  private configuration: Titulo[] = [];

  constructor() {
    this.inicializarTitulos();
  }

  public getConfiguracion(componente: string) {
    return this.configuration.find(item => item.nombreComponente == componente);
  }

  private inicializarTitulos() {

    // ChartsDenunciaComponent
    var tituloChartsDenunciaComponent: Titulo = {
      nombreComponente: "ChartsDenunciaComponent",
      titulo: "Charts Denuncias",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-shield-lock",
      link: "/bienvenida",
      tituloLink: "Listado de Denuncias Activas. Click para dashboard"
    };
    this.configuration.push(tituloChartsDenunciaComponent);

    // ChartsReclamosComponent
    var tituloChartsReclamosComponent: Titulo = {
      nombreComponente: "ChartsReclamosComponent",
      titulo: "Indicadores Requerimientos",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-file-bar-graph",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloChartsReclamosComponent);

    // EjecucionProcesosComponent
    var tituloEjecucionProcesosComponent: Titulo = {
      nombreComponente: "EjecucionProcesosComponent",
      titulo: "Gestión de Procesos Masivos",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-gear-fill",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloEjecucionProcesosComponent);

    // UsuarioTablaComponent
    var tituloUsuarioTablaComponent: Titulo = {
      nombreComponente: "UsuarioTablaComponent",
      titulo: "Tablero de Gestión de Empleados",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/trabajador.svg",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloUsuarioTablaComponent);

    /**
     * PaginaTablaComponent
     */
    var tituloPaginaTablaComponent: Titulo = {
      nombreComponente: "PaginaTablaComponent",
      titulo: "Tablero de Gestión de Páginas",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-book",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloPaginaTablaComponent);

    /**
     * ReclamoTablaComponent
     */
    var tituloReclamoTablaComponent: Titulo = {
      nombreComponente: "ReclamoTablaComponent",
      titulo: "Tablero de Gestión de Requerimientos",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-list-task",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloReclamoTablaComponent);

    /**
     * TipoRolTablaComponent
     */
    var tituloTipoRolTablaComponent: Titulo = {
      nombreComponente: "TipoRolTablaComponent",
      titulo: "Tablero de Gestión de Roles",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-list-task",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloTipoRolTablaComponent);

    /**
     * SugerenciaTablaComponent
     */
    var tituloSugerenciaTablaComponent: Titulo = {
      nombreComponente: "SugerenciaTablaComponent",
      titulo: "Tablero de Gestión de Sugerencias",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/patch-plus-fll.svg",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloSugerenciaTablaComponent);

    /**
     * ImpuestosVecinoIdentificadorComponent
     */
    var tituloImpuestosVecinoIdentificadorComponent: Titulo = {
      nombreComponente: "ImpuestosVecinoIdentificadorComponent",
      titulo: "Ver y Pagar impuestos de Lote",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/money.svg",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloImpuestosVecinoIdentificadorComponent);

    /**
     * TablaDenunciaComponent
     */
    var tituloTablaDenunciaComponent: Titulo = {
      nombreComponente: "TablaDenunciaComponent",
      titulo: "Tablero de Gestión de Denuncias",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/shield-exclamation.svg",
      link: "/bienvenida",
      tituloLink: "Denuncia generada por el vecino. Click para regresar al tablero"
    };
    this.configuration.push(tituloTablaDenunciaComponent  );

    /**
     * LoteTablaComponent
     */
    var tituloLoteTablaComponent: Titulo = {
      nombreComponente: "LoteTablaComponent",
      titulo: "Tablero de Gestión de Lotes",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/Imagenes/construccion_lote.png",
      link: "/lotesypersonas",
      tituloLink: "Click para ir a home"
    };
    this.configuration.push(tituloLoteTablaComponent);


    /**
     * LoteFormGenerarComponent
     */
    var tituloLoteFormGenerarComponent: Titulo = {
      nombreComponente: "LoteFormGenerarComponent",
      titulo: "Registración de Lote",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/lote_px.png",
      link: "/lote-tabla",
      tituloLink: "Click para volver al escritorio"
    };
    this.configuration.push(tituloLoteFormGenerarComponent);

    /**
     * DatosFinanzasEconomicosComponent
     */
    var tituloDatosFinanzasEconomicosComponent: Titulo = {
      nombreComponente: "DatosFinanzasEconomicosComponent",
      titulo: "Descarga de Datos Abiertos Financieros",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/Imagenes/open-data.png",
      link: "",
      tituloLink: ""
    };
    this.configuration.push(tituloDatosFinanzasEconomicosComponent);

    /**
     * DatosFinanEconomgenerarComponent          
     */
    var tituloDatosFinanEconomGenerarComponent: Titulo = {
      nombreComponente: "DatosFinanEconomGenerarComponent",
      titulo: "Gestión de Datos Abiertos Financieros",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/Finanzas.png",
      link: "/bienvenida",
      tituloLink: "Volver al escritorio"
    };
    this.configuration.push(tituloDatosFinanEconomGenerarComponent);

    
    /**
    * DatosFinanzasEconomicosBorradoComponent          
    */
    var tituloDatosFinanzasEconomicosBorradoComponent: Titulo = {
      nombreComponente: "DatosFinanzasEconomicosBorradoComponent",
      titulo: " Borrado de Datasets Impositivos",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/trash.svg",
      link: "/datos-finan-econom-generar",
      tituloLink: "Volver a la gestión de datos financieros"
    };
    this.configuration.push(tituloDatosFinanzasEconomicosBorradoComponent);

    /**
    * DenunciaDetalleComponent          
    */
    var tituloDenunciaDetalleComponent: Titulo = {
      nombreComponente: "DenunciaDetalleComponent",
      titulo: "Información provista inicialmente",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-shield-fill-exclamation",
      link: "/tabla-denuncia",
      tituloLink: "Volver a la gestión de denuncias"
    };
    this.configuration.push(tituloDenunciaDetalleComponent);

    /**
   * DenunciaDetalleComponent          
   */
    var tituloTrabajoFormGenerarComponent: Titulo = {
      nombreComponente: "TrabajoFormGenerarComponent",
      titulo: "Registrar trabajo en la Denuncia",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-shield-fill-exclamation",
      link: "/tabla-denuncia",
      tituloLink: "Volver a la gestión de denuncias"
    };
    this.configuration.push(tituloTrabajoFormGenerarComponent);

    /**
  * DenunciaDetalleComponent          
  */
    var tituloTrabajoTablaComponent: Titulo = {
      nombreComponente: "TrabajoTablaComponent",
      titulo: "Trabajos realizados en la Denuncia",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/Imagenes/trabajando.svg",
      link: "/tabla-denuncia",
      tituloLink: "Volver a la gestión de denuncias"
    };
    this.configuration.push(tituloTrabajoTablaComponent);

    /**
 * HistoricoDenunciaTablaComponent          
 */
    var tituloHistoricoDenunciaTablaComponent: Titulo = {
      nombreComponente: "HistoricoDenunciaTablaComponent",
      titulo: "Historial de Denuncias Cerradas",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/calendar3.svg",
      link: "/bienvenida",
      tituloLink: "Volver al escritorio"
    };
    this.configuration.push(tituloHistoricoDenunciaTablaComponent);

    /**
* HistoricoDenunciaTablaComponent          
*/
    var tituloHistoricoDenunciaTrabajosComponent: Titulo = {
      nombreComponente: "HistoricoDenunciaTrabajosComponent",
      titulo: "Historial de trabajos realizados en la Denuncia",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/calendar3.svg",
      link: "/historico-denuncia-tabla",
      tituloLink: "Volver al tablero de historicos de denuncia"
    };
    this.configuration.push(tituloHistoricoDenunciaTrabajosComponent);

    /**
     * ReclamoFormConsultarComponent
     */
    var tituloReclamoFormConsultarComponent: Titulo = {
      nombreComponente: "ReclamoFormConsultarComponent",
      titulo: "Consultar Requerimiento",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-list-task",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloReclamoFormConsultarComponent);


    
    // FormUsuarioGenerarComponent
    var tituloFormUsuarioGenerarComponent: Titulo = {
      nombreComponente: "FormUsuarioGenerarComponent",
      titulo: "Alta/Edición de Empleados",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/trabajador.svg",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloFormUsuarioGenerarComponent);


    /**
     * PaginaFormGenerarComponent
     */
    var tituloPaginaFormGenerarComponent: Titulo = {
      nombreComponente: "PaginaFormGenerarComponent",
      titulo: "Alta/Edición de Páginas",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-book",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloPaginaFormGenerarComponent);


    /**
     * TipoRolFormGenerarComponent
     */
    var tituloTTipoRolFormGenerarComponent: Titulo = {
      nombreComponente: "TipoRolFormGenerarComponent",
      titulo: "Alta/Edición de Roles",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-list-task",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloTTipoRolFormGenerarComponent);

        /**
    * DenunciaFormGenerarComponent    
    */
    var tituloDenunciaFormGenerarComponent: Titulo = {
      nombreComponente: "DenunciaFormGenerarComponent",
      titulo: "Formulario de Generación de Denuncia",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-shield-fill-exclamation",
      link: "/",
      tituloLink: "Volver al home"
    };
    this.configuration.push(tituloDenunciaFormGenerarComponent);

           /**
    * TrabajoDetalleDenuncia          
    */
    var tituloTrabajoDetalleDenunciaComponent: Titulo = {
      nombreComponente: "TrabajoDetalleDenunciaComponent",
      titulo: "Trabajos realizados en la Denuncia",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-shield-fill-exclamation",
      link: "/trabajo-tabla",
      tituloLink: "Volver a la gestión de denuncias"
    };
    this.configuration.push(tituloTrabajoDetalleDenunciaComponent);

    
    // IndicadoresGestionComponent
    var tituloIndicadoresGestionComponent: Titulo = {
      nombreComponente: "IndicadoresGestionComponent",
      titulo: "Indicadores de Gestión",
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bi bi-file-bar-graph",
      link: "/bienvenida",
      tituloLink: ""
    };
    this.configuration.push(tituloIndicadoresGestionComponent);

  }
}
