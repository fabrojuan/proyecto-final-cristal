import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { LoteService } from '../../services/lote.service';
import { VecinoService } from '../../services/vecino.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';
//Validaciones de inputs:
import { ControlValueAccessor, Validator, AbstractControl, ValidationErrors, NG_VALUE_ACCESSOR, NG_VALIDATORS } from '@angular/forms';
import { CampoRequeridoComponent } from '../campo-requerido/campo-requerido.component';

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
  public Persona: FormGroup;
  tituloModal: string = "Registro de Persona";
  resultadoGuardadoModal: any = "";
  //Validaciones
  onTouched: () => void = () => { };
  onChange: (value: any) => void = () => { };
  subscriptions: Subscription;


  @ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any> | undefined;
  //Esta linea anterior es para el modal.

  constructor(private loteservice: LoteService, private router: Router, private modalService: NgbModal, private vecinoservice: VecinoService, private formBuilder: FormBuilder) {
    this.subscriptions = new Subscription();
    this.Persona = this.formBuilder.group(
      {
        "Nombre": new FormControl("", [Validators.required, Validators.maxLength(65)]),
        "Apellido": new FormControl("", [Validators.required, Validators.maxLength(65)]),
        'Dni': new FormControl("", [Validators.required, Validators.maxLength(8), Validators.pattern("^[0-9]{8}")]),
        "FechaNac": new FormControl("", [Validators.required, Validators.maxLength(16), Validators.pattern("^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}")]),
        "Mail": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),
        "Telefono": new FormControl("", [Validators.required, Validators.maxLength(20), Validators.pattern("^[0-9]{1,20}")]),
        "Domicilio": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        "Altura": new FormControl("", [Validators.required, Validators.maxLength(4), Validators.pattern("^[0-9]{1,4}")]),
        "Bhabilitado": new FormControl("1"),
        "Iidpersona": new FormControl("0"),

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
    if (this.Persona.valid == true) {

      console.log(this.Persona.value);
      this.vecinoservice.GuardarPersona(this.Persona.value).subscribe(data => {
        if (data) {
          console.log(data);

          this.resultadoGuardadoModal = "Se ha registrado la persona correctamente.";

        }
        else
          this.resultadoGuardadoModal = "El mail de la persona ya ha sido registrado anteriormente utilice otro correo electrónico por favor.";
      });


      this.modalService.open(this.myModalInfo);
      this.router.navigate(["/"]);
    }
  }

  cerrarmodal() {
    this.modalService.dismissAll(this.myModalInfo);
    if (this.redirectPersona > 0)
      this.router.navigate(["/persona-form-generar"]);
  }

  volverLoteTabla() {
    this.router.navigate(["/lote-tabla"]);
  }


}









