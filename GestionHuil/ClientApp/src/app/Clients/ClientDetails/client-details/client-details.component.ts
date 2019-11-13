import { Component, OnInit } from '@angular/core';
import { ClientServiceService } from '../../../../Service/client-service.service';
import {  ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrls: ['./client-details.component.scss']
})
export class ClientDetailsComponent implements OnInit {
clientBaseId: any;
clientBase: any;
constructor(private clientService: ClientServiceService , private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      // tslint:disable-next-line:no-string-literal
      this.clientBaseId = params['id'];
    });
    this.clientService.getById(this.clientBaseId).subscribe(
      res => {
        this.clientBase = res;
      },
      error => { console.log('There was an error while retrieving Posts !!!' + error);
      });
  }

}
