import { Component, OnInit , Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef , MAT_DIALOG_DATA } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../format-datepicker';
import { DatePipe } from '@angular/common';
import { VenteHuileService } from '../../../../Service/vente-huile.service';
@Component({
  selector: 'app-edit-vente-huile',
  templateUrl: './edit-vente-huile.component.html',
  styleUrls: ['./edit-vente-huile.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class EditVenteHuileComponent implements OnInit {
  editVenteform: FormGroup;
  formSubmitEditVenteNew: any;
  database: any;
  submitted = false;
  varietebase: any;
  qteLocale: number = Number(0);
  prixLocale: number = Number(9);
  resulteMontant: number;
  qtebidonLocal: number = Number(0);
  prixbidonLocal: number = Number(9);
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private formBuilder: FormBuilder,
              private venteHuileService: VenteHuileService ,
              public dialogRef: MatDialogRef<EditVenteHuileComponent> , private snackBar: MatSnackBar) {
                this.editVenteform = this.formBuilder.group({
                  id: [''],
                  date: [''],
                  clientId: ['', Validators.required],
                  varieteId: ['', Validators.required],
                  qte_bidon: [0],
                  prix_bidon: [1.5],
                  qte_Vente: [, Validators.required],
                  prix_Unitaire: [],
                  montantVente: [],
               });
              }

  ngOnInit() {
    this.venteHuileService.getById(this.data.id).subscribe(data => {
      this.database = data;
      this.editVenteform.setValue({
        id: this.data.id,
        date: this.database.date,
        clientId: this.database.clientId,
        varieteId: this.database.varieteId,
        qte_Vente: this.database.qte_Vente,
        prix_Unitaire: this.database.prix_Unitaire,
        qte_bidon: this.database.qte_bidon,
        prix_bidon: this.database.prix_bidon,
        montantVente: this.database.montantVente,
      });
      this.qteLocale = this.database.qte_Vente;
      this.prixLocale = this.database.prix_Unitaire;
      this.resulteMontant = this.database.montantVente;
      this.qtebidonLocal = this.database.qte_bidon;
      this.prixbidonLocal = this.database.prix_bidon;
    }, error => {
      console.log('error to upload coordoes');
    });
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.editVenteform.invalid) {
      return;
  }
    this.formSubmitEditVenteNew = {
    id: this.database.id,
      date: this.database.date,
    clientId: this.database.clientId,
    varieteId: this.database.varieteId,
    qte_Vente: particulierform.qte_Vente,
    qte_bidon: particulierform.qte_bidon,
    prix_bidon: particulierform.prix_bidon,
    prix_Unitaire: particulierform.prix_Unitaire,
    montantVente: particulierform.montantVente,
  };
    this.venteHuileService.update(this.formSubmitEditVenteNew).subscribe(
      data => {
               this.openSnackBar('Vente Huile a été Modfier avec succès' , 'Ok'); } ,
      error => {this.openSnackBar('Quantité insifisante' , 'Ok');
                console.log(error);
                this.ngOnInit();
    });
    this.dialogRef.close();
}
onSearchChangea(event: number): void {

  this.qteLocale = event;
  this.prixLocale = this.editVenteform.get('prix_Unitaire').value;
  this.resulteMontant = this.qteLocale * this.prixLocale + Number(this.qtebidonLocal * this.prixbidonLocal);
  this.editVenteform.get('montantVente').setValue(Number(this.resulteMontant).toFixed(3));

}

onSearchChangeb(event: number): void {
  this.prixLocale = event;
  this.qteLocale = this.editVenteform.get('qte_Vente').value;
  this.resulteMontant = this.qteLocale * this.prixLocale + Number(this.qtebidonLocal * this.prixbidonLocal);
  this.editVenteform.get('montantVente').setValue(Number(this.resulteMontant).toFixed(3));
}
onSearchChangee(event: number): void {
  this.qtebidonLocal = event;
  this.prixbidonLocal = this.editVenteform.get('prix_bidon').value;
  this.resulteMontant = this.qteLocale * this.prixLocale + Number(this.qtebidonLocal * this.prixbidonLocal);
  this.editVenteform.get('montantVente').setValue(Number(this.resulteMontant).toFixed(3));
}
onSearchChangec(event: number): void {
  this.prixbidonLocal = event;
  this.qtebidonLocal = this.editVenteform.get('qte_bidon').value;
  this.resulteMontant = this.qteLocale * this.prixLocale + Number(this.qtebidonLocal * this.prixbidonLocal);
  this.editVenteform.get('montantVente').setValue(Number(this.resulteMontant).toFixed(3));
}

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.editVenteform.controls[controlName].hasError(errorName);
  }
  fermerDialog() {
    this.dialogRef.close();
  }

}
