import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';
import { UntypedFormGroup, UntypedFormControl, Validators, UntypedFormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'denuncia-generar',
  templateUrl: './denuncia-generar.component.html',
  styleUrls: ['./denuncia-generar.component.css']
})
export class DenunciaGenerarComponent implements OnInit {
  TiposDenuncia: any;
   Denuncia: UntypedFormGroup;
  foto: any;
  foto2: any;
  foto3: any;
  Rpta: number = 0;
  resultadoGuardadoModal: any = "";
  @ViewChild("myModalInfo", { static: false }) myModalInfo: TemplateRef<any> | undefined;
  //Esta linea anterior es para el modal.
  constructor(private denunciaservice: DenunciaService, private router: Router, private modalService: NgbModal, private formBuilder: UntypedFormBuilder) {
    this.Denuncia = this.formBuilder.group(
      {
        //"Nro_Denuncia": new FormControl("0"),
        "codTipoDenuncia": new UntypedFormControl("", [Validators.required]),
        //  'Descripcion': new FormControl("", [Validators.required, Validators.maxLength(2500)]),
        //  'Calle': new FormControl("", [Validators.required, Validators.maxLength(100)]),
        //  "Nombre_Infractor": new FormControl("pepe"),
        //  "Apellido_Infractor": new FormControl("argento"),
        //  "Bhabilitado": new FormControl("1"),
        //  "Mail_Notificacion": new FormControl("test"),  //, [Validators.required,Validators.maxLength(100),Validators.pattern("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")]
        //  "Movil_Notificacion": new FormControl("351155"),
        //  "Entre_Calles": new FormControl("", [Validators.required, Validators.maxLength(100)]),
        //  "Altura": new FormControl("", [Validators.required, Validators.maxLength(6)]),
        //  "foto": new FormControl(""),
        //  "foto2": new FormControl(""),
        //  "foto3": new FormControl("")
      }
    );
  }

  //Aqui vamos a leer el archivo
  changeFoto() {


    //Probar si funca esto que hice en la primer foto.
    var file = (<HTMLInputElement>document.getElementById("fupFoto")).files?.[0] || new Blob(['test text'], { type: 'text/plain' });
    var fileReader = new FileReader();

    fileReader.onloadend = () => {   //Uso el Arrowfunction sino me marca error con foto.

      this.foto = fileReader.result;
    }
    fileReader.readAsDataURL(file);
    // Foto 2
    var file2 = (<HTMLInputElement>document.getElementById("fupFoto2")).files?.[0] || new Blob(['test text'], { type: 'text/plain' });
    //var file2 = (<HTMLInputElement>document.getElementById("fupFoto2")).files[0];

    var fileReader2 = new FileReader();

    fileReader2.onloadend = () => {   //Uso el Arrowfunction sino me marca error con foto.

      this.foto2 = fileReader2.result;
    }
    fileReader2.readAsDataURL(file2);
    //Foto3
    var file3 = (<HTMLInputElement>document.getElementById("fupFoto3")).files?.[0] || new Blob(['test text'], { type: 'text/plain' });
    //var file3 = (<HTMLInputElement>document.getElementById("fupFoto3")).files[0]; original
    var fileReader3 = new FileReader();

    fileReader3.onloadend = () => {   //Uso el Arrowfunction sino me marca error con foto.

      this.foto3 = fileReader3.result;
    }
    fileReader3.readAsDataURL(file3);
  }

  ngOnInit() {
    this.denunciaservice.getTipoDenuncia().subscribe(data => this.TiposDenuncia = data);
    this.foto = "";
    this.foto2 = "";
    this.foto3 = "";
  }

  guardarDatos() {
    if (this.Denuncia.valid == true) {

      //this.Denuncia.controls["foto"].setValue(this.foto); //Seteo la foto antes de guardarla
      //this.Denuncia.controls["foto2"].setValue(this.foto2); //Seteo la foto antes de guardarla
      //this.Denuncia.controls["foto3"].setValue(this.foto3); //Seteo la foto antes de guardarla
      console.log(this.Denuncia.value);
 
      this.denunciaservice.agregarDenuncia(this.Denuncia.value).subscribe(data => { 

        if (data) {
          console.log(data);
          this.resultadoGuardadoModal = "Se ha generado la denuncia correctamente, pronto le notificaremos acerca de la misma.";

        }
        else
          this.resultadoGuardadoModal = "La denuncia no se ha podido registrar genere un ticket con el error en nuestra pestaña de problemas";
      });


      this.modalService.open(this.myModalInfo);
      //this.router.navigate(["/"]);
    }
  }

}
