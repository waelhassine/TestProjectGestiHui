import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';
import { StockageOlivesService } from '../../../../Service/stockage-olives.service';
@Component({
  selector: 'app-edit-stockage-olives',
  templateUrl: './edit-stockage-olives.component.html',
  styleUrls: ['./edit-stockage-olives.component.scss']
})
export class EditStockageOlivesComponent implements OnInit {
  editStockageform: FormGroup;
  formSubmitEditStockageNew: any;
  database: any;
  submitted = false;
  varietebase: any;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private formBuilder: FormBuilder,
              private stockageOlivesService: StockageOlivesService ,
              public dialogRef: MatDialogRef<EditStockageOlivesComponent> , private snackBar: MatSnackBar) {
    this.editStockageform = this.formBuilder.group({
      id: [''],
      date: [''],
      clientId: ['', Validators.required],
      varieteId: ['', Validators.required],
      poids: ['', Validators.required],
      vehicule: [''],
      chauffeur: [''],
  });
   }

  ngOnInit() {
    this.stockageOlivesService.getById(this.data.id).subscribe(data => {
      this.database = data;
      this.editStockageform.setValue({
        id: this.data.id,
        date: this.database.date,
        clientId: this.database.clientId,
        varieteId: this.database.varieteId,
        poids: this.database.poids,
        vehicule: this.database.vehicule,
        chauffeur: this.database.chauffeur,
      });
    }, error => {
      console.log('error to upload coordoes');
    });
    this.stockageOlivesService.getAllvarite().subscribe(data => {
      this.varietebase = data;
    } ,
    error => {
      console.log('There was an error while retrieving Posts !!!' + error);
    });
  }
  onSubmit(particulierform) {
    this.submitted = true;
    if (this.editStockageform.invalid) {
      return;
  }
    this.formSubmitEditStockageNew = {
    id: this.database.id,
      date: this.database.date,
    clientId: this.database.clientId,
    varieteId: particulierform.varieteId,
    poids: particulierform.poids,
    vehicule: particulierform.vehicule,
    chauffeur: particulierform.chauffeur,
  };
    this.stockageOlivesService.update(this.formSubmitEditStockageNew).subscribe(
      data => {
               this.openSnackBar('Stockage Olive a été Modfier avec succès' , 'Ok'); } ,
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
    return this.editStockageform.controls[controlName].hasError(errorName);
  }
  fermerDialog() {
    this.dialogRef.close();
  }

}
