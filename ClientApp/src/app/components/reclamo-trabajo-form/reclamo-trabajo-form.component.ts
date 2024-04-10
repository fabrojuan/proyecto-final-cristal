import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { ReclamoService } from '../../services/reclamo.service';

@Component({
  selector: 'reclamo-trabajo-form',
  templateUrl: './reclamo-trabajo-form.component.html',
  styleUrls: ['./reclamo-trabajo-form.component.css']
})
export class ReclamoTrabajoFormComponent implements OnInit {
  Trabajo: UntypedFormGroup;
  Empleados: any;
  Prioridades: any;
  parametro: any;
  NroReclamo: any;
  idEmpleado: any;

  constructor(private TrabajoService: TrabajoService, private usuarioService: UsuarioService, private router: Router,
              private activatedRoute: ActivatedRoute, private reclamoService: ReclamoService) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"];
      if (this.parametro >= 1) {
        console.log(this.parametro);
      } else {
        this.NroReclamo = 0;
      }
    });

    this.Trabajo = new UntypedFormGroup(
      {
        "Id_Usuario": new UntypedFormControl("0"),
        "Id_Vecino": new UntypedFormControl("0"),
        "descripcionReclamo": new UntypedFormControl(""),
        "Descripcion": new UntypedFormControl("", [Validators.required, Validators.minLength(50), Validators.maxLength(2500)]),
        "Nro_Prioridad": new UntypedFormControl("3"),
        "estado_Reclamo": new UntypedFormControl(""),
        "tipoReclamo": new UntypedFormControl(""),
        "nro_Reclamo": new UntypedFormControl("0"),
        "legajoActual": new UntypedFormControl("0"),
        "nomApeVecino": new UntypedFormControl("")


        //"Mail": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),

      });
  }
  ngOnInit() {
    this.TrabajoService.getUsuario().subscribe(data => this.Empleados = data);
    this.TrabajoService.getPrioridad().subscribe(data => this.Prioridades = data);
    //LLamamos AL SERVICIO que me traiga el id de sesion para cargarle el trabajo al usuario actual logueado. 
    // this.usuarioService.obtenerSessionidEmpleado().subscribe(empleado => {
    //   // this.Reclamo.controls["idVecino"].setValue(idvecino.valor);
    //   this.idEmpleado = empleado.valor;
    // });
    if (this.parametro >= 1) {
      this.TrabajoService.RecuperarReclamo(this.parametro).subscribe(param => {
        
        

      });

      this.reclamoService.getReclamo(this.parametro).subscribe(data => {
        this.Trabajo.controls["nro_Reclamo"].setValue(data.nroReclamo);
        this.Trabajo.controls["estado_Reclamo"].setValue(data.estadoReclamo);
        this.Trabajo.controls["descripcionReclamo"].setValue(data.descripcion);
        this.Trabajo.controls["tipoReclamo"].setValue(data.tipoReclamo);
        this.Trabajo.controls["nomApeVecino"].setValue(data.nombreYapellido);

        this.Trabajo.controls["Id_Usuario"].setValue(this.idEmpleado);  //este es el empleado que tiene gnada la denuncia.
        this.Trabajo.controls["Id_Vecino"].setValue(data.idVecino);
      });
    } else {
    }
  }
  guardarDatos() {
    if (this.Trabajo.valid == true) {
      this.TrabajoService.GuardarTrabajoReclamo(this.Trabajo.value).subscribe(data => {
        if (data == 1) {
          var inf = data;
          this.router.navigate(["/reclamo-tabla"]);

        }
        else {
          console.log("No redirecciona la url, no toma la variable del controller")
        }
      });

    }
  }
  clickMethod() {
    alert("Se registró el usuario correctamente");
    //Luego de presionar click debe redireccionar al home
  }
}
