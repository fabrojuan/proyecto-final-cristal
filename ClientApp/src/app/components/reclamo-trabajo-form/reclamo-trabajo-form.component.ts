import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'reclamo-trabajo-form',
  templateUrl: './reclamo-trabajo-form.component.html',
  styleUrls: ['./reclamo-trabajo-form.component.css']
})
export class ReclamoTrabajoFormComponent implements OnInit {
  Trabajo: FormGroup;
  Empleados: any;
  Prioridades: any;
  parametro: any;
  NroReclamo: any;
  idEmpleado: any;

  constructor(private TrabajoService: TrabajoService, private usuarioService: UsuarioService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"];
      if (this.parametro >= 1) {
        console.log(this.parametro);
      } else {
        this.NroReclamo = 0;
      }
    });

    this.Trabajo = new FormGroup(
      {
        "Id_Usuario": new FormControl("0"),
        "Id_Vecino": new FormControl("0"),
        "Descripcion": new FormControl("", [Validators.required, Validators.minLength(50), Validators.maxLength(2500)]),
        "Nro_Prioridad": new FormControl("3"),
        "estado_Reclamo": new FormControl(""),
        "nro_Reclamo": new FormControl("0"),
        "legajoActual": new FormControl("0"),
        "nomApeVecino": new FormControl("")


        //"Mail": new FormControl("", [Validators.required, Validators.maxLength(100), Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]),

      });
  }
  ngOnInit() {
    this.TrabajoService.getUsuario().subscribe(data => this.Empleados = data);
    this.TrabajoService.getPrioridad().subscribe(data => this.Prioridades = data);
    //LLamamos AL SERVICIO que me traiga el id de sesion para cargarle el trabajo al usuario actual logueado. 
    this.usuarioService.obtenerSessionidEmpleado().subscribe(empleado => {
      // this.Reclamo.controls["idVecino"].setValue(idvecino.valor);
      this.idEmpleado = empleado.valor;
    });
    if (this.parametro >= 1) {
      this.TrabajoService.RecuperarReclamo(this.parametro).subscribe(param => {
        this.Trabajo.controls["Id_Usuario"].setValue(this.idEmpleado);  //este es el empleado que tiene gnada la denuncia.
        this.Trabajo.controls["estado_Reclamo"].setValue(param.estadoReclamo);
        this.Trabajo.controls["nro_Reclamo"].setValue(param.nroReclamo);
        this.Trabajo.controls["Id_Vecino"].setValue(param.idVecino);
        this.Trabajo.controls["nomApeVecino"].setValue(param.nombreYapellido);

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
