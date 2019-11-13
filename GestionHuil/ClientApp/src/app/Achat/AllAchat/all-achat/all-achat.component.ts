import { Component, OnInit , ViewChild} from '@angular/core';
import { MatDialog} from '@angular/material';
import { Router } from '@angular/router';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { AchatService } from '../../../../Service/achat.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { AddAchatComponent} from '../../AddChat/add-achat/add-achat.component';
import { EditAchatComponent } from '../../EditAchat/edit-achat/edit-achat.component';
import {AddFactureComponent} from '../../../Clients/ClientDetails/FactureTrituration/AjouterFacture/add-facture/add-facture.component';
@Component({
  selector: 'app-all-achat',
  templateUrl: './all-achat.component.html',
  styleUrls: ['./all-achat.component.scss']
})
export class AllAchatComponent implements OnInit {
  displayedColumns: string[] = ['id', 'trituration.date', 'trituration.client.nom', 'qteAchete',
   'trituration.variete.name' , 'prix_unitaire', 'montantAchat', 'note', 'action'];
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
  constructor(private dialog: MatDialog , private achatService: AchatService
            , private router: Router , private snackBar: MatSnackBar , private formBuilder: FormBuilder) {
              this.formfilter = this.formBuilder.group({
                Fromdo: new Date(2018, 7, 5) ,
                Fromto: new Date(2019, 7, 25)
                });
             }

  ngOnInit() {
    this.GetAllData();
  }
  OnCreate() {
    const dialogRef = this.dialog.open(AddAchatComponent, {
      width: '45%', height: '90%'
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      });
  }
  OnEdit(ida: any) {
    this.achatService.checkAchatExitsFacture(ida).subscribe(data => {
      this.messsage = data;
      if (this.messsage.message === 'Ok') {
        const dialogRef = this.dialog.open(EditAchatComponent, {
          width: '45%', height: '90%',
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
    this.achatService.checkAchatExitsFacture(id).subscribe(data => {
      this.messsage = data;
      if (this.messsage.message === 'Ok') {
      this.achatService.remove(id).subscribe(
        dataa => {
             this.openSnackBar('Achat a été Supprimer avec succès' , 'Ok');
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
    this.achatService.filter(serializedForm).subscribe( data => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
     } ,
     error => { console.log(error); });
  }
  // tslint:disable-next-line:use-lifecycle-interface
  ngAfterViewInit(): void {
    // this.dataSource.sort = this.sort;
    // this.dataSource.paginator = this.paginator;
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
openSnackBar(message: string, action: string) {
  this.snackBar.open(message, action, {
    duration: 2000,
  });
}
OnExportPdf(id) {

  this.achatService.pdfDown(id).subscribe(x => {
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
  GetAllData() {
    this.isLoadingResults = true;
    this.achatService.getAll().subscribe(
      res => {
        this.dataSource = new MatTableDataSource();
        this.dataSource.data = res;
        this.dataSource.filterPredicate = (data: any, filterValue: string) => {
          return data.trituration.client.nom.trim().toLocaleLowerCase().indexOf(filterValue.trim().toLocaleLowerCase()) >= 0;
         };
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.isLoadingResults = false;
      },
      error => {this.isLoadingResults = true;
                console.log('There was an error while retrieving Posts !!!' + error);
      });
  }
  OnCreateFacture(element) {
    this.achatService.checkAchatExitsFacture(element.id).subscribe(data => {
      this.messsage = data;
      if (this.messsage.message === 'Ok') {
      const dialogRef = this.dialog.open(AddFactureComponent, {
        data : {element , factureTypeId : 2}
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

}
