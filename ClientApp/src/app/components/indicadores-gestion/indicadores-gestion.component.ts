import { Component, OnInit } from '@angular/core';
import { IndicadoresGraficosComponent } from '../indicadores-graficos/indicadores-graficos.component';
import { IndicadoresService } from 'src/app/services/indicadores.service';

@Component({
  selector: 'app-indicadores-gestion',
  templateUrl: './indicadores-gestion.component.html',
  styleUrls: ['./indicadores-gestion.component.css']
})
export class IndicadoresGestionComponent implements OnInit {

  tituloPeriodoIndicadorTiempoMedioResolucionRequerimientos: string = "Este Mes"
  tituloPeriodoIndicadorTiempoMedioResolucionDenuncias: string = "Este Mes"
  valorActual: string = ""
  valorVariacion: string = ""
  tipoVariacion: string = ""

  constructor(private indicadoresService: IndicadoresService) { }

  ngOnInit(): void {
    this.cargarDatos("MES");
  }

  private cargarDatos(tipoPeriodo: string) {
    this.indicadoresService.getIndicadorTiempoMedioResolucionRequerimientosGeneral(tipoPeriodo)
      .subscribe(res => {
        console.log(res);
        this.valorActual = res.valorActual;
        this.valorVariacion = res.valorVariacion;
        this.tipoVariacion = res.tipoVariacion;
      });
  }

  verTiempoMedioResolucionRequerimientosMes() {
    this.tituloPeriodoIndicadorTiempoMedioResolucionRequerimientos = "Este Mes"
    this.cargarDatos("MES");
  }

  verTiempoMedioResolucionRequerimientosTrimestre() {
    this.tituloPeriodoIndicadorTiempoMedioResolucionRequerimientos = "Este Trimestre"
    this.cargarDatos("TRIMESTRE");
  }

  verTiempoMedioResolucionRequerimientosAnio() {
    this.tituloPeriodoIndicadorTiempoMedioResolucionRequerimientos = "Este Año"
    this.cargarDatos("ANIO");
  }

  verTiempoMedioResolucionDenunciasMes() {
    this.tituloPeriodoIndicadorTiempoMedioResolucionDenuncias = "Este Mes"
  }

  verTiempoMedioResolucionDenunciasTrimestre() {
    this.tituloPeriodoIndicadorTiempoMedioResolucionDenuncias = "Este Trimestre"
  }

  verTiempoMedioResolucionDenunciasAnio() {
    this.tituloPeriodoIndicadorTiempoMedioResolucionDenuncias = "Este Año"
  }

}
