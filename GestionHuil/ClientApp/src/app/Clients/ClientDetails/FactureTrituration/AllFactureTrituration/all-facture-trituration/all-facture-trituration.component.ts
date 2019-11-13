import { Component, OnInit , ViewChild } from '@angular/core';
import { MatDialog} from '@angular/material';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import {  ActivatedRoute } from '@angular/router';
import { FactureService } from '../../../../../../Service/facture.service';
// tslint:disable-next-line:import-spacing
import {  AjouterReglementFactureComponent }
from '../../../../../Reglement/AjouterReglement/ajouter-reglement-facture/ajouter-reglement-facture.component';
import {AllReglementComponent } from '../../../../../Reglement/AllReglement/all-reglement/all-reglement.component';
export interface FactureTri {
  id: number;
  date: string;
  resteApayer: number;
  montant: number;
}
@Component({
  selector: 'app-all-facture-trituration',
  templateUrl: './all-facture-trituration.component.html',
  styleUrls: ['./all-facture-trituration.component.scss']
})
export class AllFactureTriturationComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date', 'typeFacture.nom', 'montant', 'restApayer', 'action'];
  isShow =  true;
  dataSource: any;
  // tslint:disable-next-line:ban-types
  basea: any;
  total: number;
  reste: any;
  payer: any;
  formfilter: FormGroup;
  pipe: DatePipe;
  myFormattedDate: any;
  myFormattedend: any;
  clientBaseId: any;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  constructor(private dialog: MatDialog ,
              private factureService: FactureService ,
              private snackBar: MatSnackBar , private formBuilder: FormBuilder,
              private route: ActivatedRoute) {
                this.formfilter = this.formBuilder.group({
                  Fromdo: new Date(2018, 7, 5) ,
                  Fromto: new Date(2019, 7, 25)
                  });
              }

  ngOnInit() {
    this.route.params.subscribe(params => {
      // tslint:disable-next-line:no-string-literal
      this.clientBaseId = params['id'];
    });
    this.GetAllTrituration(this.clientBaseId);
  }
  GetAllTrituration(id) {
    this.factureService.geTriturationtByIdClient(id).subscribe(
      data => {
        this.basea = data;
        this.total = 0; this.reste = 0; this.payer = 0;
        this.dataSource = new MatTableDataSource();
        this.dataSource.data = data;
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.basea.forEach(element => {
          if (element.typeFactureId === 1 || element.typeFactureId === 3 || element.typeFactureId === 4) {
            this.total =  this.total + Number(element.montant) ;
            this.reste = this.reste + Number(element.restApayer);
            this.payer = this.total - this.reste;
          }
        });

      },
      error => { console.log('There was an error while retrieving Posts !!!' + error);
      });

  }
  AfterViewInit(): void {

    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
  OnCreate() {

  }
  OnSeeReglement(ida) {
    const dialogRef = this.dialog.open(AllReglementComponent, {
      width: '60%',
      height: '50%',
      data : { id: ida }
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      });
  }
  OnCreateReglement(element) {
    if (element.restApayer >= 0) {
      const dialogRef = this.dialog.open(AjouterReglementFactureComponent, {
        width: '40%',
        data : { id: element.id }
      });
      dialogRef.afterClosed().subscribe(result => {
        this.ngOnInit();
        });
    } else {
      this.openSnackBar('Cette Action Payée' , 'Ok');
    }

  }
  OnEdit(ida: any) {
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
    this.factureService.filter(serializedForm).subscribe( data => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
     } ,
     error => { console.log(error); });
  }
  OnCloseFilter() {
  this.isShow = !this.isShow;
  this.GetAllTrituration(this.clientBaseId);
  }
  OnExportPdf(id) {
    this.factureService.pdfDown(id).subscribe(x => {
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
