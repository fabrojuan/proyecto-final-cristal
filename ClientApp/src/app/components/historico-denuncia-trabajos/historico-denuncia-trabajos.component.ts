import { Component, OnInit, Input} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TrabajoService } from '../../services/trabajo.service';

@Component({
  selector: 'historico-denuncia-trabajos',
  templateUrl: './historico-denuncia-trabajos.component.html',
  styleUrls: ['./historico-denuncia-trabajos.component.css']
})
export class HistoricoDenunciaTrabajosComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  parametro: any;
  trabajos: any;
  cabeceras: string[] = ["Fecha", "Descripcion", "Empleado"];
  constructor(private TrabajoService: TrabajoService, private router: Router, private activatedRoute: ActivatedRoute) {
    // Inicializa la propiedad fila en el constructor
    this.activatedRoute.params.subscribe(parametro => {
      this.parametro = parametro["id"];
      if (this.parametro >= 1) {
      } else {
        console.log("No adopto el parametro");
      }
    });
  }

  ngOnInit() {
    if (this.parametro >= 1) {
      this.TrabajoService.ListarTrabajosDenunciasCerradas(this.parametro).subscribe(data => this.trabajos = data);
    }

  }
  //ngAfterViewInit() {
  //  const cell = this.fila.nativeElement.querySelector('.description-cell');
  //  cell.style.height = `${cell.scrollHeight}px`;
  //}

  volver() {
    this.router.navigate(["/historico-denuncia-tabla"]);
  }
}





