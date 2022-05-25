import { Component, OnInit } from '@angular/core';
import { DenunciaService } from '../../services/denuncia.service';
import { UsuarioService } from '../../services/usuario.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IndicadoresService } from '../../services/indicadores.service';


@Component({
  selector: 'bienvenida',
  templateUrl: './bienvenida.component.html',
  styleUrls: ['./bienvenida.component.css']
})
export class BienvenidaComponent implements OnInit {

  idEmpleado: any;
  nombreEmpleado: any;
  chartDenuncia: any;
  totalDenuncias: any;
  totalDenunAsignaEmpleado: any;

  constructor(private usuarioService: UsuarioService, private indicadoresService: IndicadoresService) { }
  ngOnInit() {
    this.indicadoresService.getDenunciasxEmpleado().subscribe(chartDenuncia => {
      this.idEmpleado = chartDenuncia.idUsuario;
      this.nombreEmpleado = chartDenuncia.nombreEmpleado;
      this.totalDenuncias = chartDenuncia.totalDenuncias;
      this.totalDenunAsignaEmpleado = chartDenuncia.totalDenunAsignaEmpleado;
      console.log(this.idEmpleado, this.nombreEmpleado, this.totalDenuncias, this.totalDenunAsignaEmpleado);
    });


  }


}




