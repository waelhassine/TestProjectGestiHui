import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../format-datepicker';
import { DatePipe } from '@angular/common';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { ReglementService } from '../../../../Service/reglement.service';
@Component({
  selector: 'app-ajouter-reglement-facture',
  templateUrl: './ajouter-reglement-facture.component.html',
  styleUrls: ['./ajouter-reglement-facture.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class AjouterReglementFactureComponent implements OnInit {
  ajouterReglementForm: FormGroup;
  ajouterReglementFormNew: any;
  isLoading = false;
  submitted = false;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<AjouterReglementFactureComponent> ,
              private reglementService: ReglementService ,
              private snackBar: MatSnackBar, private datePipe: DatePipe) {
                this.ajouterReglementForm = this.formBuilder.group({
                  date: [],
                  modeReglement: [''],
                  montant: [, Validators.required],
                  factureId: [9, Validators.required],
              });
               }

  ngOnInit() {
    console.log(this.data.id);
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.ajouterReglementForm.invalid) {
      return;
  }
    particulierform.date = this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss');
    this.ajouterReglementFormNew = {
      date: particulierform.date ,
      factureId: this.data.id,
      modeReglement: particulierform.modeReglement,
      montant: particulierform.montant,
  };
    console.log(this.ajouterReglementFormNew);
    this.reglementService.add(this.ajouterReglementFormNew).subscribe(
      data => {console.log(data);
               this.openSnackBar('Reglement a été Ajouter avec succès' , 'Ok'); } ,
      error => {this.openSnackBar(error , 'Ok'); console.log(error);
    });
    this.dialogRef.close();
}
openSnackBar(message: string, action: string) {
  this.snackBar.open(message, action, {
    duration: 2000,
  });
}
public hasError = (controlName: string, errorName: string) => {
  return this.ajouterReglementForm.controls[controlName].hasError(errorName);
}
fermerDialog() {
  this.dialogRef.close();
}

}
