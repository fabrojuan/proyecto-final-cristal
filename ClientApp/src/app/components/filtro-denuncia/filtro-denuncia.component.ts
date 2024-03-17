import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DenunciaService } from '../../services/denuncia.service';

@Component({
  selector: 'filtro-denuncia',
  templateUrl: './filtro-denuncia.component.html',
  styleUrls: ['./filtro-denuncia.component.css']
})
export class FiltroDenunciaComponent implements OnInit {
  error: boolean = false;
  TiposDenuncia: any;
  urlBase: string = "";
  Denuncia: UntypedFormGroup;
  constructor(private denunciaservice: DenunciaService, private router: Router, @Inject('BASE_URL') baseUrl: string) {
    this.urlBase = baseUrl;
    this.Denuncia = new UntypedFormGroup(
      {
        'Descripcion': new UntypedFormControl("",[Validators.maxLength(2500)]),
             }
    );
  }
  ngOnInit() {
  }


  filtrarDenuncias(){
    if (this.Denuncia.valid == true) {

      this.denunciaservice.filtrardenuncia(this.Denuncia.value).subscribe(res => {
        if (res.idUsuario == 0 || res.idUsuario == "") {
          this.error = true;
          //Completar aca debo,
         //devolver un modal que diga que no hay resultados con la busqueda.
        }
        else {
          //Esta Ok
          this.error = false;
          //Devolver el listado de denuncias en la tabla con el resultado correspondiente. Acorde el criterio de busqueda por descripcion.
        }
        console.log(res);

      });
    }
  }



}



