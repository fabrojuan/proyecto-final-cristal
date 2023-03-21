import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthInterceptor } from './services/AuthInterceptor';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
//*****Componentes**********
import { BienvenidaComponent } from './components/bienvenida/bienvenida.component';
import { LoginComponent } from './components/login/login.component';
import { DenunciaFormGenerarComponent } from './components/denuncia-form-generar/denuncia-form-generar.component';
import { BienvenidaVecinoComponent } from './components/bienvenida-vecino/bienvenida-vecino.component';
import { DashDatosAbiertosComponent } from './components/dash-datos-abiertos/dash-datos-abiertos.component';
import { DenunciaDetalleComponent } from './components/denuncia-detalle/denuncia-detalle.component';
import { DatosFinanEconomGenerarComponent } from './components/datos-finan-econom-generar/datos-finan-econom-generar.component';
import { DatosMedioambienteComponent } from './components/datos-medioambiente/datos-medioambiente.component';
import { DatosSolicitudesComponent } from './components/datos-solicitudes/datos-solicitudes.component';
import { DatosSugerenciasComponent } from './components/datos-sugerencias/datos-sugerencias.component';
import { DatosTerritorioUrbanismoComponent } from './components/datos-territorio-urbanismo/datos-territorio-urbanismo.component';
import { EjecucionProcesosComponent } from './components/ejecucion-procesos/ejecucion-procesos.component';
import { FiltroDenunciaComponent } from './components/filtro-denuncia/filtro-denuncia.component';
import { FormUsuarioGenerarComponent } from './components/form-usuario-generar/form-usuario-generar.component';
import { ImpuestoPagoSendComponent } from './components/impuesto-pago-send/impuesto-pago-send.component';
import { ImpuestosVecinoAdeudaTablaComponent } from './components/impuestos-vecino-adeuda-tabla/impuestos-vecino-adeuda-tabla.component';
import { DatosFinanzasEconomicosComponent } from './components/datos-finanzas-economicos/datos-finanzas-economicos.component';
import { ImpuestosVecinoIdentificadorComponent } from './components/impuestos-vecino-identificador/impuestos-vecino-identificador.component';
import { LoginVecinoComponent } from './components/login-vecino/login-vecino.component';
import { MapasCordobaComponent } from './components/mapas-cordoba/mapas-cordoba.component';
import { PaginaFormGenerarComponent } from './components/pagina-form-generar/pagina-form-generar.component';
import { PaginaTablaComponent } from './components/pagina-tabla/pagina-tabla.component';
import { ReclamoFormGenerarComponent } from './components/reclamo-form-generar/reclamo-form-generar.component';
import { ReclamoTablaComponent } from './components/reclamo-tabla/reclamo-tabla.component';
import { ReclamoTrabajoFormComponent } from './components/reclamo-trabajo-form/reclamo-trabajo-form.component';
import { ReclamoTrabajoTablaComponent } from './components/reclamo-trabajo-tabla/reclamo-trabajo-tabla.component';
import { SugerenciaFormGenerarComponent } from './components/sugerencia-form-generar/sugerencia-form-generar.component';
import { SugerenciaTablaComponent } from './components/sugerencia-tabla/sugerencia-tabla.component';
import { TablaDenunciaComponent } from './components/tabla-denuncia/tabla-denuncia.component';
import { TablaEstadoDenunciaComponent } from './components/denuncia-estado-tabla/denuncia-estado-tabla.component';
import { DenunciaTipoTablaComponent } from './components/denuncia-tipo-tabla/denuncia-tipo-tabla.component';
import { TrabajoTablaComponent } from './components/trabajo-tabla/trabajo-tabla.component';
import { UsuarioTablaComponent } from './components/usuario-tabla/usuario-tabla.component';
import { TipoRolFormGenerarComponent } from './components/tipo-rol-form-generar/tipo-rol-form-generar.component';
import { TipoRolTablaComponent } from './components/tipo-rol-tabla/tipo-rol-tabla.component';
import { TrabajoDetalleDenunciaComponent } from './components/trabajo-detalle-denuncia/trabajo-detalle-denuncia.component';
import { TrabajoFormGenerarComponent } from './components/trabajo-form-generar/trabajo-form-generar.component';
import { UsuarioVecinoFormGenerarComponent } from './components/usuario-vecino-form-generar/usuario-vecino-form-generar.component';
import { DenunciaGenerarComponent } from './components/denuncia-generar/denuncia-generar.component';
import { LoteTablaComponent } from './components/lote-tabla/lote-tabla.component';
import { LoteFormGenerarComponent } from './components/lote-form-generar/lote-form-generar.component';
import { LoteService } from './services/lote.service';
import { LoteDetalleComponent } from './components/lote-detalle/lote-detalle.component';

