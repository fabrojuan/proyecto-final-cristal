import { Component, OnInit } from '@angular/core';
import { ReclamoService } from '../../services/reclamo.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SugerenciaService } from '../../services/sugerencia.service';
import { Router, RouterModule } from '@angular/router';
@Component({
  selector: 'sugerencia-form-generar',
  templateUrl: './sugerencia-form-generar.component.html',
  styleUrls: ['./sugerencia-form-generar.component.css']
})
export class SugerenciaFormGenerarComponent implements OnInit {
  TiposReclamo: any;
  nombreVecino: any;
  idVecino: any;
  Sugerencia: FormGroup;
  constructor(private sugerenciaservice: SugerenciaService, private router: Router) {
    this.Sugerencia = new FormGroup(
      {
        "Descripcion": new FormControl("", [Validators.required, Validators.minLength(50)]),

      }
    );
  }

  ngOnInit() {
    //this.sugerenciaservice.getTipoSugerencia().subscribe(data => this.TiposSugerencia = data);


  }
  guardarDatos() {

    if (this.Sugerencia.valid == true) {
      this.sugerenciaservice.agregarSugerencia(this.Sugerencia.value).subscribe(data => { });
      this.router.navigate(["/"]);
    }
  }
  clickMethod() {
    alert("La sugerencia se ha generada correctamente, agradecemos su compromiso para mejorar ");



  }


}
