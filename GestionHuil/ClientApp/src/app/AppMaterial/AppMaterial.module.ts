import { NgModule } from '@angular/core';
import {FlexLayoutModule} from '@angular/flex-layout';
import {MatButtonModule, MatListModule, MatIconModule, MatCardModule, MatMenuModule, MatInputModule, MatButtonToggleModule,
    MatProgressSpinnerModule, MatSelectModule, MatSlideToggleModule, MatDialogModule, MatSnackBarModule, MatToolbarModule,
    MatTabsModule, MatSidenavModule, MatTooltipModule, MatRippleModule, MatRadioModule, MatGridListModule,
  MatDatepickerModule, MatNativeDateModule, MatSliderModule, MatAutocompleteModule ,
 MatTableModule, MatSortModule , MatChipsModule} from '@angular/material';



@NgModule({
  exports: [
    MatButtonModule, MatListModule, MatIconModule, MatCardModule, MatMenuModule, MatInputModule, MatSelectModule,
    MatButtonToggleModule, MatSlideToggleModule, MatProgressSpinnerModule, MatDialogModule, MatSnackBarModule,  MatToolbarModule,
     MatTabsModule,  MatSidenavModule,  MatTooltipModule,  MatRippleModule,  MatRadioModule,  MatGridListModule, MatDatepickerModule,
     MatNativeDateModule,  MatSliderModule, MatAutocompleteModule, MatTableModule, MatSortModule, FlexLayoutModule , MatChipsModule
  ]
})
export class AppMaterialModule {
}
