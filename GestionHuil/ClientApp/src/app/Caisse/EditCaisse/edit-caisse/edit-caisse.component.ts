import { Component, OnInit , Inject} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef , MAT_DIALOG_DATA } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../format-datepicker';
import { DatePipe } from '@angular/common';
import { CaissesService } from '../../../../Service/caisses.service';
@Component({
  selector: 'app-edit-caisse',
  templateUrl: './edit-caisse.component.html',
  styleUrls: ['./edit-caisse.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class EditCaisseComponent implements OnInit {
editFormCaisse: FormGroup;
formSubmitEditCaisse: any;
database: any;
submitted = false;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private formBuilder: FormBuilder,
              private caissesService: CaissesService , private datePipe: DatePipe ,
              public dialogRef: MatDialogRef<EditCaisseComponent> , private snackBar: MatSnackBar) {
                this.editFormCaisse = this.formBuilder.group({
                  id: [''],
                  date: [''],
                  montant: ['', Validators.required],
                  personne: ['', Validators.required],
               });
               }

  ngOnInit() {
    this.caissesService.getById(this.data.id).subscribe(data => {
      this.database = data;
      this.editFormCaisse.setValue({
        id: this.data.id,
        date: this.database.date,
        montant: this.database.montant,
        personne: this.database.personne,
      });
    }, error => {
      console.log('error to upload coordoes');
    });
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.editFormCaisse.invalid) {
      return;
  }
    this.formSubmitEditCaisse = {
      id: this.database.id,
      date: this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss') ,
      montant: particulierform.montant ,
      personne: particulierform.personne
    };
    this.caissesService.update(this.formSubmitEditCaisse).subscribe(
      data => {
               this.openSnackBar('Vente Huile a été Modfier avec succès' , 'Ok'); } ,
error => {this.openSnackBar('Quantité insifisante' , 'Ok');
          console.log(error);
          this.ngOnInit();
    });
    this.dialogRef.close();
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.editFormCaisse.controls[controlName].hasError(errorName);
  }

}
