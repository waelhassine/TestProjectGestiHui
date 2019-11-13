import { Component, OnInit , ViewChild, AfterViewInit} from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { ClientServiceService } from '../../../../Service/client-service.service';
import {AddClientComponent } from '../../AddClient/add-client/add-client.component';
import { EditClientComponent } from '../../EditClient/edit-client/edit-client.component';
@Component({
  selector: 'app-all-clients',
  templateUrl: './all-clients.component.html',
  styleUrls: ['./all-clients.component.scss']
})
export class AllClientsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['id', 'nom', 'tel', 'gsm', 'ville', 'action'];
  dataSource: any;
  isLoadingResults = true;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(private dialog: MatDialog , private clientservice: ClientServiceService
            , private router: Router , private snackBar: MatSnackBar) {
             }

  ngOnInit() {
    this.GetAllClient();

  }
  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

  ngAfterViewInit(): void {

    // this.dataSource.sort = this.sort;
    // this.dataSource.paginator = this.paginator;
  }

  OnCreate() {
    const dialogRef = this.dialog.open(AddClientComponent, {
      width: '40%',
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      },
      error => {
        this.openSnackBar('Client existe en Systéme' , 'Ok');
      });
  }
  OnEdit(ida: any) {
    const dialogRef = this.dialog.open(EditClientComponent, {
      width: '40%',
      data : { id: ida }
    });
    dialogRef.afterClosed().subscribe(result => {
     this.ngOnInit();
      });
  }
  OnDelete(id: any) {
    this.clientservice.remove(id).subscribe(
      data => {
           this.openSnackBar('Contact a été Modifier avec succès' , 'Ok');
           this.ngOnInit();
    },
    error => {
      this.openSnackBar(error , 'Ok');
    });
  }

  GetAllClient() {
    this.isLoadingResults = true;
    this.clientservice.getAll().subscribe(
      res => {
        this.dataSource = new MatTableDataSource();
        this.dataSource.data = res;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoadingResults = false;
      },
      error => { this.isLoadingResults = true;
                 console.log('There was an error while retrieving Posts !!!' + error);
      });
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

}
