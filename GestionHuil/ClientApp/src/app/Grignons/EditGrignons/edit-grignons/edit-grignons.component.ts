import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';
import { GrignonsService} from '../../../../Service/grignons.service';
@Component({
  selector: 'app-edit-grignons',
  templateUrl: './edit-grignons.component.html',
  styleUrls: ['./edit-grignons.component.scss']
})
export class EditGrignonsComponent implements OnInit {
  editGrignonsform: FormGroup;
  formSubmitGrigonsNew: any;
  database: any;
  submitted = false;
  qteLocale: number = Number(0);
  prixLocale: number = Number(9);
  resulteMontant: number;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private formBuilder: FormBuilder,
              private grignonsService: GrignonsService ,
              public dialogRef: MatDialogRef<EditGrignonsComponent> , private snackBar: MatSnackBar) {
                this.editGrignonsform = this.formBuilder.group({
                  id: [''],
                  date: [''],
                  clientId: ['', Validators.required],
                  vehicule: [''],
                  matricule: ['', Validators.required],
                  transporteur: [''],
                  chaufeur: [''],
                  poids: [ 0],
                  prix_unitaire: [0],
                  montantAchat: [{value: 0, disabled: true}, Validators.required],
              });
               }

  ngOnInit() {
    this.grignonsService.getById(this.data.id).subscribe(data => {
      this.database = data;
      this.editGrignonsform.setValue({
        id: this.data.id,
        date: this.database.date,
        clientId: this.database.clientId,
        vehicule: this.database.vehicule,
        matricule: this.database.matricule,
        transporteur: this.database.transporteur,
        chaufeur: this.database.chaufeur,
        poids: this.database.poids,
        prix_unitaire: this.database.prix_unitaire,
        montantAchat: this.database.montantAchat,
      });
      this.qteLocale = this.editGrignonsform.get('poids').value;
      this.prixLocale = this.editGrignonsform.get('prix_unitaire').value;
      this.resulteMontant = this.qteLocale * this.prixLocale;
    }, error => {
      console.log('error to upload coordoes');
    });

  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.editGrignonsform.invalid) {
      return;
  }
    this.formSubmitGrigonsNew = {
      id: this.database.id,
      date: this.database.date,
      vehicule: particulierform.vehicule,
      matricule: particulierform.matricule,
      transporteur: particulierform.transporteur,
      chaufeur: particulierform.chaufeur,
      poids: particulierform.poids,
      prix_unitaire: particulierform.prix_unitaire,
      montantAchat: Number(this.resulteMontant).toFixed(3),
      clientId: this.database.clientId,
    };
    this.grignonsService.update(this.formSubmitGrigonsNew).subscribe(
      data => {
               this.openSnackBar('Trituration a été Modfier avec succès' , 'Ok');
               this.ngOnInit();
               this.dialogRef.close(); } ,
      error => {this.openSnackBar(error , 'Ok'); console.log(error);
    });
    this.dialogRef.close();
}
onSearchChangea(event: number): void {
  this.qteLocale = event;
  this.prixLocale = this.editGrignonsform.get('prix_unitaire').value;
  this.resulteMontant = this.qteLocale * this.prixLocale;
  this.editGrignonsform.get('montantAchat').setValue(Number(this.resulteMontant).toFixed(3));

}

onSearchChangeb(event: number): void {
  this.prixLocale = event;
  this.qteLocale = this.editGrignonsform.get('poids').value;
  this.resulteMontant = this.qteLocale * this.prixLocale;
  this.editGrignonsform.get('montantAchat').setValue(Number(this.resulteMontant).toFixed(3));
}
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.editGrignonsform.controls[controlName].hasError(errorName);
  }
  fermerDialog() {
    this.dialogRef.close();
  }

}
