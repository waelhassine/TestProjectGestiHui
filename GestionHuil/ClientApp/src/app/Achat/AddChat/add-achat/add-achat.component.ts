import { Client } from './../../../Trituration/AddTrituration/add-trituration/add-trituration.component';
import { Component, OnInit } from '@angular/core';
import { TriturationService } from '../../../../Service/trituration.service';
import { AchatService } from '../../../../Service/achat.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { TouchSequence } from 'selenium-webdriver';
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
  selector: 'app-add-achat',
  templateUrl: './add-achat.component.html',
  styleUrls: ['./add-achat.component.scss']
})
export class AddAchatComponent implements OnInit {
  createAchatForm: FormGroup;
  triturationbase: any ;
  filteredTrituration: Observable<any[]>;
  isLoading = false;
  submitted = false;
  formSubmitAchat: any;
  formSubmitAchatNew: any;
  selectedTrituration: Trituration;
  idTriturationSelected: number;
  prixunitaireLocal: number;
  qteAchatLocal: number;
  QteRestantEnStock: number;
  montantAchat: number;

  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<AddAchatComponent> ,
              private triturationService: TriturationService ,
              private achatService: AchatService ,
              private snackBar: MatSnackBar ) {
      this.createAchatForm = this.formBuilder.group({
        qteAchete: [0],
        triturationId: ['', Validators.required],
        prix: [ 8.5000 , Validators.required],
        montantAchat: [{value: 0, disabled: true}, Validators.required],
        note: [''],
    });
     }
  ngOnInit() {
    this.achatService.getAllTrituration().subscribe(data => {
      this.triturationbase  = data;
      this.filteredTrituration = this.createAchatForm.get('triturationId').valueChanges.pipe(
      startWith(''),
      map(value => typeof value === 'string' ? value : value.client.nom),
       map(trituration => trituration ? this.filterTrituration(trituration) : this.triturationbase.slice()));

    },
    error => {
      console.log('There was an error while retrieving Posts !!!' + error);
    }
    );
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
  filterTrituration(value: any): Trituration[] {
    const filterValue = value.toLowerCase();
    return this.triturationbase.filter(option => option.client.nom.toLowerCase().indexOf(filterValue) === 0);
  }
  displayFn(trituration) {
    this.selectedTrituration = trituration;
    return  this.selectedTrituration.client.nom ;
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  onSubmit(form) {
    this.submitted = true;
    if (this.createAchatForm.invalid) {
      return;
  }
    this.formSubmitAchat = form;
    this.formSubmitAchatNew = {
      qteAchete: this.qteAchatLocal,
      triturationId: this.idTriturationSelected,
      prix_unitaire: this.prixunitaireLocal,
      montantAchat: this.montantAchat,
      note: this.formSubmitAchat.note,
    };
    this.achatService.add(this.formSubmitAchatNew).subscribe(
      data => {
               this.openSnackBar('Achat a été ajouté avec succès' , 'Ok');
               this.ngOnInit();
               this.dialogRef.close(); } ,
      error => {this.openSnackBar(error , 'Ok'); console.log(error);
    });
    this.dialogRef.close();

  }
  public hasError = (controlName: string, errorName: string) => {
    return this.createAchatForm.controls[controlName].hasError(errorName);
  }
  onSearchChangeb(event: number): void {
    this.prixunitaireLocal = event;
    this.qteAchatLocal = this.createAchatForm.get('qteAchete').value;
    this.montantAchat = (this.prixunitaireLocal * this.qteAchatLocal);
    this.createAchatForm.get('montantAchat').setValue(Number(this.montantAchat).toFixed(3));
  }
  onSearchChangea(event: number): void {
    this.qteAchatLocal = event;
    this.montantAchat = (this.prixunitaireLocal * this.qteAchatLocal);
    this.prixunitaireLocal = this.createAchatForm.get('prix').value;
    this.createAchatForm.get('montantAchat').setValue(Number(this.montantAchat).toFixed(3));
  }
  selected(event) {
    this.idTriturationSelected = event.source.selectedTrituration.id;
    this.QteRestantEnStock = event.source.selectedTrituration.huileRestante;
    this.prixunitaireLocal = this.createAchatForm.get('prix').value;
    this.qteAchatLocal = event.source.selectedTrituration.huileRestante;
    this.montantAchat = (this.prixunitaireLocal * this.qteAchatLocal );
    this.createAchatForm.get('qteAchete').setValue(this.qteAchatLocal);
    this.createAchatForm.get('montantAchat').setValue(Number(this.montantAchat).toFixed(3));
  }

}
