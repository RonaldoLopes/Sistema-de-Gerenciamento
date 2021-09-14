import { Component, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';
import HC_exporting from 'highcharts/modules/exporting';
import { ProjetoUR } from 'src/app/_models/ProjetoUR';
import { ProjetoURGlobal } from 'src/app/_models/ProjetoURGlobal';
import { Observable, interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-widget-horasimp',
  templateUrl: './horasimp.component.html',
  styleUrls: ['./horasimp.component.scss']
})
export class HorasimpComponent implements OnInit {

  private updateSubscription: Subscription;
  Highcharts = Highcharts;
  chartOptions = {};
  valPrev: any;
  valUtil: any;
  projCod: any;

  nameProje: any;

  projetoUR: ProjetoUR[];

  constructor(
  ) {
    setTimeout(() => {
      this.plotGraphUR();
    }, 1000);
  }

  ngOnInit(): void { 
      this.plotGraphUR();
      this.updateSubscription = interval(30000).subscribe(
        () => {
            this.plotGraphUR();
        }
      );
  }

  plotGraphUR() {
    this.chartOptions = {
      chart: {
        type: 'bar'
    },
    title: {
        text: 'Horas de Implementação ' + ProjetoURGlobal.nome
    },
    subtitle: {
        text: ''
    },
    xAxis: {
        categories: ['Horas'],
        title: {
            text: null
        }
    },
    yAxis: {
        min: 0,
        title: {
            text: 'Horas Projeto',
            align: 'high'
        },
        labels: {
            overflow: 'justify'
        }
    },
    tooltip: {
        valueSuffix: ''
    },
    plotOptions: {
        bar: {
            dataLabels: {
                enabled: true
            }
        }
    },
    legend: {
      layout: 'vertical',
      align: 'right',
      verticalAlign: 'middle',
      itemMarginTop: 10,
      itemMarginBottom: 10,
      floating: false,
      borderWidth: 1,
      backgroundColor:
      Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
      shadow: true
    },
    credits: {
        enabled: false
    },
    series: [{
        name: 'Horas Prev. Implem.',
        data: [ProjetoURGlobal.hPrevistasI]
    }, {
        name: 'Horas Util. Implem.',
        data: [ProjetoURGlobal.hUtilizadasI]
    }]
    };
    HC_exporting(Highcharts);
    setTimeout(() => {
        window.dispatchEvent(
        new Event('resize')
        );
    }, 300);
  }

}
