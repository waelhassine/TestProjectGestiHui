import { Component, OnInit , ViewChild, AfterViewInit} from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import {MatSnackBar} from '@angular/material/snack-bar';
import { CaissesService } from '../../../../Service/caisses.service';
import { AddCaisseComponent } from '../../AddCaisse/add-caisse/add-caisse.component';
import { EditCaisseComponent } from '../../EditCaisse/edit-caisse/edit-caisse.component';
@Component({
  selector: 'app-all-caisse',
  templateUrl: './all-caisse.component.html',
  styleUrls: ['./all-caisse.component.scss']
})
export class AllCaisseComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date', 'montant', 'personne', 'action'];
  dataSource: any;
  isShow =  true;
  formfilter: FormGroup;
  pipe: DatePipe;
  myFormattedDate: any;
  myFormattedend: any;
  isLoadingResults = true;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(private dialog: MatDialog , private caissesService: CaissesService
    ,         private router: Router , private snackBar: MatSnackBar
     ,        private formBuilder: FormBuilder) {this.formfilter = this.formBuilder.group({
      Fromdo: new Date(2018, 7, 5) ,
      Fromto: new Date(2019, 7, 25)
      });   }


  ngOnInit() {
    this.GetAllCaisse();
  }
  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

  // tslint:disable-next-line:use-lifecycle-interface
  ngAfterViewInit(): void {

    // this.dataSource.sort = this.sort;
    // this.dataSource.paginator = this.paginator;
  }

  OnCreate() {
    const dialogRef = this.dialog.open(AddCaisseComponent, {
      width: '40%',
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      });
  }
  OnEdit(ida: any) {
    const dialogRef = this.dialog.open(EditCaisseComponent, {
      width: '40%',
      data : { id: ida }
    });
    dialogRef.afterClosed().subscribe(result => {
     this.ngOnInit();
      });
  }
  OnDelete(id: any) {
    this.caissesService.remove(id).subscribe(
      data => {
           this.openSnackBar('Contact a été Modifier avec succès' , 'Ok');
           this.ngOnInit();
    },
    error => {
      this.openSnackBar(error , 'Ok');
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
    this.caissesService.filter(serializedForm).subscribe( data => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
     } ,
     error => { console.log(error); });
  }
  GetAllCaisse() {
    this.isLoadingResults = true;
    this.caissesService.getAll().subscribe(
      res => {

        this.dataSource = new MatTableDataSource();
        this.dataSource.data = res;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoadingResults = false;
      },
      error => { this.isLoadingResults = false;
                 console.log('There was an error while retrieving Posts !!!' + error);
      });
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
  OnCloseFilter() {
    this.isShow = !this.isShow;
    this.GetAllCaisse();
    }


}
