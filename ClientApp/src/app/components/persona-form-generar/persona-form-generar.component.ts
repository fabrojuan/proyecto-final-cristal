import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { LoteService } from '../../services/lote.service';
import { VecinoService } from '../../services/vecino.service';
import { UntypedFormGroup, UntypedFormControl, Validators, UntypedFormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';
//Validaciones de inputs:
import { ControlValueAccessor, Validator, AbstractControl, ValidationErrors, NG_VALUE_ACCESSOR, NG_VALIDATORS } from '@angular/forms';
import { CampoRequeridoComponent } from '../campo-requerido/campo-requerido.component';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'persona-form-generar',
  templateUrl: './persona-form-generar.component.html',
  styleUrls: ['./persona-form-generar.component.css'],
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

export class PersonaFormGenerarComponent implements OnInit, ControlValueAccessor, Validator {
  
  redirectPersona: number = 0;
  public Persona: UntypedFormGroup;
  resultadoGuardadoModal: any = "";
  //Validaciones
  onTouched: () => void = () => { };
  onChange: (value: any) => void = () => { };
  subscriptions: Subscription;
  isFormSubmitted: boolean=false;

  constructor(private loteservice: LoteService, private router: Router,
              private vecinoservice: VecinoService, private formBuilder: UntypedFormBuilder,
              public _toastService: ToastService) {

    this.subscriptions = new Subscription();
    this.Persona = this.formBuilder.group(
      {
        "Nombre": new UntypedFormControl("", [Validators.required, Validators.maxLength(65)]),
        "Apellido": new UntypedFormControl("", [Validators.required, Validators.maxLength(65)]),
        'Dni': new UntypedFormControl("", [Validators.required, Validators.maxLength(8), Validators.pattern("^[0-9]{8}")]),
        "FechaNac": new UntypedFormControl("", [Validators.required, Validators.maxLength(10), Validators.pattern("^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}")]),
        "Mail": new UntypedFormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),
        "Telefono": new UntypedFormControl("", [Validators.required, Validators.maxLength(20), Validators.pattern("^[0-9]{1,20}")]),
        "Domicilio": new UntypedFormControl("", [Validators.required, Validators.maxLength(100)]),
        "Altura": new UntypedFormControl("", [Validators.required, Validators.maxLength(4), Validators.pattern("^[0-9]{1,4}")]),
        "Bhabilitado": new UntypedFormControl("1"),
        "Iidpersona": new UntypedFormControl("0"),

      },
      { validators: this.todoslosCamposRequeridos }
    );
  }

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
  validate(control: AbstractControl): ValidationErrors | null {
    return this.todoslosCamposRequeridos(control);
  }

  todoslosCamposRequeridos(control: AbstractControl): ValidationErrors | null {
    const controlValue = control.value;
    const isValid =
      controlValue.Nombre &&
      controlValue.Apellido &&
      controlValue.Dni &&
      controlValue.FechaNac &&
      controlValue.Mail &&
      controlValue.Telefono &&
      controlValue.Domicilio &&
      controlValue.Altura &&
      controlValue.Bhabilitado;
    return isValid ? null : { required: true };
  }
  writeValue(value: any): void {
    value && this.Persona.setValue(value, { emitEvent: false });
  }

  registerOnChange(onChange: (value: any) => void): void {
    this.subscriptions.add(this.Persona.valueChanges.subscribe(onChange));
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  setDisabledState(disabled: boolean): void {
    disabled ? this.Persona.disable() : this.Persona.enable();
  }

  guardarDatos() {

    this.isFormSubmitted = true;

    if (this.Persona.invalid) {
      Object.values(this.Persona.controls).forEach(
        control => {
          control.markAsTouched();
        }
      );
      return;
    }

    this.vecinoservice.GuardarPersona(this.Persona.value).subscribe(data => {

      if (data) {
        this._toastService.showOk("Se ha registrado la persona correctamente");
        this.router.navigate(["/usuario-tabla"]);
      }
      else {
        this._toastService.showError("El mail de la persona ya ha sido registrado anteriormente, utilice otro correo electrónico por favor");
      }

    });

  }

  volverLoteTabla() {
    this.router.navigate(["/lote-tabla"]);
  }

  get nombreNoValido() {
    return this.isFormSubmitted && this.Persona.controls.Nombre.errors;
  }

  get apellidoNoValido() {
    return this.isFormSubmitted && this.Persona.controls.Apellido.errors;
  }

  get dniNoValido() {
    return this.isFormSubmitted && this.Persona.controls.Dni.errors;
  }

  get fechaNacimientoNoValido() {
    return this.isFormSubmitted && this.Persona.controls.FechaNac.errors;
  }

  get mailNoValido() {
    return this.isFormSubmitted && this.Persona.controls.Mail.errors;
  }

  get telefonoNoValido() {
    return this.isFormSubmitted && this.Persona.controls.Telefono.errors;
  }

  get domicilioNoValido() {
    return this.isFormSubmitted && this.Persona.controls.Domicilio.errors;
  }

  get alturaNoValido() {
    return this.isFormSubmitted && this.Persona.controls.Altura.errors;
  }


}









