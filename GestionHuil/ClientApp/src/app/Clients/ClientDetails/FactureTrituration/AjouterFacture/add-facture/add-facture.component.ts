import { Component, OnInit, Inject  } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { AppDateAdapter, APP_DATE_FORMATS} from '../../../../../../format-datepicker';
import { DatePipe } from '@angular/common';
import {DateAdapter, MAT_DATE_FORMATS} from '@angular/material/core';
import { FactureService } from '../../../../../../Service/facture.service';
@Component({
  selector: 'app-add-facture',
  templateUrl: './add-facture.component.html',
  styleUrls: ['./add-facture.component.scss'],
  providers: [
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS},
    DatePipe
    ]
})
export class AddFactureComponent implements OnInit {
  ajouterFacturationFormNew: any;
  isLoading = false;
  submitted = false;
  // tslint:disable-next-line:no-inferrable-types
  tria: string = 'tri';
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              public dialogRef: MatDialogRef<AddFactureComponent> ,
              private factureService: FactureService ,
              private snackBar: MatSnackBar, private datePipe: DatePipe) { }

  ngOnInit() {
  }
  fermerDialog() {
    this.dialogRef.close();
  }
  OnCreateFacture() {
    if (this.data.factureTypeId === 1 ) {
      this.ajouterFacturationFormNew = {
        date: this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss') ,
        montant: this.data.element.montant,
        typeFactureId: this.data.factureTypeId,
        triturationId: this.data.element.id,
        clientId: this.data.element.clientId,
    };
      this.factureService.add(this.ajouterFacturationFormNew).subscribe(data => {
        this.openSnackBar('Facture Trituration a été ajouté avec succès' , 'Ok');
      } , error => {
        this.openSnackBar(error , 'Ok'); console.log(error);
      });
    } else if (this.data.factureTypeId === 2) {
      this.ajouterFacturationFormNew = {
        date: this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss') ,
        montant: this.data.element.montantAchat ,
        achatId: this.data.element.id,
        typeFactureId: this.data.factureTypeId,
        clientId: this.data.element.trituration.clientId,
    };
      this.factureService.add(this.ajouterFacturationFormNew).subscribe(data => {
        this.openSnackBar('Facture Achat a été ajouté avec succès' , 'Ok');
      } , error => {
        this.openSnackBar(error , 'Ok'); console.log(error);
      });
    } else if (this.data.factureTypeId === 3) {
      this.ajouterFacturationFormNew = {
        date: this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss') ,
        montant: this.data.element.montantVente,
        venteHuileId: this.data.element.id,
        typeFactureId: this.data.factureTypeId,
        clientId: this.data.element.clientId,
    };
      this.factureService.add(this.ajouterFacturationFormNew).subscribe(data => {
        this.openSnackBar('Facture Vente a été ajouté avec succès' , 'Ok');
      } , error => {
        this.openSnackBar(error , 'Ok'); console.log(error);
      });
    } else if (this.data.factureTypeId === 4) {
      this.ajouterFacturationFormNew = {
        date: this.datePipe.transform(Date.now(), 'MM/dd/yyyy HH:mm:ss') ,
        montant: this.data.element.montantAchat,
        grignonId: this.data.element.id,
        typeFactureId: this.data.factureTypeId,
        clientId: this.data.element.clientId,
    };
      this.factureService.add(this.ajouterFacturationFormNew).subscribe(data => {
        this.openSnackBar('Facture Grignons été ajouté avec succès' , 'Ok');
      } , error => {
        this.openSnackBar(error , 'Ok'); console.log(error);
      });
    }
    this.dialogRef.close();
    }
    openSnackBar(message: string, action: string) {
      this.snackBar.open(message, action, {
        duration: 2000,
      });
    }


}
