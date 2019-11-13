import { Component, OnInit } from '@angular/core';
import { TriturationService } from '../../../../Service/trituration.service';
import { ClientServiceService } from '../../../../Service/client-service.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../format-datepicker';
import { DatePipe } from '@angular/common';
export interface Client {
  id: number;
  nom: string;
  tel: string;
  gsm: string;
  ville: string;
}

@Component({
  selector: 'app-add-trituration',
  templateUrl: './add-trituration.component.html',
  styleUrls: ['./add-trituration.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class AddTriturationComponent implements OnInit {
  createclientForm: FormGroup;
  clientbase: any ;
  filteredClients: Observable<any[]>;
  isLoading = false;
  submitted = false;
  formSubmitTrituration: any;
  formSubmitTriturationNew: any;
  selectedClient: Client;
  resulteMontant: number;
  qteLocale: number = Number(0);
  prixLocale: number = Number(0.8);
  qteLivreeLocale: number;
  qteRestantLocale: number;
  ListStockageOlive: any;
  calculPoids: number;
  calculrendement: number;
  // private formSubmitAttempt: boolean;
  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<AddTriturationComponent> ,
              private triturationService: TriturationService ,
              private clientService: ClientServiceService ,
              private snackBar: MatSnackBar , private datePipe: DatePipe) {
                this.createclientForm = this.formBuilder.group({
                  date: [''],
                  clientId: ['', Validators.required],
                  poids: [{value: '', disabled: true}, Validators.required],
                  prixUnitaire: [ 0.8 , Validators.required],
                  montant: [{value: 0, disabled: true}, Validators.required],
                  huileObtenu: [null, Validators.required],
                  qteLivree: [null, Validators.required],
                  huileRestante: [{value: 0, disabled: true}, Validators.required],
                  rendement: [{value: 0, disabled: true}],
                  stockageOlives: [[], Validators.required]
              });
               }
  ngOnInit() {
    this.clientService.getAll().subscribe(data => {
      this.clientbase  = data;
      this.filteredClients = this.createclientForm.get('clientId').valueChanges.pipe(
      startWith(''),
      map(value => typeof value === 'string' ? value : value.nom),
       map(client => client ? this.filterClient(client) : this.clientbase.slice()));

    },
    error => {
      console.log('There was an error while retrieving Posts !!!' + error);
    }
    );
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  changeClient($event) {
    this.calculPoids = 0;
    // tslint:disable-next-line:prefer-const
    let compteur = $event.value;
    compteur.forEach(element => {
      this.calculPoids = this.calculPoids + element.poids;
    });
    this.createclientForm.get('poids').setValue(this.calculPoids);
    if (this.qteLocale !== null) {
      this.calculrendement = (((this.qteLocale / this.calculPoids)) * 28 );
      this.createclientForm.get('rendement').setValue(Number(this.calculrendement).toFixed(3));
    } else {
      this.createclientForm.get('rendement').setValue(Number(0));
    }
}
  callSomeFunction(event) {
    this.triturationService.getAllStockNew(event.id).subscribe(data => {
        this.ListStockageOlive = data;
    }, error => {
      console.log(error);
    });
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.createclientForm.invalid) {
      return;
  }
    this.formSubmitTrituration = particulierform;
    console.log(this.formSubmitTrituration);
    this.formSubmitTriturationNew = {
      date: this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss'),
      poids: this.calculPoids,
      prixUnitaire: this.formSubmitTrituration.prixUnitaire,
      montant: this.resulteMontant,
      huileObtenu: this.formSubmitTrituration.huileObtenu,
      qteLivree: this.formSubmitTrituration.qteLivree,
      rendement: this.calculrendement,
      huileRestante: this.qteRestantLocale,
      clientId: this.formSubmitTrituration.stockageOlives[0].clientId,
      varieteId: this.formSubmitTrituration.stockageOlives[0].varieteId,
      stockageOlives:  this.formSubmitTrituration.stockageOlives
    };
    console.log(this.formSubmitTriturationNew);
    // tslint:disable-next-line:only-arrow-functions
    this.formSubmitTriturationNew.stockageOlives.map( function(item: any ) {
      delete item.client;
      delete item.variete;
      return item;
  });
    this.formSubmitTriturationNew.rendement = Number(this.calculrendement).toFixed(3);
    console.log(this.formSubmitTriturationNew);
    this.triturationService.add(this.formSubmitTriturationNew).subscribe(
      data => {
               this.openSnackBar('Trituration a été ajouté avec succès' , 'Ok'); }
      , error => {this.openSnackBar(error , 'Ok'); console.log(error);
    });
    this.dialogRef.close();
    // stop here if form is invalid

  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
  filterClient(value: any): Client[] {
    const filterValue = value.toLowerCase();
    return this.clientbase.filter(option => option.nom.toLowerCase().indexOf(filterValue) === 0);
  }
  displayFn(client) {
    this.selectedClient = client;
    return  client.nom ;
  }
  onSearchChangea(event: number): void {

    this.qteLocale = event;
    this.resulteMontant = this.calculPoids * this.prixLocale;
    this.createclientForm.get('montant').setValue(Number(this.resulteMontant).toFixed(3));
    this.calculrendement = (((this.qteLocale / this.calculPoids)) * 28 );
    this.createclientForm.get('rendement').setValue(Number(this.calculrendement).toFixed(3));
  }

  onSearchChangeb(event: number): void {
    this.prixLocale = event;
    this.resulteMontant = this.calculPoids * this.prixLocale;
    this.createclientForm.get('montant').setValue(Number(this.resulteMontant).toFixed(3));
  }
  onSearchChangec(event: number): void {
    this.qteLivreeLocale = this.createclientForm.get('qteLivree').value;
    this.qteRestantLocale = this.qteLocale - this.qteLivreeLocale ;
    this.createclientForm.get('huileRestante').setValue(this.qteRestantLocale);
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.createclientForm.controls[controlName].hasError(errorName);
  }
  fermerDialog() {
    this.dialogRef.close();
  }







}
