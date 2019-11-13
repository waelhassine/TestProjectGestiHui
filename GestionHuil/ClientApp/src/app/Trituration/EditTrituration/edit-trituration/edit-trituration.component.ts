import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import { TriturationService } from '../../../../Service/trituration.service';
import {MatSnackBar} from '@angular/material/snack-bar';
export interface Client {
  id: number;
  nom: string;
  tel: string;
  gsm: string;
  ville: string;
}
@Component({
  selector: 'app-edit-trituration',
  templateUrl: './edit-trituration.component.html',
  styleUrls: ['./edit-trituration.component.scss']
})
export class EditTriturationComponent implements OnInit {
  edittriturationform: FormGroup;
  database: any;
  submitted = false;
  formSubmitTrituration: any;
  formSubmitTriturationNew: any;
  selectedClient: Client;
  resulteMontant: number;
  qteLocale: number;
  prixLocale: number = Number(0.8);
  qteLivreeLocale: number;
  qteRestantLocale: number;
  calculPoids: number;
  calculrendement: number;
  // private formSubmitAttempt: boolean;
  constructor(@Inject(MAT_DIALOG_DATA) public date: any, private formBuilder: FormBuilder, private triturationService: TriturationService ,
              public dialogRef: MatDialogRef<EditTriturationComponent> , private snackBar: MatSnackBar) {
                this.edittriturationform = this.formBuilder.group({
                  id: [''],
                  date: [''],
                  poids: [{value: 0, disabled: true}],
                  prixUnitaire: [ 0.8 ],
                  montant: [{value: 0, disabled: true}],
                  rendement: [{value: 0, disabled: true}],
                  huileObtenu: [null],
                  qteLivree: [null],
                  huileRestante: [{value: '', disabled: true}],
              });
               }

  ngOnInit() {
    this.triturationService.getById(this.date.id).subscribe(data => {
      this.database = data;
      this.edittriturationform.setValue({
        id: this.date.id,
        date: this.database.date,
        poids: this.database.poids,
        montant: this.database.montant,
        rendement: this.database.rendement,
        prixUnitaire: this.database.prixUnitaire,
        huileObtenu: this.database.huileObtenu,
        qteLivree: this.database.qteLivree,
        huileRestante: this.database.huileRestante,
      });

      this.qteLocale = this.edittriturationform.get('huileObtenu').value;
      this.qteLivreeLocale = this.edittriturationform.get('qteLivree').value;
      this.prixLocale = this.edittriturationform.get('prixUnitaire').value;
      this.calculPoids = this.database.poids;
      this.resulteMontant = this.calculPoids * this.prixLocale;
      this.calculrendement = this.edittriturationform.get('rendement').value;
      this.qteRestantLocale = this.qteLocale - this.qteLivreeLocale ;
      this.edittriturationform.get('huileRestante').setValue(this.qteRestantLocale);
    }, error => {
      console.log('error to upload coordoes');
    });
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.edittriturationform.invalid) {
      return;
  }
    this.formSubmitTriturationNew = {
      id: this.database.id,
      date: this.database.date,
      poids: this.database.poids,
      rendement: Number(this.calculrendement).toFixed(3),
      prixUnitaire: particulierform.prixUnitaire,
      montant: Number(this.resulteMontant).toFixed(3),
      huileObtenu: particulierform.huileObtenu,
      qteLivree: particulierform.qteLivree,
      huileRestante: this.qteRestantLocale,
      clientId: this.database.clientId,
      varieteId: this.database.varieteId
    };
    this.triturationService.update(this.formSubmitTriturationNew).subscribe(
      data => {
               this.openSnackBar('Trituration a été Modfier avec succès' , 'Ok');
               this.ngOnInit();
               this.dialogRef.close(); } ,
      error => {this.openSnackBar(error , 'Ok'); console.log(error);
    });
    this.dialogRef.close();
}

  onNoClick(): void {
    this.dialogRef.close();
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

  onSearchChangea(event: number): void {

    this.qteLocale = event;
    this.resulteMontant = this.calculPoids * this.prixLocale;
    this.edittriturationform.get('montant').setValue(Number(this.resulteMontant).toFixed(3));
    this.calculrendement = (((this.qteLocale / this.calculPoids)) * 28 );
    this.edittriturationform.get('rendement').setValue(Number(this.calculrendement).toFixed(3));
    this.qteRestantLocale = this.qteLocale - this.qteLivreeLocale ;
    this.edittriturationform.get('huileRestante').setValue(this.qteRestantLocale);
  }

  onSearchChangeb(event: number): void {
    this.prixLocale = event;
    this.resulteMontant = this.calculPoids * this.prixLocale;
    this.edittriturationform.get('montant').setValue(Number(this.resulteMontant).toFixed(3));
  }
  onSearchChangec(event: number): void {
    this.qteLivreeLocale = this.edittriturationform.get('qteLivree').value;
    this.qteRestantLocale = this.qteLocale - this.qteLivreeLocale ;
    this.edittriturationform.get('huileRestante').setValue(this.qteRestantLocale);
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.edittriturationform.controls[controlName].hasError(errorName);
  }
  fermerDialog() {
    this.dialogRef.close();
  }

}
