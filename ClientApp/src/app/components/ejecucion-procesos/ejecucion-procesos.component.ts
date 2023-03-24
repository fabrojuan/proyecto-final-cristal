import { Component, OnInit, Inject, ViewChild, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ImpuestoService } from '../../services/impuesto.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';
//Validaciones de inputs:
import { ControlValueAccessor, Validator, AbstractControl, ValidationErrors, NG_VALUE_ACCESSOR, NG_VALIDATORS } from '@angular/forms';
import { CampoRequeridoComponent } from '../campo-requerido/campo-requerido.component';
import { __values } from 'tslib';
import { ResultadoEjecucionProceso } from 'src/app/modelos/ResultadoEjecucionProceso';
import { SolicitudGeneracionImpuestos } from 'src/app/modelos/SolicitudGeneracionImpuestos';

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
export class EjecucionProcesosComponent implements OnInit, ControlValueAccessor, Validator {
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
  public Valuacion: FormGroup;
  tituloModal: string = "Generación Impuesto Inmobiliario";
  //Validaciones
  numRegex = /^-?\d{3,9}[.,]?\d{0,2}$/;
  numRegexPorcentual = /^-?\d{1,2}[.,]?\d{0,2}$/;
  //a mostrar en modal
  onTouched: () => void = () => { };
  onChange: (value: any) => void = () => { };
  subscriptions: Subscription;
  InfoModalOkRedirect: number = 0;
  @ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any> | undefined;
  @ViewChild("myModalInfo2", { static: false }) myModalInfo2: TemplateRef<any> | undefined;

  //Esta linea anterior es para el modal.
  esquina: any = 0;
  asfaltado: any = 0;
  DatosRegistrados: any;

  constructor(private impuestoService: ImpuestoService, private router: Router, @Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private formBuilder: FormBuilder)
  {
    
    this.subscriptions = new Subscription();
    this.Valuacion = this.formBuilder.group(
      {
        "ValorSupTerreno": new FormControl("", [Validators.required, Validators.maxLength(10), Validators.pattern(this.numRegex)]),
        "ValorSupEdificada": new FormControl("", [Validators.required, Validators.maxLength(10), Validators.pattern(this.numRegex)]), //Antes entre 2 y 10 numeros ^[0-9]{2,10}
        "IncrementoEsquina": new FormControl("", [Validators.required, Validators.maxLength(10), Validators.pattern(this.numRegexPorcentual)]),
        "IncrementoAsfalto": new FormControl("", [Validators.required, Validators.maxLength(10), Validators.pattern(this.numRegexPorcentual)]),

        "Bhabilitado": new FormControl("1"),

      },
      { validators: this.todoslosCamposRequeridos }
    );
  }//fin constructor
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
  validate(control: AbstractControl): ValidationErrors | null {
    return this.todoslosCamposRequeridos(control);
  }

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

  registerOnChange(onChange: (value: any) => void): void {
    this.subscriptions.add(this.Valuacion.valueChanges.subscribe(onChange));
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  setDisabledState(disabled: boolean): void {
    disabled ? this.Valuacion.disable() : this.Valuacion.enable();
  }
  

  ngOnInit() {
  }

  //CONFIRMACION DE BOLETAS DIARIAS
  getConfirmacionBoletas() {
    this.impuestoService.getUltimaFechaBoleta().subscribe(param => this.limpiezaBoleta = param);
    this.fechaEjecucion = new Date(this.limpiezaBoleta.fechaEjecucion);
    console.log('dia de ultima ejecucion:' + this.fechaEjecucion.toLocaleString('es-AR', { day: 'numeric' }));
    this.aniofechaEjeTemp = this.fechaEjecucion.toLocaleString('es-AR', { day: '2-digit' });
    console.log('dia Actual' + this.fechaHoy.toLocaleString('es-AR', { day: '2-digit' }));
    this.anioActualTemp = this.fechaHoy.toLocaleString('es-AR', { day: '2-digit' });
    if (this.anioActualTemp == this.aniofechaEjeTemp) {
      alert("La confirmacion de boletas ya ha sido realizada previamente");
      this.router.navigate(["/bienvenida"]);
    }
    else {
      alert("La confirmacion de boletas se realizó exitosamente");
      this.impuestoService.SP_LimpiezaBoletas().subscribe(param => this.limpiezaBoleta = param);
    }
  }

  getGeneracionImpuestos() {

    const solicitud: SolicitudGeneracionImpuestos = {
      anio: new Date().getFullYear(),
      montoSuperficieTerreno: 1500,
      montoSuperficieEdificada: 1800,
      coeficienteInteresEsquina: 1,
      coeficienteInteresAsfalto: 2
    };

    this.impuestoService.SP_GeneracionImpuestos(solicitud)
      .subscribe(response => this.resultadoEjecucionProceso = response);

    if (this.resultadoEjecucionProceso?.resultado === "OK") {
      alert("La generacion de Impuestos se realizó exitosamente");
    } else {
      alert(this.resultadoEjecucionProceso?.mensaje);
      this.router.navigate(["/bienvenida"]);
    }
}

  //Generacion de Interes Mensual.
  getinteresMensual() {
    this.impuestoService.getUltimaFechaInteres().subscribe(param => this.interesMensual = param);
    this.fechaEjecucion = new Date(this.interesMensual.fechaEjecucion);
    console.log('Mes ultima ejecucion:' + this.fechaEjecucion.toLocaleString('en-us', { month: 'long' }));
    this.MesfechaEjeTemp = this.fechaEjecucion.toLocaleString('en-us', { month: 'long' });
    console.log('Mes Actual' + this.fechaHoy.toLocaleString('en-us', { month: 'long' }));
    this.MesActualTemp = this.fechaHoy.toLocaleString('en-us', { month: 'long' });
    if (this.MesActualTemp == this.MesfechaEjeTemp) {
      alert("El INTERES MENSUAL ya ha sido ejecutado anteriormente");
      this.router.navigate(["/bienvenida"]);
    }
    else {
      alert("El INTERES ha sido ejecutado exitosamente");
      this.impuestoService.SP_GeneracionInteresesMensuales().subscribe(param => this.generacionImpuestos = param);

    }
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

    if (this.Valuacion.valid == true) {
      this.InfoModalOkRedirect = 1;
      //A partir de aca llamar al guardar
       //console.log(this.Valuacion.value); para verificar si los datos llegan ok al modulo de angular
    
      //this.Valuacion.reset(); estaba reseteandolo aca tengo que resetear despues.......ver estt 13/03/23
      this.modalService.dismissAll(this.Valuacion.value);
      this.impuestoService.SP_Valuacion(this.Valuacion.value).subscribe(data => {
        if (data) {
          console.log(data);

          this.resultadoGuardadoModal = "El Impuesto Anual Se genero de Manera Correcta.";
   //       this.redirectPersona = 1;
          this.modalService.open(this.myModalInfo2);
        }
        else
          this.resultadoGuardadoModal = "El El proceso de Registro tuvo algun fallo.";
     //   this.redirectPersona = 1;
      });


     // this.modalService.open(this.myModalInfo);
     // this.router.navigate(["/ejecucion-procesos"]);
    }
  }

  cerrarmodal() {
    this.modalService.dismissAll(this.myModalInfo);
    if (this.InfoModalOkRedirect > 0)
      this.router.navigate(["/ejecucion-procesos"]);
  }

}
