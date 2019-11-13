import { Component, OnInit , ViewChild, AfterViewInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import {  ActivatedRoute } from '@angular/router';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { GrignonsService } from '../../../../Service/grignons.service';
import { AddGrignonsComponent } from '../../AddGrignons/add-grignons/add-grignons.component';
import { EditGrignonsComponent } from '../../EditGrignons/edit-grignons/edit-grignons.component';
import { AddFactureComponent } from '../../../Clients/ClientDetails/FactureTrituration/AjouterFacture/add-facture/add-facture.component';
@Component({
  selector: 'app-all-grignons',
  templateUrl: './all-grignons.component.html',
  styleUrls: ['./all-grignons.component.scss']
})
export class AllGrignonsComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date' , 'client.nom', 'poids', 'prix_unitaire', 'montantAchat', 'vehicule', 'chaufeur' , 'action'];
  dataSource: any;
  isShow =  true;
  isLoadingResults = true;
  formfilter: FormGroup;
  pipe: DatePipe;
  myFormattedDate: any;
  myFormattedend: any;
  messsage: any;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  constructor(private dialog: MatDialog ,
              private grignonsService: GrignonsService ,
              private snackBar: MatSnackBar , private formBuilder: FormBuilder,
              private route: ActivatedRoute) {this.formfilter = this.formBuilder.group({
                Fromdo: new Date(2018, 7, 5) ,
                Fromto: new Date(2019, 7, 25)
                });  }

  ngOnInit() {
    this.GetAllGrignons();
  }
  GetAllGrignons() {
    this.isLoadingResults = true;
    this.grignonsService.getAll().subscribe(
      res => {
        this.dataSource = new MatTableDataSource();
        this.dataSource.data = res;
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.isLoadingResults = false;
      },
      error => {this.isLoadingResults = true; console.log('There was an error while retrieving Posts !!!' + error);
      });
  }
  AfterViewInit(): void {

    // this.dataSource.sort = this.sort;
    // this.dataSource.paginator = this.paginator;
  }
  OnCreate() {
    const dialogRef = this.dialog.open(AddGrignonsComponent, {
      width: '45%'
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      });
  }
  OnEdit(ida: any) {
    this.grignonsService.checkGrignonsExitsFacture(ida).subscribe(data => {
      this.messsage = data;
      if (this.messsage.message === 'Ok') {
      const dialogRef = this.dialog.open(EditGrignonsComponent, {
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
    this.grignonsService.checkGrignonsExitsFacture(id).subscribe(data => {
      this.messsage = data;
      if (this.messsage.message === 'Ok') {
      this.grignonsService.remove(id).subscribe(
        dataa => {
             this.openSnackBar('Vente Huile a été Supprimer avec succès' , 'Ok');
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
    this.grignonsService.filter(serializedForm).subscribe( data => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
     } ,
     error => { console.log(error); });
  }
  OnCreateFacture(element) {
    this.grignonsService.checkGrignonsExitsFacture(element.id).subscribe(data => {
      this.messsage = data;
      if (this.messsage.message === 'Ok') {
      const dialogRef = this.dialog.open(AddFactureComponent, {
        data : {element , factureTypeId : 4}
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
  OnCloseFilter() {
  this.isShow = !this.isShow;
  this.ngOnInit();
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
