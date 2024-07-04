import { Component, OnInit, Inject, ViewChild, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UntypedFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ImpuestoService } from '../../services/impuesto.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';

//Validaciones de inputs:
import { ControlValueAccessor, Validator, AbstractControl, ValidationErrors, NG_VALUE_ACCESSOR, NG_VALIDATORS } from '@angular/forms';
import { CampoRequeridoComponent } from '../campo-requerido/campo-requerido.component';
import { __values } from 'tslib';
import { ResultadoEjecucionProceso } from 'src/app/modelos/ResultadoEjecucionProceso';
import { SolicitudGeneracionImpuestos } from 'src/app/modelos/SolicitudGeneracionImpuestos';
import { ToastService } from 'src/app/services/toast.service';


@Component({
  selector: 'ejecucion-procesos',
  templateUrl: './ejecucion-procesos.component.html',
  styleUrls: ['./ejecucion-procesos.component.css'] ,
providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: CampoRequeridoComponent,
    },
    {
      provide: NG_VALIDATORS,
      multi: true,
      useExisting: CampoRequeridoComponent,
    },
  ]
})
export class EjecucionProcesosComponent implements OnInit /*, ControlValueAccessor, Validator*/ {
  generacionImpuestos: any;
  resultadoEjecucionProceso: ResultadoEjecucionProceso | undefined;
  interesMensual: any;
  fechaEjecucion: Date = new Date();
  limpiezaBoleta: any;
  fechaHoy: Date = new Date();
  // fechaEjecucion: Date ;
  MesfechaEjeTemp: any;
  MesActualTemp: any;
  aniofechaEjeTemp: any;
  anioActualTemp: any;
  resultadoGuardadoModal: any = "";
  public Valuacion: UntypedFormGroup;
  tituloModal: string = "Generación Impuesto Inmobiliario";
  //Validaciones
  numRegex = /^-?\d{3,9}[.,]?\d{0,2}$/;
  numRegexPorcentual = /^-?\d{1,2}[.,]?\d{0,2}$/;
  //a mostrar en modal
  //onTouched: () => void = () => { };
  //onChange: (value: any) => void = () => { };
  //subscriptions: Subscription;
  InfoModalOkRedirect: number = 0;
  isFormSubmitted: boolean=false;
  @ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any> | undefined;
  @ViewChild("myModalInfo2", { static: false }) myModalInfo2: TemplateRef<any> | undefined;

  //Esta linea anterior es para el modal.
  esquina: any = 0;
  asfaltado: any = 0;
  DatosRegistrados: any;
  constructor(private impuestoService: ImpuestoService, private router: Router, @Inject('BASE_URL') baseUrl: string, 
              private modalService: NgbModal, private formBuilder: UntypedFormBuilder,
              public _toastService: ToastService)
  {
    
    //this.subscriptions = new Subscription();
    this.Valuacion = this.formBuilder.group(
      {
        "ValorSupTerreno": new UntypedFormControl("", [Validators.required, Validators.maxLength(10), Validators.pattern(this.numRegex)]),
        "ValorSupEdificada": new UntypedFormControl("", [Validators.required, Validators.maxLength(10), Validators.pattern(this.numRegex)]), //Antes entre 2 y 10 numeros ^[0-9]{2,10}
        "IncrementoEsquina": new UntypedFormControl("", [Validators.required, Validators.maxLength(10), Validators.pattern(this.numRegexPorcentual)]),
        "IncrementoAsfalto": new UntypedFormControl("", [Validators.required, Validators.maxLength(10), Validators.pattern(this.numRegexPorcentual)]),

        "Bhabilitado": new UntypedFormControl("1"),

      },
      { validators: this.todoslosCamposRequeridos }
    );
  }//fin constructor
  ngOnDestroy(): void {
    //this.subscriptions.unsubscribe();
  }
  /*validate(control: AbstractControl): ValidationErrors | null {
    return this.todoslosCamposRequeridos(control);
  }*/

