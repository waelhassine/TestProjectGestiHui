import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../../../format-datepicker';
import { DatePipe } from '@angular/common';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { DiversTransactionService } from '../../../../../../Service/divers-transaction.service';

@Component({
  selector: 'app-edit-divers-transaction',
  templateUrl: './edit-divers-transaction.component.html',
  styleUrls: ['./edit-divers-transaction.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class EditDiversTransactionComponent implements OnInit {
  editDiversTransactionForm: FormGroup;
  editDiversTransactionFormNew: any;
  diverstransactionbase: any;
  isLoading = false;
  submitted = false;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<EditDiversTransactionComponent> ,
              private diversTransactionService: DiversTransactionService ,
              private snackBar: MatSnackBar, private datePipe: DatePipe) {
                this.editDiversTransactionForm = this.formBuilder.group({
                  id: [''],
                  date: [],
                  clientId: [''],
                  typeTransaction: ['', Validators.required],
                  montant: [, Validators.required],
                  typeDePaiement: [9, Validators.required],
              }); }

  ngOnInit() {
    this.diversTransactionService.getById(this.data.id).subscribe(data => {
      this.diverstransactionbase = data;
      this.editDiversTransactionForm.setValue({
        id: this.diverstransactionbase.id,
        date: this.diverstransactionbase.date,
        clientId: this.diverstransactionbase.clientId,
        typeTransaction: this.diverstransactionbase.typeTransaction,
        montant: this.diverstransactionbase.montant,
        typeDePaiement: this.diverstransactionbase.typeDePaiement,
      });
    }, error => {

    });
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.editDiversTransactionForm.invalid) {
      return;
  }
    particulierform.date = this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss');
    this.editDiversTransactionFormNew = {
      id: this.diverstransactionbase.id,
      date: this.diverstransactionbase.date,
      clientId:  this.diverstransactionbase.clientId,
      typeTransaction: particulierform.typeTransaction,
      montant: particulierform.montant,
      typeDePaiement: particulierform.typeDePaiement,
  };
    this.diversTransactionService.update(this.editDiversTransactionFormNew).subscribe(
      data => {
               this.openSnackBar('Divers Transaction a été Modfier avec succès' , 'Ok'); } ,
      error => {this.openSnackBar(error , 'Ok');
    });
    this.dialogRef.close();
}
openSnackBar(message: string, action: string) {
  this.snackBar.open(message, action, {
    duration: 2000,
  });
}
public hasError = (controlName: string, errorName: string) => {
  return this.editDiversTransactionForm.controls[controlName].hasError(errorName);
}
fermerDialog() {
  this.dialogRef.close();
}

}
