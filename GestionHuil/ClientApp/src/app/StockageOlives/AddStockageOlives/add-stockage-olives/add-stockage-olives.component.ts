import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import {map, startWith} from 'rxjs/operators';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../format-datepicker';
import { DatePipe } from '@angular/common';
import {Observable} from 'rxjs';
import { StockageOlivesService } from '../../../../Service/stockage-olives.service';
import { ClientServiceService } from '../../../../Service/client-service.service';
export interface Client {
  id: number;
  nom: string;
  tel: string;
  gsm: string;
  ville: string;
}
@Component({
  selector: 'app-add-stockage-olives',
  templateUrl: './add-stockage-olives.component.html',
  styleUrls: ['./add-stockage-olives.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class AddStockageOlivesComponent implements OnInit {
  createstockageForm: FormGroup;
  clientbase: any;
  varietebase: any;
  isLoading = false;
  submitted = false;
  selectedClient: Client;
  filteredClients: Observable<any[]>;
  formSubmitStockage: any;
  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<AddStockageOlivesComponent> ,
              private stockageOlivesService: StockageOlivesService ,
              private clientService: ClientServiceService ,
              private snackBar: MatSnackBar , private datePipe: DatePipe) {
                this.createstockageForm = this.formBuilder.group({
                  date: [''],
                  clientId: ['', Validators.required],
                  varieteId: ['', Validators.required],
                  poids: ['', Validators.required],
                  vehicule: [''],
                  chauffeur: [''],
              });
               }

  ngOnInit() {
    this.clientService.getAll().subscribe(data => {
      this.clientbase  = data;
      this.filteredClients = this.createstockageForm.get('clientId').valueChanges.pipe(
      startWith(''),
      map(value => typeof value === 'string' ? value : value.nom),
       map(client => client ? this.filterClient(client) : this.clientbase.slice()));

    },
    error => {
      console.log('There was an error while retrieving Posts !!!' + error);
    }
    );
    this.stockageOlivesService.getAllvarite().subscribe(data => {
      this.varietebase = data;
    } ,
    error => {
      console.log('There was an error while retrieving Posts !!!' + error);
    });
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.createstockageForm.invalid) {
      return;
  }
    console.log(particulierform);
    this.formSubmitStockage = particulierform;
    this.formSubmitStockage.date = this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss');
    this.formSubmitStockage.clientId = particulierform.clientId.id;
    this.stockageOlivesService.add(this.formSubmitStockage).subscribe(
      data => {
               this.openSnackBar('Stockge Olivies a été ajouté avec succès' , 'Ok'); } ,
      error => {this.openSnackBar(error , 'Ok'); console.log(error);
    });
    this.dialogRef.close();
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
  onNoClick(): void {
    this.dialogRef.close();
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.createstockageForm.controls[controlName].hasError(errorName);
  }

}
