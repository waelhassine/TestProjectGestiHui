import { Component, OnInit , ViewChild} from '@angular/core';
import { MatDialog} from '@angular/material';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { StockageOlivesService } from '../../../../Service/stockage-olives.service';
import { AddStockageOlivesComponent } from '../../AddStockageOlives/add-stockage-olives/add-stockage-olives.component';
import { EditStockageOlivesComponent} from '../../EditStockageOlives/edit-stockage-olives/edit-stockage-olives.component';
@Component({
  selector: 'app-all-stockage-olives',
  templateUrl: './all-stockage-olives.component.html',
  styleUrls: ['./all-stockage-olives.component.scss']
})
export class AllStockageOlivesComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date', 'client.nom', 'poids', 'variete.name',
  'vehicule', 'chauffeur', 'action'];
  isShow =  true;
  messsage: any;
  isLoadingResults = true;
  dataSource: any;
  formfilter: FormGroup;
  pipe: DatePipe;
  myFormattedDate: any;
  myFormattedend: any;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  constructor(private dialog: MatDialog , private stockageOlivesService: StockageOlivesService ,
              private snackBar: MatSnackBar , private formBuilder: FormBuilder) {
      this.formfilter = this.formBuilder.group({
        Fromdo: new Date(2018, 7, 5) ,
        Fromto: new Date(2019, 7, 25)
        });
    }
  ngOnInit() {
    this.GetAllStockage();
  }

  OnCreate() {
    const dialogRef = this.dialog.open(AddStockageOlivesComponent, {
      width: '45%'
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      });
  }
  OnEdit(ida: any) {
    this.stockageOlivesService.checkStockageExitsTrituration(ida).subscribe(data => {
      this.messsage = data;
      if (this.messsage.message === 'Ok') {

    const dialogRef = this.dialog.open(EditStockageOlivesComponent, {
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
    this.stockageOlivesService.checkStockageExitsTrituration(id).subscribe(data => {
      this.messsage = data;
      if (this.messsage.message === 'Ok') {
      this.stockageOlivesService.remove(id).subscribe(
      dataa => {
           this.openSnackBar('Stockage Olives a été Supprimer avec succès' , 'Ok');
           this.ngOnInit();
    },
    error => {
      console.log(error);
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
    this.pipe.transform(val.begin, 'dd/MM/yyyy');
    this.pipe.transform(val.end, 'dd/MM/yyyy');
    this.formfilter.setValue({
      Fromdo: this.pipe.transform(val.begin, 'MM/dd/yyyy') ,
    Fromto: this.pipe.transform(val.end, 'MM/dd/yyyy')
    });
    const serializedForm = JSON.stringify(this.formfilter.value);
    this.stockageOlivesService.filter(serializedForm).subscribe( data => {
      this.dataSource.data = data;
      console.log(this.dataSource.data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
     } ,
     error => { console.log(error); });
  }

public doFilter = (value: string) => {
    value = value.trim(); // Remove whitespace
    value = value.toLowerCase();
    this.dataSource.filter = value;
}
// tslint:disable-next-line:use-lifecycle-interface
ngAfterViewInit(): void {

  // this.dataSource.sort = this.sort;
  // this.dataSource.paginator = this.paginator;
}
OnCloseFilter() {
this.isShow = !this.isShow;
this.GetAllStockage();
}
openSnackBar(message: string, action: string) {
  this.snackBar.open(message, action, {
    duration: 2000,
  });
}
GetAllStockage() {
  this.isLoadingResults = true;
  this.stockageOlivesService.getAll().subscribe(
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
OnExportPdf(id) {

  this.stockageOlivesService.pdfDown(id).subscribe(x => {
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
