import { Component, OnInit } from '@angular/core';
import { ClientServiceService } from '../../../../Service/client-service.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
@Component({
  selector: 'app-add-client',
  templateUrl: './add-client.component.html',
  styleUrls: ['./add-client.component.scss']
})
export class AddClientComponent implements OnInit {
  createclientForm: FormGroup;
  submitted = false;
  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<AddClientComponent> ,
              private clientervice: ClientServiceService ,
              private snackBar: MatSnackBar) {
                this.createclientForm = this.formBuilder.group({
                  nom: ['', Validators.required],
                  tel: [''],
                  gsm: ['', Validators.required],
                  ville: [' ', Validators.required]
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
    this.clientervice.add(particulierform).subscribe(
      data => {
               this.openSnackBar('Contact a été ajouté avec succès' , 'Ok'); } ,
      error => {this.openSnackBar('Client en Systéme' , 'Ok');
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
