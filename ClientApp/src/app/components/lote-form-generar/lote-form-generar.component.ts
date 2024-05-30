import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { LoteService } from '../../services/lote.service';
import { UntypedFormGroup, UntypedFormControl, Validators, UntypedFormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';
//Validaciones de inputs:
import { ControlValueAccessor, Validator, AbstractControl, ValidationErrors, NG_VALUE_ACCESSOR, NG_VALIDATORS } from '@angular/forms';
import { CampoRequeridoComponent } from '../campo-requerido/campo-requerido.component';

@Component({
  selector: 'lote-form-generar',
  templateUrl: './lote-form-generar.component.html',
  styleUrls: ['./lote-form-generar.component.css'],
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
  ],
})
export class LoteFormGenerarComponent implements OnInit, ControlValueAccessor, Validator {
  TiposLote: any;
  redirectPersona: number = 0;
  public Lote: UntypedFormGroup;
  idLote: string = "";
  dniTitular: string = "";
  tituloModal: string = "";
  resultadoGuardadoModal: any = "";
  esquina: any = 0;
  asfaltado: any = 0;
  //Validaciones
  onTouched: () => void = () => { };
  onChange: (value: any) => void = () => { };
  subscriptions: Subscription;


  @ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any> | undefined;
  //Esta linea anterior es para el modal.

  constructor(private loteservice: LoteService, private router: Router, private modalService: NgbModal, private formBuilder: UntypedFormBuilder) {
    this.subscriptions = new Subscription();
    this.Lote = this.formBuilder.group(
      {
        "CodTipoInmueble": new UntypedFormControl("", [Validators.required]),
        "NomenclaturaCatastral": new UntypedFormControl("", [Validators.required]),
        'Manzana': new UntypedFormControl("", [Validators.required, Validators.maxLength(3), Validators.pattern("^[0-9]{1,3}")]),
        'NroLote': new UntypedFormControl("", [Validators.required, Validators.maxLength(2), Validators.pattern("^[0-9]{1,2}")]),
        "Calle": new UntypedFormControl("", [Validators.required, Validators.maxLength(100)]),
        "Altura": new UntypedFormControl("", [Validators.required, Validators.maxLength(4), Validators.pattern("^[0-9]{1,4}")]),
        "Bhabilitado": new UntypedFormControl("1"),
        "SupTerreno": new UntypedFormControl("", [Validators.required, Validators.maxLength(4), Validators.pattern("^[0-9]{1,4}")]),  //, [Validators.required,Validators.maxLength(100),Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]
        "SupEdificada": new UntypedFormControl("", [Validators.required, Validators.maxLength(4), Validators.pattern("^[0-9]{1,4}")]),
        "DniTitular": new UntypedFormControl("", [Validators.required, Validators.maxLength(8), Validators.pattern("^[0-9]{8}")]),
        "Esquina": new UntypedFormControl("0"),
        "Asfaltado": new UntypedFormControl("0"),
      },
      { validators: this.todoslosCamposRequeridos }
    );
  }

  ngOnInit(): void {
    this.loteservice.getTipoLote().subscribe(data => this.TiposLote = data);

  }
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
  validate(control: AbstractControl): ValidationErrors | null {
    return this.todoslosCamposRequeridos(control);
  }

  todoslosCamposRequeridos(control: AbstractControl): ValidationErrors | null {
    const controlValue = control.value;
    const isValid = controlValue.NomenclaturaCatastral &&
      controlValue.Manzana &&
      controlValue.NroLote &&
      controlValue.Calle &&
      controlValue.Altura &&
      controlValue.SupTerreno &&
      controlValue.SupEdificada &&
      controlValue.DniTitular &&
      controlValue.CodTipoInmueble && (controlValue.CodTipoInmueble !== '1' || controlValue.genderOther);
    return isValid ? null : { required: true };
  }
  writeValue(value: any): void {
    value && this.Lote.setValue(value, { emitEvent: false });
  }

  registerOnChange(onChange: (value: any) => void): void {
    this.subscriptions.add(this.Lote.valueChanges.subscribe(onChange));
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  setDisabledState(disabled: boolean): void {
    disabled ? this.Lote.disable() : this.Lote.enable();
  }
  verCheck() {
    var checks = document.getElementsByClassName("check");
    var check: any;  //Array checks
    check = checks;
    if (check[0].checked == true) {
      this.esquina = true;
      //this.Lote.setValue.name.match.(Esquina, this.esquina)
      this.Lote.controls["Esquina"].setValue(this.esquina);
    }
    else {
      if (check[0].checked == false) {
        this.esquina = false;
      }
      // this.esquina = 0;
    }

  }
  verCheckAsfalto() {
    var checks = document.getElementsByClassName("checkAsfalto");
    var check: any;  //Agrego un any a la variable check y no jodió mas.
    check = checks;
    if (check[0].checked == true) {
      this.asfaltado = true;
      //this.Lote.setValue.name.match.(Esquina, this.esquina)
      this.Lote.controls["Asfaltado"].setValue(this.asfaltado);
    }
    else {
      this.asfaltado = false;
      // this.asfaltado = 0;
    }

  }


  guardarDatos() {
    if (this.Lote.valid == true) {

      console.log(this.Lote.value);
      this.loteservice.agregarLote(this.Lote.value).subscribe(data => {
        if (data) {
       
          this.resultadoGuardadoModal = "Se ha generado Lote correctamente.";

        }
        else
          this.resultadoGuardadoModal = " Lote no se ha podido registrar genere un ticket con el error en nuestra pestaña de problemas";
      });


      this.modalService.open(this.myModalInfo);
      this.router.navigate(["/"]);
    }
  }

  VerificarPreexistenciaPersona() {
    this.dniTitular = this.Lote.get('DniTitular')?.value;
    this.tituloModal = "Busqueda de Persona Registrada";
    //Hay una carga de persona cuando registro empleado reutilizar eso, ademas crear pagina usuario que está creda como tabla-usuario y no al reves como corresponde.
    this.loteservice.getPersonaPreExistente(this.dniTitular).subscribe((data: any) => {
      if (data > 0) {
        console.log(data);

        this.resultadoGuardadoModal = "La Persona ha sido cargada previamente en el sistema puede continuar con el registro.";
        this.redirectPersona = 0;
      }
      else {
        this.resultadoGuardadoModal = "La persona no está registrada sera redireccionado para el registro posteriormente continuar con el lote.";
        this.redirectPersona = 1;
      }
    });
    this.modalService.open(this.myModalInfo);
  }

  BuscarLotePrexistente() {
    this.idLote = this.Lote.get('NomenclaturaCatastral')?.value;
    this.tituloModal = "Busqueda de Lote previamente registrado";
    this.loteservice.getLotePreExistente(this.idLote).subscribe(data => {
      if (data > 0) {
        console.log(data);
        this.resultadoGuardadoModal = "El Lote ya existe en el sistema edite el lote existente si corresponde.";
      }
      else {
        this.resultadoGuardadoModal = "el Lote no está registrado puede continuar con el registro.";
      }
    });
    this.modalService.open(this.myModalInfo);

  }

  cerrarmodal() {
    this.modalService.dismissAll(this.myModalInfo);
    if (this.redirectPersona > 0) { this.router.navigate(["/persona-form-generar"]); }
    else { //resetear campos del modal ya que la persona esta registrada y seteamos lote.
      this.tituloModal = "Busqueda de Lote previamente registrado";
    }
  }

  volverLoteTabla() {
    this.router.navigate(["/lote-tabla"]);
  }

}