  todoslosCamposRequeridos(control: AbstractControl): ValidationErrors | null {
    const controlValue = control.value;
    
    const isValid = controlValue.ValorSupTerreno &&
      controlValue.ValorSupEdificada &&
      controlValue.IncrementoEsquina &&
      controlValue.IncrementoAsfalto;
    return isValid ? null : { required: true };
  }
  writeValue(value: any): void {
    value && this.Valuacion.setValue(value, { emitEvent: false });
  }

  setDisabledState(disabled: boolean): void {
    disabled ? this.Valuacion.disable() : this.Valuacion.enable();
  }
  

  ngOnInit() {
  
  }

  //CONFIRMACION DE BOLETAS DIARIAS
  getConfirmacionBoletas() {


    this.impuestoService.confirmarBoletas()
      .subscribe(response => {
        this.resultadoEjecucionProceso = response;

        if (this.resultadoEjecucionProceso?.resultado === "OK") {
          this._toastService.showOk("La confirmacion de boletas se realizó exitosamente");
        } else {
          this._toastService.showError(this.resultadoEjecucionProceso?.mensaje?.toString() || "");
        }
      });
  }

  //Generacion de Interes Mensual.
  getinteresMensual() {

    this.impuestoService.generarInteresesMensuales()
      .subscribe(response => {
        this.resultadoEjecucionProceso = response;

        if (this.resultadoEjecucionProceso?.resultado === "OK") {
          this._toastService.showOk("La generación de Intereses Mensuales se realizó exitosamente");
        } else {
          this._toastService.showError(this.resultadoEjecucionProceso?.mensaje?.toString() || "");
        }
      });
  }

  volverHome() {
    this.router.navigate(["/bienvenida"]);
  }
  LanzarModal() {
    
    this.modalService.open(this.myModalInfo);
  }

  //Este guardar datos pertenece al impuesto inmobiliario y hay que hace el procesamiento alli.
  //Valuacion Impuestos. y generacion impuesto Anual.
  guardarDatos() {

    this.DatosRegistrados = this.Valuacion.value;
    this.isFormSubmitted = true;

    if (this.Valuacion.valid == true) {
      this.InfoModalOkRedirect = 1;
    
      //this.Valuacion.reset(); estaba reseteandolo aca tengo que resetear despues.......ver estt 13/03/23
      this.modalService.dismissAll(this.Valuacion.value);

      const solicitud: SolicitudGeneracionImpuestos = {
        anio: new Date().getFullYear(),
        montoSuperficieTerreno: this.Valuacion.get("ValorSupTerreno")?.value,
        montoSuperficieEdificada: this.Valuacion.get("ValorSupEdificada")?.value,
        coeficienteInteresEsquina: this.Valuacion.get("IncrementoEsquina")?.value,
        coeficienteInteresAsfalto: this.Valuacion.get("IncrementoAsfalto")?.value
      };
  
      this.impuestoService.generarImpuestos(solicitud)
        .subscribe(response => {
          this.resultadoEjecucionProceso = response;
  
          if (this.resultadoEjecucionProceso?.resultado === "OK") {
            this._toastService.showOk("La generacion de Impuestos se realizó exitosamente")
          } else {
            this._toastService.showError(this.resultadoEjecucionProceso?.mensaje?.toString() || "");
          }
        });
    }
  }

  cerrarmodal() {
    this.modalService.dismissAll(this.myModalInfo);
    if (this.InfoModalOkRedirect > 0)
      this.router.navigate(["/ejecucion-procesos"]);
  }

  get valorSupTerrenoNoValido() {
    return this.isFormSubmitted && this.Valuacion.controls.ValorSupTerreno.errors;
  }

  get incrementoEsquinaNoValido() {
    return this.isFormSubmitted && this.Valuacion.controls.IncrementoEsquina.errors;
  }

  get valorSupEdificadaNoValido() {
    return this.isFormSubmitted && this.Valuacion.controls.ValorSupEdificada.errors;
  }

  get incrementoAsfaltoNoValido() {
    return this.isFormSubmitted && this.Valuacion.controls.IncrementoAsfalto.errors;
  }

}
