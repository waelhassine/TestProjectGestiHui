import { Component, OnInit , ViewChild , AfterViewInit} from '@angular/core';
import { MatDialog} from '@angular/material';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { VenteHuileService } from '../../../../Service/vente-huile.service';
import { AddVenteHuileComponent } from '../../AddVente/add-vente-huile/add-vente-huile.component';
import { EditVenteHuileComponent } from '../../EditVente/edit-vente-huile/edit-vente-huile.component';
import {AddFactureComponent} from '../../../Clients/ClientDetails/FactureTrituration/AjouterFacture/add-facture/add-facture.component';
import { StockageHuileService } from '../../../../Service/stockage-huile.service';
@Component({
  selector: 'app-all-vente-huile',
  templateUrl: './all-vente-huile.component.html',
  styleUrls: ['./all-vente-huile.component.scss']
})
export class AllVenteHuileComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date', 'client.nom', 'variete.name', 'qte_Vente',
   'prix_Unitaire', 'montantBidon', 'montantVente', 'action'];
  isShow =  true;
  isLoadingResults = true;
  messsage: any;
  dataSource: any;
  formfilter: FormGroup;
  pipe: DatePipe;
  myFormattedDate: any;
  myFormattedend: any;
  datasourceStatistique: any = 0;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  constructor(private stockageHuileService: StockageHuileService ,
              private dialog: MatDialog ,
              private venteHuileService: VenteHuileService ,
              private snackBar: MatSnackBar , private formBuilder: FormBuilder) {
                this.formfilter = this.formBuilder.group({
                  Fromdo: new Date(2018, 7, 5) ,
                  Fromto: new Date(2019, 7, 25)
                  });
               }

  ngOnInit() {
    this.GetAllVenteHuile();
    this.GetStat();
    }
  public doFilter = (value: string) => {
    value = value.trim(); // Remove whitespace
    value = value.toLowerCase();
    this.dataSource.filter = value;
}

AfterViewInit(): void {

  // this.dataSource.sort = this.sort;
  // this.dataSource.paginator = this.paginator;
}
OnCreate() {
  const dialogRef = this.dialog.open(AddVenteHuileComponent, {
    width: '45%',
    height: '95%'
  });
  dialogRef.afterClosed().subscribe(result => {
    this.ngOnInit();
    });
}
OnEdit(ida: any) {
  this.venteHuileService.checkVenteExitsFacture(ida).subscribe(data => {
    this.messsage = data;
    if (this.messsage.message === 'Ok') {
    const dialogRef = this.dialog.open(EditVenteHuileComponent, {
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
  this.venteHuileService.checkVenteExitsFacture(id).subscribe(data => {
    this.messsage = data;
    if (this.messsage.message === 'Ok') {
    this.venteHuileService.remove(id).subscribe(
      dataa => {
           this.openSnackBar('Vente Huile a été Supprimer avec succès' , 'Ok');
           this.ngOnInit();
    },
    error => {
      this.openSnackBar(error , 'Ok');
    });
  } else {
    this.openSnackBar(this.messsage.message , 'Ok');  }
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
  this.venteHuileService.filter(serializedForm).subscribe( data => {
    this.dataSource.data = data;
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
   } ,
   error => { console.log(error); });
}
OnCloseFilter() {
this.isShow = !this.isShow;
this.GetAllVenteHuile();
}
GetAllVenteHuile() {
  this.isLoadingResults = true;
  this.venteHuileService.getAll().subscribe(
    res => {
      this.dataSource = new MatTableDataSource();
      this.dataSource.data = res;
      this.dataSource.filterPredicate = (data: any, filterValue: string) => {
        return data.client.nom.trim().toLocaleLowerCase().indexOf(filterValue.trim().toLocaleLowerCase()) >= 0;
        this.isLoadingResults = false;
       };
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    },
    error => { console.log('There was an error while retrieving Posts !!!' + error);
    });

}
GetStat() {
  this.isLoadingResults = true;
  this.stockageHuileService.getAll().subscribe(data => {
    this.datasourceStatistique = data;
    this.isLoadingResults = false;
    } , error => { this.isLoadingResults = true;
                   console.log('ERROR !!!!!!');
    });
}
OnCreateFacture(element) {
  this.venteHuileService.checkVenteExitsFacture(element.id).subscribe(data => {
    this.messsage = data;
    if (this.messsage.message === 'Ok') {
    const dialogRef = this.dialog.open(AddFactureComponent, {
      data : {element , factureTypeId : 3}
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
  this.venteHuileService.pdfDown(id).subscribe(x => {
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

}
