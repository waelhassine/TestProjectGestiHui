import { Component, OnInit } from '@angular/core';
import { StatistiqueService } from '../../../Service/statistique.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-statistiques',
  templateUrl: './statistiques.component.html',
  styleUrls: ['./statistiques.component.scss']
})
export class StatistiquesComponent implements OnInit {
  formfilter: FormGroup;
  pipe: DatePipe;
  public triturationdata: any;
  public statistiqueData: any;
  public achatdata: any;
  public ventedata: any;
  public grignonsdata: any;

  constructor( private statistiqueService: StatistiqueService ,
               private formBuilder: FormBuilder) {
                this.formfilter = this.formBuilder.group({
                  AnyDayInWeek: new Date(2018, 7, 5)

                  });
                }

  ngOnInit() {

    this.statistiqueService.getAll().subscribe(
      dataa => {
                this.statistiqueData = dataa;
                this.triturationdata = this.statistiqueData.weekTriturations;
                this.achatdata = this.statistiqueData.weekAchats;
                this.ventedata = this.statistiqueData.weekVentes;
                this.grignonsdata = this.statistiqueData.weekGrignons;
                // this.statistiqueService.setOption(this.triturationdata);
      }, error => {

      }
    );



  }
  addEvent(event) {
    const val = event.value;
    this.pipe = new DatePipe('en');
    this.formfilter.setValue({
      AnyDayInWeek : this.pipe.transform(val, 'MM/dd/yyyy') ,
    });
    const serializedForm = JSON.stringify(this.formfilter.value);
    this.statistiqueService.getByWeek(serializedForm).subscribe( data => {
      this.statistiqueData = data;
      this.triturationdata = this.statistiqueData.weekTriturations;
      this.achatdata = this.statistiqueData.weekAchats;
      this.ventedata = this.statistiqueData.weekVentes;
      this.grignonsdata = this.statistiqueData.weekGrignons;
                // this.statistiqueService.setOption(this.triturationdata);
     } ,
     error => { console.log(error); });
  }

}
