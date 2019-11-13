import { Component, OnInit, Input , OnChanges} from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';
import { Label } from 'ng2-charts';
@Component({
  selector: 'app-vente-statistique',
  templateUrl: './vente-statistique.component.html',
  styleUrls: ['./vente-statistique.component.scss']
})
export class VenteStatistiqueComponent implements OnInit , OnChanges {
  @Input() myData: any;
  total: any = null;
  montant: any = null;
  public barChartOptions: ChartOptions = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    scales: { xAxes: [{}], yAxes: [{}] },
    plugins: {
      datalabels: {
        anchor: 'end',
        align: 'end',
      }
    }
  };
  barChartLabels: Label[] = ['Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi', 'Dimanche'];
  barChartType: ChartType = 'bar';
  barChartLegend = true;
  barChartPlugins = [pluginDataLabels];
  public barChartData: ChartDataSets[] = [
    { data: [], label: 'Huile' },
    { data: [], label: 'Montant' }
  ];
  constructor() { }
  ngOnChanges(changes: any): void {

    // tslint:disable-next-line:prefer-const
    let personChange = changes.myData;
    if (personChange) {
      this.total = this.getValueTotal();
      this.montant = this.getValueMotant();
      this.barChartData[0].data =  this.total;
      this.barChartData[1].data =  this.montant;
    }
    }

  ngOnInit() {
    this.total = this.getValueTotal();
    this.montant = this.getValueMotant();
    this.barChartData[0].data =  this.total;
    this.barChartData[1].data =  this.montant;
  }
  getValueTotal() {
    return this.myData.map((o) => o.total);
  }
  getValueMotant() {
    return this.myData.map((o) => o.montant);
  }

}
