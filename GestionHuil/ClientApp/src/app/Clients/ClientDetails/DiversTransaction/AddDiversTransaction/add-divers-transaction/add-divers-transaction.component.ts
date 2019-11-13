import { Component, OnInit , Inject} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../../../format-datepicker';
import { DatePipe } from '@angular/common';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { DiversTransactionService } from '../../../../../../Service/divers-transaction.service';
@Component({
  selector: 'app-add-divers-transaction',
  templateUrl: './add-divers-transaction.component.html',
  styleUrls: ['./add-divers-transaction.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class AddDiversTransactionComponent implements OnInit {
  createDiversTransactionForm: FormGroup;
  createDiversTransactionFormNew: any;
  clientbase: any;
  isLoading = false;
  submitted = false;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<AddDiversTransactionComponent> ,
              private diversTransactionService: DiversTransactionService ,
              private snackBar: MatSnackBar, private datePipe: DatePipe) {
      this.createDiversTransactionForm = this.formBuilder.group({
        date: [],
        clientId: [''],
        typeTransaction: ['', Validators.required],
        montant: [, Validators.required],
        typeDePaiement: [9, Validators.required],
    }); }

  ngOnInit() {
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.createDiversTransactionForm.invalid) {
      return;
  }
    particulierform.date = this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss');
    this.createDiversTransactionFormNew = {
      date: particulierform.date ,
      clientId: this.data.id,
      typeTransaction: particulierform.typeTransaction,
      montant: particulierform.montant,
      typeDePaiement: particulierform.typeDePaiement,
    montantVente: particulierform.montantVente,
  };
    this.diversTransactionService.add(this.createDiversTransactionFormNew).subscribe(
      data => {
               this.openSnackBar('Divers Transaction a été Modfier avec succès' , 'Ok'); } ,
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
  return this.createDiversTransactionForm.controls[controlName].hasError(errorName);
}
fermerDialog() {
  this.dialogRef.close();
}

}
