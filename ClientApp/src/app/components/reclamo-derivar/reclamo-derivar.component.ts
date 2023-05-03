import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Usuario } from 'src/app/modelos_Interfaces/Usuario';
import { ReclamoService } from 'src/app/services/reclamo.service';
import { ToastService } from 'src/app/services/toast.service';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-reclamo-derivar',
  templateUrl: './reclamo-derivar.component.html',
  styleUrls: ['./reclamo-derivar.component.css']
})
export class ReclamoDerivarComponent implements OnInit {

  listaUsuarios: Usuario[] = [];
  usuarioSeleccionado: Usuario = {
    idUsuario: 0,
    nombreUser: '',
    nombreCompleto: ''
  };
  nroReclamo: number = 0;

  constructor(private _usuarioService: UsuarioService,
              private _reclamoService: ReclamoService,
              private _activatedRouter: ActivatedRoute,
              private _router: Router,
              public _toastService: ToastService) {

    this._activatedRouter.params.subscribe(params => {
      this.nroReclamo = params['id'];
    });


    _usuarioService.getUsuarios().subscribe(usuarios => {
      console.log(usuarios);
      this.listaUsuarios = usuarios
    });

  }

  ngOnInit(): void {

  }

  guardar() {
    
    if (this.usuarioSeleccionado.idUsuario == 0 
      || this.usuarioSeleccionado.idUsuario == undefined) {
      this._toastService.show("Debe seleccionar un empleado", { classname: 'bg-danger text-light', delay: 5000 });
      return;
    }

    this._reclamoService
      .modificarReclamo(this.nroReclamo, 
                        { 
                          idUsuarioAsignado: this.usuarioSeleccionado.idUsuario
                        }).subscribe(res => 
                          { 
                            this._toastService.show(`Se registró con éxito la derivación`, { classname: 'bg-success text-light', delay: 5000 })
                            this._router.navigate(["/reclamo-tabla"]);                        
                          });
  }

  cancelar() {
    this._router.navigate(["/reclamo-tabla"]);
  }

}
