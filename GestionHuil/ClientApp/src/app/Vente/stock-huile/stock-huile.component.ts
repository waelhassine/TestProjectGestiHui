import { Component, OnInit } from '@angular/core';
import { StockageHuileService } from '../../../Service/stockage-huile.service';
@Component({
  selector: 'app-stock-huile',
  templateUrl: './stock-huile.component.html',
  styleUrls: ['./stock-huile.component.scss']
})
export class StockHuileComponent implements OnInit {
  dataSource: any;
  constructor(private stockageHuileService: StockageHuileService) { }

  ngOnInit() {
    this.stockageHuileService.getAll().subscribe(data => {
      this.dataSource = data;
    } , error => {
        console.log('ERROR !!!!!!');
    });
  }

}
