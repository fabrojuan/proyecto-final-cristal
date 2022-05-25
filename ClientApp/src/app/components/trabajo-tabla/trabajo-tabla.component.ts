import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';

@Component({
  selector: 'trabajo-tabla',
  templateUrl: './trabajo-tabla.component.html',
  styleUrls: ['./trabajo-tabla.component.css']
})
export class TrabajoTablaComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  parametro: any;
  trabajos: any;
  cabeceras: string[] = ["Fecha", "Numero de Trabajo", "Descripcion", "Leg. Empleado"];
  constructor(private TrabajoService: TrabajoService, private router: Router, private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"];
      if (this.parametro >= 1) {
        console.log(this.parametro);
      } else {
        console.log("No adopto el parametro");
      }
    });
  }

  ngOnInit() {
    if (this.parametro >= 1) {
      this.TrabajoService.ListarTrabajos(this.parametro).subscribe(data => this.trabajos = data);

    }

  }
  volver() {
    this.router.navigate(["/tabla-denuncia"]);
  }
}








