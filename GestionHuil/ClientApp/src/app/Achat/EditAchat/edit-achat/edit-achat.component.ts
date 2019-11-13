import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import { AchatService } from '../../../../Service/achat.service';
import {MatSnackBar} from '@angular/material/snack-bar';
export interface Trituration {
  id: number;
  date: string;
  montant: number;
  huileRestante: number;
  client: {
    id: number,
            nom: string,
            tel: string,
            gsm: string,
            ville: string
  };
  variete: {
    id: number ,
    name: string
  };
}
@Component({
  selector: 'app-edit-achat',
  templateUrl: './edit-achat.component.html',
  styleUrls: ['./edit-achat.component.scss']
})
export class EditAchatComponent implements OnInit {
  editAchatForm: FormGroup;
  triturationbase: any;
  submitted = false;
  formSubmitAchat: any;
  formSubmitAchatNew: any;
  prixunitaireLocal: number;
  montantAchat: number;
  qteAchatLocal: number;
  maxAchat: number;
  constructor( @Inject(MAT_DIALOG_DATA) public data: any, private formBuilder: FormBuilder, private achatService: AchatService ,
               public dialogRef: MatDialogRef<EditAchatComponent> , private snackBar: MatSnackBar) {
                this.editAchatForm = this.formBuilder.group({
                  id: [''],
                  qteAchete: [],
                  triturationId: [0],
                  prix: [ 8.5000 , Validators.required],
                  montantAchat: [{value: 0, disabled: true}, Validators.required],
                  note: [''],
              });
                }

  ngOnInit() {
    this.achatService.getById(this.data.id).subscribe(data => {
      this.triturationbase = data;
      this.editAchatForm.setValue({
        id: this.data.id,
        qteAchete: this.triturationbase.qteAchete,
        triturationId: this.triturationbase.triturationId,
        prix: this.triturationbase.prix_unitaire,
        montantAchat: this.triturationbase.montantAchat,
        note: this.triturationbase.note,
      });
      this.montantAchat = this.triturationbase.montantAchat;
      this.qteAchatLocal = this.triturationbase.qteAchete;
      this.maxAchat = this.qteAchatLocal;
      this.prixunitaireLocal = this.triturationbase.prix_unitaire;
    }, error => {
      console.log('error to upload coordoes');
    });

  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.editAchatForm.invalid) {
      return;
  }
    this.formSubmitAchatNew = {
      id: this.triturationbase.id,
      qteAchete: particulierform.qteAchete,
      triturationId: this.triturationbase.triturationId,
      prix_unitaire: particulierform.prix,
      montantAchat: this.montantAchat,
      note: particulierform.note,

  };
    this.achatService.update(this.formSubmitAchatNew).subscribe(
      data => {
               this.openSnackBar('Achat a été Modfier avec succès' , 'Ok'); } ,
      error => {this.openSnackBar(error , 'Ok');
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
public hasError = (controlName: string, errorName: string) => {
  return this.editAchatForm.controls[controlName].hasError(errorName);
}
fermerDialog() {
  this.dialogRef.close();
}
onSearchChangeb(event: number): void {
  this.prixunitaireLocal = event;
  this.qteAchatLocal = this.editAchatForm.get('qteAchete').value;
  this.montantAchat = (this.prixunitaireLocal * this.qteAchatLocal );
  this.editAchatForm.get('montantAchat').setValue(Number(this.montantAchat).toFixed(3));
}
onSearchChangea(event: number): void {
  this.qteAchatLocal = event;
  this.prixunitaireLocal = this.editAchatForm.get('prix').value;
  this.montantAchat = (this.prixunitaireLocal * this.qteAchatLocal);
  this.editAchatForm.get('montantAchat').setValue(Number(this.montantAchat).toFixed(3));
}

}
