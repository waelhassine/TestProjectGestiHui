import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import {map, startWith} from 'rxjs/operators';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../format-datepicker';
import { DatePipe } from '@angular/common';
import {Observable} from 'rxjs';
import {GrignonsService} from '../../../../Service/grignons.service';
import {ClientServiceService} from '../../../../Service/client-service.service';
export interface Client {
  id: number;
  nom: string;
  tel: string;
  gsm: string;
  ville: string;
}
@Component({
  selector: 'app-add-grignons',
  templateUrl: './add-grignons.component.html',
  styleUrls: ['./add-grignons.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class AddGrignonsComponent implements OnInit {
  createGrignonsForm: FormGroup;
  clientbase: any;
  isLoading = false;
  submitted = false;
  selectedClient: Client;
  filteredClients: Observable<any[]>;
  formSubmitVente: any;
  qteLocale: number = Number(0);
  prixLocale: number = Number(9);
  resulteMontant: number;
  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<AddGrignonsComponent> ,
              private grignonsService: GrignonsService ,
              private clientService: ClientServiceService  ,
              private snackBar: MatSnackBar , private datePipe: DatePipe) {
                this.createGrignonsForm = this.formBuilder.group({
                  date: [''],
                  clientId: ['', Validators.required],
                  vehicule: [''],
                  matricule: [''],
                  transporteur: [''],
                  chaufeur: [''],
                  poids: [, Validators.required],
                  prix_unitaire: [0.45, Validators.required],
                  montantAchat: [{value: 0, disabled: true}, Validators.required],
              });
               }

  ngOnInit() {
    this.clientService.getAll().subscribe(data => {
      this.clientbase  = data;
      this.filteredClients = this.createGrignonsForm.get('clientId').valueChanges.pipe(
      startWith(''),
      map(value => typeof value === 'string' ? value : value.nom),
       map(client => client ? this.filterClient(client) : this.clientbase.slice()));

    },
    error => {
      console.log('There was an error while retrieving Posts !!!' + error);
    }
    );
  }
  onSearchChangea(event: number): void {

    this.qteLocale = event;
    this.prixLocale = this.createGrignonsForm.get('prix_unitaire').value;
    this.resulteMontant = this.qteLocale * this.prixLocale;
    this.createGrignonsForm.get('montantAchat').setValue(Number(this.resulteMontant).toFixed(3));

  }

  onSearchChangeb(event: number): void {
    this.prixLocale = event;
    this.qteLocale = this.createGrignonsForm.get('poids').value;
    this.resulteMontant = this.qteLocale * this.prixLocale;
    this.createGrignonsForm.get('montantAchat').setValue(Number(this.resulteMontant).toFixed(3));
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.createGrignonsForm.invalid) {
      return;
  }
    this.formSubmitVente = particulierform;
    this.formSubmitVente.date = this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss');
    this.formSubmitVente.clientId = particulierform.clientId.id;
    this.formSubmitVente.montantAchat = Number(this.resulteMontant).toFixed(3);
    this.grignonsService.add(this.formSubmitVente).subscribe(
       data => {
                this.openSnackBar('Vente a été ajouté avec succès' , 'Ok'); } ,
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
    return this.createGrignonsForm.controls[controlName].hasError(errorName);
  }

}
