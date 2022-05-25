import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { DatasetFinanzasService } from '../../services/dataset-finanzas.service';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'datos-finanzas-economicos',
  templateUrl: './datos-finanzas-economicos.component.html',
  styleUrls: ['./datos-finanzas-economicos.component.css']
})
export class DatosFinanzasEconomicosComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  financieros: any;
  p: number = 1;
  cabeceras: string[] = ["Nombre de Archivo", "Ubicación"];
  constructor(private datasetfinanzasService: DatasetFinanzasService, private usuarioService: UsuarioService, private formBuilder: FormBuilder) {
    this.form = new FormGroup({
      //'NombreUser': new FormControl("", Validators.required),
      //'Contrasenia': new FormControl("", Validators.required)
    });
  }

  form: FormGroup;

  ngOnInit() {

    this.datasetfinanzasService.ListarFinancieros().subscribe(data => this.financieros = data);
    //this.form = this.formBuilder.group({
    //  nombreArchivo: '',
    //  ubicacion: 0
    //});

  }



}


