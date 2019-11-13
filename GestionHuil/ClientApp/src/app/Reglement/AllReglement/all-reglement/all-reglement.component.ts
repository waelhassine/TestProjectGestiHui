import { Component, OnInit , ViewChild} from '@angular/core';
import { MatDialog , MatDialogRef , MAT_DIALOG_DATA} from '@angular/material';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import {MatSnackBar} from '@angular/material/snack-bar';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import {  ActivatedRoute } from '@angular/router';
import { ReglementService } from '../../../../Service/reglement.service';
import { Inject } from '@angular/core';
@Component({
  selector: 'app-all-reglement',
  templateUrl: './all-reglement.component.html',
  styleUrls: ['./all-reglement.component.scss']
})
export class AllReglementComponent implements OnInit {
  displayedColumns: string[] = ['id', 'date', 'modeReglement', 'montant'];
  dataSource: any;
  facturebaseId: any;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any ,
              private dialog: MatDialog ,
              private reglementService: ReglementService ,
              private snackBar: MatSnackBar , private formBuilder: FormBuilder,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      // tslint:disable-next-line:no-string-literal
      this.facturebaseId = this.data.id;
    });
    console.log(this.facturebaseId);
    this.GetAllReglement(this.facturebaseId);
  }
  GetAllReglement(id) {
    this.reglementService.getByFactureId(id).subscribe(
      res => {
        console.log(res);
        this.dataSource = new MatTableDataSource();
        this.dataSource.data = res;
        console.log(this.dataSource.data);
      },
      error => { console.log('There was an error while retrieving Posts !!!' + error);
      });
  }

}
