import { Component, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';
import HC_exporting from 'highcharts/modules/exporting';
import { ProjetoUR } from 'src/app/_models/ProjetoUR';
import { ProjetoURGlobal } from 'src/app/_models/ProjetoURGlobal';
import { Observable, interval, Subscription } from 'rxjs';
@Component({
  selector: 'app-widget-app-recursos',
  templateUrl: './recursos.component.html',
  styleUrls: ['./recursos.component.scss']
})
export class RecursosComponent implements OnInit {
    
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
        text: 'Recursos do projeto ' + ProjetoURGlobal.nome
    },
    subtitle: {
        text: ''
    },
    xAxis: {
        categories: ['Recursos'],
        title: {
            text: null
        }
    },
    yAxis: {
        min: 0,
        title: {
            text: 'Recursos Projeto',
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
        name: 'Recursos Previstos',
        data: [ProjetoURGlobal.rPrevistos]
    }, {
        name: 'Recursos Utilizados',
        data: [ProjetoURGlobal.rUtilizados]
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
