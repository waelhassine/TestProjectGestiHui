import { Component, OnInit , ViewChild } from '@angular/core';
import { MatDialog} from '@angular/material';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import {  ActivatedRoute } from '@angular/router';
import { DiversTransactionService } from '../../../../../../Service/divers-transaction.service';
import {AddDiversTransactionComponent } from '../../AddDiversTransaction/add-divers-transaction/add-divers-transaction.component';
import {  EditDiversTransactionComponent} from '../../EditDiversTransaction/edit-divers-transaction/edit-divers-transaction.component';
@Component({
  selector: 'app-all-divers-transaction',
  templateUrl: './all-divers-transaction.component.html',
  styleUrls: ['./all-divers-transaction.component.scss']
})
export class AllDiversTransactionComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date', 'typeTransaction', 'montant', 'typeDePaiement', 'action'];
  isShow =  true;
  dataSource: any;
  formfilter: FormGroup;
  pipe: DatePipe;
  myFormattedDate: any;
  myFormattedend: any;
  clientBaseId: any;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  constructor(private dialog: MatDialog ,
              private diversTransactionService: DiversTransactionService ,
              private snackBar: MatSnackBar , private formBuilder: FormBuilder,
              private route: ActivatedRoute) {
      this.formfilter = this.formBuilder.group({
        Fromdo: new Date(2018, 7, 5) ,
        Fromto: new Date(2019, 7, 25)
        }); }

  ngOnInit() {
    this.route.params.subscribe(params => {
      // tslint:disable-next-line:no-string-literal
      this.clientBaseId = params['id'];
    });
    this.GetAllTransaction(this.clientBaseId);
  }
  GetAllTransaction(id) {
    this.diversTransactionService.getByIdClient(id).subscribe(
      res => {
        this.dataSource = new MatTableDataSource();
        this.dataSource.data = res;
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error => { console.log('There was an error while retrieving Posts !!!' + error);
      });
  }
  AfterViewInit(): void {

    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
  OnCreate() {
    const dialogRef = this.dialog.open(AddDiversTransactionComponent, {
      width: '40%',
      data : { id: this.clientBaseId }
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      });

  }
  OnEdit(ida: any) {
    const dialogRef = this.dialog.open(EditDiversTransactionComponent, {
      width: '40%',
      data : { id: ida }
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      });
  }
  OnDelete(id: any) {

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
    this.diversTransactionService.filter(serializedForm).subscribe( data => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
     } ,
     error => { console.log(error); });
  }
  OnCloseFilter() {
  this.isShow = !this.isShow;
  this.GetAllTransaction(this.clientBaseId);
  }
  OnExportPdf(id) {
    this.diversTransactionService.pdfDown(id).subscribe(x => {
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
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
  public doFilter = (value: string) => {
    value = value.trim(); // Remove whitespace
    value = value.toLowerCase();
    this.dataSource.filter = value;
}

}
