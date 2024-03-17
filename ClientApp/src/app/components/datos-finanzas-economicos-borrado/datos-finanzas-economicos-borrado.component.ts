import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DatasetFinanzasService } from '../../services/dataset-finanzas.service';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'datos-finanzas-economicos-borrado',
  templateUrl: './datos-finanzas-economicos-borrado.component.html',
  styleUrls: ['./datos-finanzas-economicos-borrado.component.css']
})
export class DatosFinanzasEconomicosBorradoComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  financieros: any;
  p: number = 1;
  cabeceras: string[] = ["Nombre de Archivo", "Ubicación"];
  constructor(private datasetfinanzasService: DatasetFinanzasService, private usuarioService: UsuarioService, private router: Router, private formBuilder: UntypedFormBuilder) {
    this.form = new UntypedFormGroup({
      //'NombreUser': new FormControl("", Validators.required),
      //'Contrasenia': new FormControl("", Validators.required)
    });
  }

  form: UntypedFormGroup;

  ngOnInit() {

    this.datasetfinanzasService.ListarFinancieros().subscribe(data => this.financieros = data);
    //this.form = this.formBuilder.group({
    //  nombreArchivo: '',
    //  ubicacion: 0
    //});

  }
  volver() {
    this.router.navigate(["/bienvenida"]);
  }

  borrarDatosAbiertos(idArchivo: any) {
  //  console.log("recibo Id Archivo" + idArchivo);
    this.datasetfinanzasService.eliminarDataset(idArchivo).subscribe(data => {
      this.datasetfinanzasService.ListarFinancieros().subscribe(data => this.financieros = data);  //Refrescaremos la info del ngoninit
    });
}


}
