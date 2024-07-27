import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { UntypedFormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DatasetFinanzasService } from '../../services/dataset-finanzas.service';
import { UsuarioService } from '../../services/usuario.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogoConfirmacionService } from '../../services/dialogo-confirmacion.service';

@Component({
  selector: 'datos-finanzas-economicos-borrado',
  templateUrl: './datos-finanzas-economicos-borrado.component.html',
  styleUrls: ['./datos-finanzas-economicos-borrado.component.css']
})
export class DatosFinanzasEconomicosBorradoComponent implements OnInit {
  @Input() isMantenimiento = true; //A ESTO DEBO DARLE EVENTO DE CLICK PARA GESTION
  financieros: any;
  idArchivo: any;
  p: number = 1;
  cabeceras: string[] = ["Nombre de Archivo", "Ubicación"];

  constructor(private datasetfinanzasService: DatasetFinanzasService, private dialogoConfirmacionService: DialogoConfirmacionService, private usuarioService: UsuarioService, private modalService: NgbModal, private router: Router, private formBuilder: UntypedFormBuilder) {
    this.form = new UntypedFormGroup({
      //'NombreUser': new FormControl("", Validators.required),
      //'Contrasenia': new FormControl("", Validators.required)
    });
  }

  form: UntypedFormGroup;

  abrirDialogoConfirmacion() {
    this.dialogoConfirmacionService.confirm('Confirmación', '¿Estás seguro de que deseas borrar el Dataset?',this.idArchivo)
      .then((result) => {
        if (result) {
          this.borrarDatosAbiertos(this.idArchivo);
          console.log('Confirmado');
        } else {
          console.log('Cancelado');
        }
      })
      .catch(() => console.log('Modal cerrado sin confirmar'));
  }

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




  //borrarDatosAbiertos(idArchivo: any) {
  ////  console.log("recibo Id Archivo" + idArchivo);
  //  this.datasetfinanzasService.eliminarDataset(idArchivo).subscribe(data => {
  //    this.datasetfinanzasService.ListarFinancieros().subscribe(data => this.financieros = data);  //Refrescaremos la info del ngoninit
  //  });
  //}
  borrarDatosAbiertos(idArchivo: number) {
    // Abre el modal de confirmación
    this.dialogoConfirmacionService.confirm('Confirmación', '¿Estás seguro de que deseas borrar este archivo?', idArchivo)
      .then((result) => {
        if (result) {
          // Si el usuario confirma, llama al método para eliminar el dataset
          this.datasetfinanzasService.eliminarDataset(idArchivo).subscribe(data => {
            // Refresca la lista de financieros después de eliminar
            this.datasetfinanzasService.ListarFinancieros().subscribe(data => {
              this.financieros = data; // Actualiza la lista
            });
          });
        }
      })
      .catch(() => console.log('Modal cerrado sin confirmar'));
  }




  //fin de la clase
}
