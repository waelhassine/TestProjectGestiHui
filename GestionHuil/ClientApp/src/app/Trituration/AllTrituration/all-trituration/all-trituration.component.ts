// import { AjouterReglementFactureComponent } from './../../../ajouter-reglement-facture/ajouter-reglement-facture.component';
import { Component, OnInit , ViewChild } from '@angular/core';
import { MatDialog} from '@angular/material';
import { Router } from '@angular/router';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { TriturationService } from '../../../../Service/trituration.service';
import { AddTriturationComponent } from '../../AddTrituration/add-trituration/add-trituration.component';
import { EditTriturationComponent } from '../../EditTrituration/edit-trituration/edit-trituration.component';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import {AddFactureComponent} from '../../../Clients/ClientDetails/FactureTrituration/AjouterFacture/add-facture/add-facture.component';
@Component({
  selector: 'app-all-trituration',
  templateUrl: './all-trituration.component.html',
  styleUrls: ['./all-trituration.component.scss']
})

export class AllTriturationComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date', 'client.nom',
   'poids', 'variete.name' , 'rendement', 'prixUnitaire', 'montant'
  , 'huileObtenu', 'qteLivree', 'huileRestante', 'action'];
  isShow =  true;
  isLoadingResults = true;
  messsage: any;
  dataSource: any;
  formfilter: FormGroup;
  pipe: DatePipe;
  myFormattedDate: any;
  myFormattedend: any;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  constructor(private dialog: MatDialog , private triturationService: TriturationService
            , private router: Router , private snackBar: MatSnackBar , private formBuilder: FormBuilder) {
              this.formfilter = this.formBuilder.group({
                Fromdo: new Date(2018, 7, 5) ,
                Fromto: new Date(2019, 7, 25)
                });
            }

ngOnInit() {
    this.GetAllTrituration();
          }

 public doFilter = (value: string) => {
            value = value.trim(); // Remove whitespace
            value = value.toLowerCase();
            this.dataSource.filter = value;
}
OnCloseFilter() {
  this.isShow = !this.isShow;
  this.ngOnInit();
}
// tslint:disable-next-line:use-lifecycle-interface
ngAfterViewInit(): void {

  // this.dataSource.sort = this.sort;
  // this.dataSource.paginator = this.paginator;
}
OnCreate() {
  const dialogRef = this.dialog.open(AddTriturationComponent, {
    width: '45%',
    height: '95%'
  });
  dialogRef.afterClosed().subscribe(result => {
    this.ngOnInit();
    });
}
OnEdit(ida: any) {

  this.triturationService.checkTriturationExitsAchat(ida).subscribe(data => {
    this.messsage = data;
    if (this.messsage.message === 'Ok') {
    const dialogRef = this.dialog.open(EditTriturationComponent, {
      width: '45%',
      data : { id: ida }
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      });
  } else {
    this.openSnackBar(this.messsage.message , 'Ok');
  }
    }, error => {

    });
}
OnDelete(id: any) {
  this.triturationService.checkTriturationExitsAchat(id).subscribe(data => {
    this.messsage = data;
    if (this.messsage.message === 'Ok') {
    this.triturationService.remove(id).subscribe(
      dataa => {
           this.openSnackBar('Trituration a été Supprimer avec succès' , 'Ok');
           this.ngOnInit();
    },
    error => {
      this.openSnackBar(error , 'Ok');
    });
  } else {
    this.openSnackBar(this.messsage.message , 'Ok');
  }
    }, error => {

    });
}
addEvent(event) {
  this.isShow = false;
  const val = event.value;
  this.pipe = new DatePipe('en');
  this.pipe.transform(val.begin, 'MM/dd/yyyy');
  this.pipe.transform(val.end, 'MM/dd/yyyy');
  this.formfilter.setValue({
    Fromdo: this.pipe.transform(val.begin, 'MM/dd/yyyy') ,
  Fromto: this.pipe.transform(val.end, 'MM/dd/yyyy')
  });
  const serializedForm = JSON.stringify(this.formfilter.value);
  this.triturationService.filter(serializedForm).subscribe( data => {
    this.dataSource.data = data;
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
   } ,
   error => { });
}

GetAllTrituration() {
  this.isLoadingResults = true;
  this.triturationService.getAll().subscribe(
    res => {
      this.dataSource = new MatTableDataSource();
      this.dataSource.data = res;
      this.dataSource.filterPredicate = (data: any, filterValue: string) => {
        return data.client.nom.trim().toLocaleLowerCase().indexOf(filterValue.trim().toLocaleLowerCase()) >= 0;
       };
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      this.isLoadingResults = false;
    },
    error => {this.isLoadingResults = true;
              console.log('There was an error while retrieving Posts !!!' + error);
    });
}
openSnackBar(message: string, action: string) {
  this.snackBar.open(message, action, {
    duration: 2000,
  });
}
OnCreateFacture(element) {
  this.triturationService.checkTriturationExitsFacture(element.id).subscribe(data => {
  this.messsage = data;
  if (this.messsage.message === 'Ok') {
  const dialogRef = this.dialog.open(AddFactureComponent, {
    data : {element , factureTypeId : 1}
  });
  dialogRef.afterClosed().subscribe(result => {
    this.ngOnInit();
    });
} else {
  this.openSnackBar(this.messsage.message , 'Ok');
}
  }, error => {

  });

}
OnExportPdf(id) {

  this.triturationService.pdfDown(id).subscribe(x => {
    // tslint:disable-next-line:prefer-const
    let  newBlob = new Blob([x], { type: 'application/pdf' });
    if (window.navigator && window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveOrOpenBlob(newBlob);
        return;
    }
    const data = window.URL.createObjectURL(newBlob);

    window.open(data, '_blank');
});
}
}
