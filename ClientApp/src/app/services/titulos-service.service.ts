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
      pathImagen: "bi bi-file-earmark-fill",
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
      pathImagen: "bi bi-person-rolodex",
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
      tipoImagen: TipoImagenEnum.ICONO,
      pathImagen: "bu bi-envelope-paper-heart",
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
      pathImagen: "../../../assets/IconsGoogle/denuncia.png",
      link: "/tabla-denuncia",
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
     * HistoricoDenunciaTablaComponent
     */
    var tituloHistoricoDenunciaTablaComponent: Titulo = {
      nombreComponente: "HistoricoDenunciaTablaComponent",
      titulo: "Histórico de Denuncias",
      tipoImagen: TipoImagenEnum.FOTO,
      pathImagen: "../../../assets/IconsGoogle/denuncia.png",
      link: "/bienvenida",
      tituloLink: "Listado de Denuncias Solucionadas. Click para dashboard"
    };
    this.configuration.push(tituloHistoricoDenunciaTablaComponent);


  }
}
