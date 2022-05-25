import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';
@Component({
  selector: 'reclamo-trabajo-tabla',
  templateUrl: './reclamo-trabajo-tabla.component.html',
  styleUrls: ['./reclamo-trabajo-tabla.component.css']
})
export class ReclamoTrabajoTablaComponent implements OnInit {

  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  parametro: any;
  trabajos: any;
  cabeceras: string[] = ["Fecha", "Numero de Reclamo", "Descripcion", "Leg. Empleado"];
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
      this.TrabajoService.ListarTrabajosReclamo(this.parametro).subscribe(data => this.trabajos = data);

    }
  }

}


