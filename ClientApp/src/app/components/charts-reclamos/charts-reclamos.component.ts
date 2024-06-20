import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-charts-reclamos',
  templateUrl: './charts-reclamos.component.html',
  styleUrls: ['./charts-reclamos.component.css']
})
export class ChartsReclamosComponent implements OnInit {

  chartReclamosCerradosPorMes: Chart;

  constructor() { 
    this.chartReclamosCerradosPorMes = new Chart("chartReclamosCerradosPorMes", {
      type: 'bar',
      data: {
        labels: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Junio'],
        datasets: [{
            label: 'Solucionados',
            data: [75, 15, 18, 48, 74],
            backgroundColor: 'rgb(255, 99, 132)',
          },
          {
            label: 'Cancelados',
            data: [11, 1, 12, 62, 95],
            backgroundColor: 'rgb(75, 192, 192)',
          },
          {
            label: 'Rechazados',
            data: [44, 5, 22, 35, 62],
            backgroundColor: 'rgb(255, 205, 86)',
          },
        ]
      },
      options: {
        plugins: {
          title: {
            display: true,
            text: 'Chart.js Bar Chart - Stacked'
          },
        },
        responsive: true,
        scales: {
          x: {
            stacked: true,
          },
          y: {
            stacked: true
          }
        }
      }
    });

  }

  ngOnInit(): void {

    this.chartReclamosCerradosPorMes = new Chart("chartReclamosCerradosPorMes", {
      type: 'bar',
      data: {
        labels: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Junio'],
        datasets: [{
            label: 'Solucionados',
            data: [75, 15, 18, 48, 74],
            backgroundColor: 'rgb(255, 99, 132)',
          },
          {
            label: 'Cancelados',
            data: [11, 1, 12, 62, 95],
            backgroundColor: 'rgb(75, 192, 192)',
          },
          {
            label: 'Rechazados',
            data: [44, 5, 22, 35, 62],
            backgroundColor: 'rgb(255, 205, 86)',
          },
        ]
      },
      options: {
        plugins: {
          title: {
            display: true,
            text: 'Chart.js Bar Chart - Stacked'
          },
        },
        responsive: true,
        scales: {
          x: {
            stacked: true,
          },
          y: {
            stacked: true
          }
        }
      }
    });

  }

}
