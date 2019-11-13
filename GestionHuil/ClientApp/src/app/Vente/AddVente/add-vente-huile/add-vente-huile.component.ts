import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import {map, startWith} from 'rxjs/operators';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../format-datepicker';
import { DatePipe } from '@angular/common';
import {Observable} from 'rxjs';
import { VenteHuileService } from '../../../../Service/vente-huile.service';
import { ClientServiceService } from '../../../../Service/client-service.service';
export interface Client {
  id: number;
  nom: string;
  tel: string;
  gsm: string;
  ville: string;
}
@Component({
  selector: 'app-add-vente-huile',
  templateUrl: './add-vente-huile.component.html',
  styleUrls: ['./add-vente-huile.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class AddVenteHuileComponent implements OnInit {
  createVenteForm: FormGroup;
  clientbase: any;
  varietebase: any;
  isLoading = false;
  submitted = false;
  selectedClient: Client;
  filteredClients: Observable<any[]>;
  formSubmitVente: any;
  qteLocale: number = Number(0);
  prixLocale: number = Number(9);
  resulteMontant: number;
  qtebidonLocal: number = Number(0);
  prixbidonLocal: number = Number(9);
  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<AddVenteHuileComponent> ,
              private venteHuileService: VenteHuileService ,
              private clientService: ClientServiceService  ,
              private snackBar: MatSnackBar , private datePipe: DatePipe) {
                this.createVenteForm = this.formBuilder.group({
                  date: [''],
                  clientId: ['', Validators.required],
                  varieteId: ['', Validators.required],
                  qte_bidon: [0],
                  prix_bidon: [1.5],
                  qte_Vente: [, Validators.required],
                  prix_Unitaire: [9, Validators.required],
                  montantVente: [{value: 0, disabled: true}, Validators.required],
              });
               }

  ngOnInit() {
    this.clientService.getAll().subscribe(data => {
      this.clientbase  = data;
      this.filteredClients = this.createVenteForm.get('clientId').valueChanges.pipe(
      startWith(''),
      map(value => typeof value === 'string' ? value : value.nom),
       map(client => client ? this.filterClient(client) : this.clientbase.slice()));

    },
    error => {
      console.log('There was an error while retrieving Posts !!!' + error);
    }
    );
    this.venteHuileService.getAllVariete().subscribe(data => {
      this.varietebase = data;
    } ,
    error => {
      console.log('There was an error while retrieving Posts !!!' + error);
    });

  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.createVenteForm.invalid) {
      return;
  }
    this.formSubmitVente = particulierform;
    this.formSubmitVente.date = this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss');
    this.formSubmitVente.clientId = particulierform.clientId.id;
    this.formSubmitVente.montantVente = Number(this.resulteMontant).toFixed(3);
    this.venteHuileService.add(this.formSubmitVente).subscribe(
      data => {
               this.openSnackBar('Vente a été ajouté avec succès' , 'Ok'); } ,
      error => {this.openSnackBar(error.error , 'Ok'); console.log(error);
    });
    this.dialogRef.close();

}
onSearchChangea(event: number): void {
  this.qteLocale = event;
  this.prixbidonLocal = this.createVenteForm.get('prix_Unitaire').value;
  this.resulteMontant = this.qteLocale * this.prixLocale + Number(this.qtebidonLocal * this.prixbidonLocal);
  this.createVenteForm.get('montantVente').setValue(Number(this.resulteMontant).toFixed(3));
}
onSearchChangee(event: number): void {
  this.qtebidonLocal = event;
  this.prixbidonLocal = this.createVenteForm.get('prix_bidon').value;
  this.resulteMontant = this.qteLocale * this.prixLocale + Number(this.qtebidonLocal * this.prixbidonLocal);
  this.createVenteForm.get('montantVente').setValue(Number(this.resulteMontant).toFixed(3));
}
onSearchChangec(event: number): void {
  this.prixbidonLocal = event;
  this.qtebidonLocal = this.createVenteForm.get('qte_bidon').value;
  this.resulteMontant = this.qteLocale * this.prixLocale + Number(this.qtebidonLocal * this.prixbidonLocal);
  this.createVenteForm.get('montantVente').setValue(Number(this.resulteMontant).toFixed(3));
}

onSearchChangeb(event: number): void {
  this.prixLocale = event;
  this.qteLocale = this.createVenteForm.get('qte_Vente').value;
  this.resulteMontant = this.qteLocale * this.prixLocale + Number(this.qtebidonLocal * this.prixbidonLocal);
  this.createVenteForm.get('montantVente').setValue(Number(this.resulteMontant).toFixed(3));
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
    return this.createVenteForm.controls[controlName].hasError(errorName);
  }

}
