import { Component, OnInit, Input } from '@angular/core';
import * as Highcharts from 'highcharts';
import HC_exporting from 'highcharts/modules/exporting';
import { ProjetoPieGlobal } from 'src/app/_models/ProjetoPieGlobal';


@Component({
  selector: 'app-widget-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.scss']
})
export class AreaComponent implements OnInit {

  chartOptions: {};

  Highcharts = Highcharts;

  constructor(
  ) {
    setTimeout(() => {
        this.plotPieChart();
    }, 500);
  }
  ngOnInit(): void {
    this.plotPieChart();
  }

  plotPieChart() {
    this.chartOptions = {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45,
                beta: 0
            }
        },
        title: {
            text: 'Projetos abertos/concluídos durante 1 ano'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                }
            }
        },
        exporting: {
            enabled: true
        },
        credits: {
            enabled: false
        },
        series: [{
            colorByPoint: true,
            data: ProjetoPieGlobal.data
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
