import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { CaissesService } from '../../../../Service/caisses.service';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../format-datepicker';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-add-caisse',
  templateUrl: './add-caisse.component.html',
  styleUrls: ['./add-caisse.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class AddCaisseComponent implements OnInit {
  createclientForm: FormGroup;
  createclientNewForm: any;
  submitted = false;
  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<AddCaisseComponent> ,
              private caissesService: CaissesService ,
              private snackBar: MatSnackBar , private datePipe: DatePipe) {
                this.createclientForm = this.formBuilder.group({
                  date: [''],
                  montant: ['', Validators.required],
                  personne: [' ', Validators.required]
              });
               }

  ngOnInit() {
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.createclientForm.invalid) {
      return;
  }
    this.createclientNewForm = {
      date: this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss') ,
      montant: particulierform.montant ,
      personne: particulierform.personne
    };
    this.caissesService.add(this.createclientNewForm).subscribe(
      data => {
               this.openSnackBar('Caisse a été ajouté avec succès' , 'Ok'); } ,
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
    return this.createclientForm.controls[controlName].hasError(errorName);
  }

}