//*****Errores y librerias Auxiliares**********
import { ErrorPaginLoginComponent } from './components/error-pagin-login/error-pagin-login.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


//*****Guards**********
import { SeguridadGuard } from './components/guards/seguridad.guard';
import { SeguridadVecinoGuard } from './components/guards/seguridad-vecino.guard';

//*****Servicios**********

import { UsuarioService } from './services/usuario.service';
import { DenunciaService } from './services/denuncia.service';
import { VecinoService } from './services/vecino.service';
import { TrabajoService } from './services/trabajo.service';
import { PruebaGraficaService } from './services/prueba-grafica.service';
import { IndicadoresService } from './services/indicadores.service';
import { ImpuestoService } from './services/impuesto.service';
import { TipoReclamoTablaComponent } from './components/tipo-reclamo-tabla/tipo-reclamo-tabla.component';
import { TipoReclamoFormComponent } from './components/tipo-reclamo-form/tipo-reclamo-form.component';

import { ToastService } from './services/toast.service';
import { ToastsContainer } from './components/toasts-container/toasts-container.component';
import { TipoDenunciaFormComponent } from './components/tipo-denuncia-form/tipo-denuncia-form.component';
import { TipoDenunciaTablaComponent } from './components/tipo-denuncia-tabla/tipo-denuncia-tabla.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BienvenidaComponent,
    LoginComponent,
    DenunciaFormGenerarComponent,
    BienvenidaVecinoComponent,
    DashDatosAbiertosComponent,
    DenunciaDetalleComponent,
    DatosFinanEconomGenerarComponent,
    ErrorPaginLoginComponent,
    DatosMedioambienteComponent,
    DatosSolicitudesComponent,
    DatosSugerenciasComponent,
    DatosTerritorioUrbanismoComponent,
    EjecucionProcesosComponent,
    FiltroDenunciaComponent,
    FormUsuarioGenerarComponent,
    ImpuestoPagoSendComponent,
    ImpuestosVecinoAdeudaTablaComponent,
    DatosFinanzasEconomicosComponent,
    ImpuestosVecinoIdentificadorComponent,
    LoginVecinoComponent,
    MapasCordobaComponent,
    PaginaFormGenerarComponent,
    PaginaTablaComponent,
    ReclamoFormGenerarComponent,
    ReclamoTablaComponent,
    ReclamoTrabajoFormComponent,
    ReclamoTrabajoTablaComponent,
    SugerenciaFormGenerarComponent,
    SugerenciaTablaComponent,
    TablaDenunciaComponent,
    TablaEstadoDenunciaComponent,
    DenunciaTipoTablaComponent,
    TrabajoTablaComponent,
    UsuarioTablaComponent,
    TipoRolFormGenerarComponent,
    TipoRolTablaComponent,
    TrabajoDetalleDenunciaComponent,
    TrabajoFormGenerarComponent,
    UsuarioVecinoFormGenerarComponent,
    DenunciaGenerarComponent,
    LoteTablaComponent,
    LoteFormGenerarComponent,
    LoteDetalleComponent,
    TipoReclamoTablaComponent,
    TipoReclamoFormComponent,
    ToastsContainer,
    TipoDenunciaFormComponent,
    TipoDenunciaTablaComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule,
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'bienvenida', component: BienvenidaComponent, canActivate: [SeguridadGuard] },  //, canActivate: [SeguridadGuard]
      { path: 'datos-finanzas-economicos', component: DatosFinanzasEconomicosComponent },
      { path: 'denuncia-form-generar', component: DenunciaFormGenerarComponent },
      { path: 'denuncia-detalle', component: DenunciaDetalleComponent }, //, canActivate: [SeguridadGuard] //Veo que hacen falta los dos rutas sino el guard no anda ok
      { path: 'denuncia-detalle/:id', component: DenunciaDetalleComponent ,canActivate: [SeguridadGuard]}, //, canActivate: [SeguridadGuard]  le dio por no funcionar verlo!
      { path: 'denuncia-tipo-tabla', component: DenunciaTipoTablaComponent },
      { path: 'ejecucion-procesos', component: EjecucionProcesosComponent, canActivate: [SeguridadGuard] },
      { path: 'error-pagina-login', component: ErrorPaginLoginComponent },
      { path: 'form-usuario-generar', component: FormUsuarioGenerarComponent, canActivate: [SeguridadGuard] },
      { path: 'impuesto-pago-send', component: ImpuestoPagoSendComponent },
      { path: 'impuestos-vecino-adeuda-tabla', component: ImpuestosVecinoAdeudaTablaComponent },
      { path: 'impuestos-vecino-adeuda-tabla/:id', component: ImpuestosVecinoAdeudaTablaComponent },
      { path: 'impuestos-vecino-identificador', component: ImpuestosVecinoIdentificadorComponent },
      { path: 'impuestos-vecino-identificador/:id', component: ImpuestosVecinoIdentificadorComponent },
      { path: 'login', component: LoginComponent },
      { path: 'login-vecino', component: LoginVecinoComponent },
      { path: 'mapas-cordoba', component: MapasCordobaComponent },
      { path: 'pagina-form-generar', component: PaginaFormGenerarComponent, canActivate: [SeguridadGuard] },
      { path: 'pagina-form-generar/:id', component: PaginaFormGenerarComponent, canActivate: [SeguridadGuard] },

      { path: 'pagina-tabla', component: PaginaTablaComponent, canActivate: [SeguridadGuard] },
      { path: 'reclamo-form-generar', component: ReclamoFormGenerarComponent, canActivate: [SeguridadVecinoGuard] },
      { path: 'reclamo-tabla', component: ReclamoTablaComponent, canActivate: [SeguridadGuard] },
      { path: 'reclamo-trabajo-form/:id', component: ReclamoTrabajoFormComponent, canActivate: [SeguridadGuard] },
      // Revisar si reclamo-trabajo-form/:id funciona sin id..
      { path: 'sugerencia-form-generar', component: SugerenciaFormGenerarComponent },
      { path: 'sugerencia-tabla', component: SugerenciaTablaComponent, canActivate: [SeguridadGuard] },
      { path: 'tabla-denuncia', component: TablaDenunciaComponent, canActivate: [SeguridadGuard] },
      { path: 'tabla-estado-denuncia', component: TablaEstadoDenunciaComponent, canActivate: [SeguridadGuard] },
      { path: 'tipo-rol-form-generar', component: TipoRolFormGenerarComponent, canActivate: [SeguridadGuard] },  //Una vez que agregue las paginas a la base se asigna guard canActivate: [SeguridadVecinoGuard]
      { path: 'tipo-rol-form-generar/:id', component: TipoRolFormGenerarComponent, canActivate: [SeguridadGuard] },
      { path: 'tipo-rol-tabla', component: TipoRolTablaComponent, canActivate: [SeguridadGuard] },  //, canActivate: [SeguridadVecinoGuard]
      { path: 'trabajo-tabla', component: TrabajoTablaComponent, canActivate: [SeguridadGuard] },
      { path: 'trabajo-tabla/:id', component: TrabajoTablaComponent, canActivate: [SeguridadGuard] },
      { path: 'trabajo-form-generar', component: TrabajoFormGenerarComponent, canActivate: [SeguridadGuard] },
      { path: 'trabajo-form-generar/:id', component: TrabajoFormGenerarComponent, canActivate: [SeguridadGuard] },
      { path: 'trabajo-detalle-denuncia/:id', component: TrabajoDetalleDenunciaComponent, canActivate: [SeguridadGuard] },
      { path: 'usuario-tabla', component: UsuarioTablaComponent, canActivate: [SeguridadGuard] },
      { path: 'usuario-vecino-form-generar', component: UsuarioVecinoFormGenerarComponent },
      { path: 'denuncia-generar', component: DenunciaGenerarComponent },
      { path: 'lote-tabla', component: LoteTablaComponent, canActivate: [SeguridadGuard] },
      { path: 'lote-detalle', component: LoteDetalleComponent, canActivate: [SeguridadGuard]}, //, canActivate: [SeguridadGuard] //Veo que hacen falta los dos rutas sino el guard no anda ok
      { path: 'lote-detalle/:id', component: LoteDetalleComponent }, //, canActivate: [SeguridadGuard]  le dio por no funcionar verlo!
      { path: 'tipo-reclamo-tabla', component: TipoReclamoTablaComponent, canActivate: [SeguridadGuard] },
      { path: 'tipo-reclamo-form/:id', component: TipoReclamoFormComponent/*, canActivate: [SeguridadGuard]*/ },
      { path: 'tipo-reclamo-form', component: TipoReclamoFormComponent, canActivate: [SeguridadGuard] },
      { path: 'tipo-denuncia-tabla', component: TipoDenunciaTablaComponent, canActivate: [SeguridadGuard] },
      { path: 'tipo-denuncia-form/:id', component: TipoDenunciaFormComponent/*, canActivate: [SeguridadGuard] */},
      { path: 'tipo-denuncia-form', component: TipoDenunciaFormComponent/*, canActivate: [SeguridadGuard] */},

      { path: '*', redirectTo: '' } //a home


    ])
  ],
  providers: [UsuarioService, DenunciaService, TrabajoService, SeguridadGuard, VecinoService, SeguridadVecinoGuard, PruebaGraficaService, IndicadoresService, ImpuestoService, LoteService, ToastService,
            { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
