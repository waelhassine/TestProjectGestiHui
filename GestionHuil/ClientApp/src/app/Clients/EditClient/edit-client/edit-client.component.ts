import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import { ClientServiceService } from '../../../../Service/client-service.service';
import {MatSnackBar} from '@angular/material/snack-bar';
@Component({
  selector: 'app-edit-client',
  templateUrl: './edit-client.component.html',
  styleUrls: ['./edit-client.component.scss']
})
export class EditClientComponent implements OnInit {
  editclientform: FormGroup;
  database: any;
  submitted = false;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private formBuilder: FormBuilder, private clientService: ClientServiceService ,
              public dialogRef: MatDialogRef<EditClientComponent> , private snackBar: MatSnackBar) {
  this.editclientform = this.formBuilder.group({
    id: [this.data.id],
    nom: ['', Validators.required],
    tel: ['', Validators.required],
    gsm: ['', Validators.required],
    ville: [' ', Validators.required],
  }); }

  ngOnInit() {
    this.clientService.getById(this.data.id).subscribe(data => {
      this.database = data;
      this.editclientform.setValue({
        id: this.data.id,
        nom: this.database.nom,
        tel: this.database.tel,
        gsm: this.database.gsm,
        ville: this.database.ville
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
    if (this.editclientform.invalid) {
      return;
    }
    this.clientService.update(particulierform).subscribe(data => {
      this.openSnackBar('Contact  a été Modifier avec succès' , 'Ok');
    }, error => {
      this.openSnackBar(error , 'Ok');
    });
    this.dialogRef.close();

  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.editclientform.controls[controlName].hasError(errorName);
  }

}
