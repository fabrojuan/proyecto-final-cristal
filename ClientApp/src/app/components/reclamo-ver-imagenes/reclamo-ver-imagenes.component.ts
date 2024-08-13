import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ReclamoService } from 'src/app/services/reclamo.service';

@Component({
  selector: 'app-reclamo-ver-imagenes',
  templateUrl: './reclamo-ver-imagenes.component.html',
  styleUrls: ['./reclamo-ver-imagenes.component.css']
})
export class ReclamoVerImagenesComponent implements OnInit {

  @Input() reclamo: any = {};
  imagenes: string[] = [];
  hayFoto2: boolean = false;

  constructor(public activeModal: NgbActiveModal, public reclamoService: ReclamoService) {   }

  ngOnInit(): void {

    if (this.reclamo.idImagen1) {
      this.reclamoService.getImagenReclamo(this.reclamo.nroReclamo, this.reclamo.idImagen1).subscribe(
        data => {
          this.imagenes.push(data);
        }
      );
      
    }

    if (this.reclamo.idImagen2) {
      this.reclamoService.getImagenReclamo(this.reclamo.nroReclamo, this.reclamo.idImagen2).subscribe(
        data => {
          this.imagenes.push(data);
          this.hayFoto2 = true;
        }
      );
    }

  }

}
