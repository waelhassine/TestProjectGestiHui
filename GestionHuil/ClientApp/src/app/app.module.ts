import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule , ReactiveFormsModule} from '@angular/forms';
import { MatPaginatorModule, MatDialogRef, MatExpansionModule, MatDateFormats, MAT_DATE_LOCALE, MAT_DATE_FORMATS} from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppMaterialModule} from '../app/AppMaterial/AppMaterial.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeLayoutComponent } from './home-layout/home-layout.component';
import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { LoginComponent } from './login/login.component';
import { NavigationComponent } from './navigation/navigation.component';
import { Globals } from './globals';
import { AuthGuard } from '../Service/auth-guard';
import { EmployeeService } from '../Service/employee.service';
import { ClientServiceService } from '../Service/client-service.service';
import { TriturationService } from '../Service/trituration.service';
import { AchatService } from '../Service/achat.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from '../Service/jwt-interceptor';
import { ErrorInterceptor } from '../Service/error-interceptor';
import { HomeComponent } from './home/home.component';
import { AllClientsComponent } from './Clients/AllClients/all-clients/all-clients.component';
import { AddClientComponent } from './Clients/AddClient/add-client/add-client.component';
import { EditClientComponent } from './Clients/EditClient/edit-client/edit-client.component';
import { AllTriturationComponent } from './Trituration/AllTrituration/all-trituration/all-trituration.component';
import { AddTriturationComponent } from './Trituration/AddTrituration/add-trituration/add-trituration.component';
import { SatDatepickerModule, SatNativeDateModule } from 'saturn-datepicker';
import { EditTriturationComponent } from './Trituration/EditTrituration/edit-trituration/edit-trituration.component';
import { AllAchatComponent } from './Achat/AllAchat/all-achat/all-achat.component';
import { AddAchatComponent } from './Achat/AddChat/add-achat/add-achat.component';
import { EditAchatComponent } from './Achat/EditAchat/edit-achat/edit-achat.component';
import { ClientDetailsComponent } from './Clients/ClientDetails/client-details/client-details.component';
import { AddStockageOlivesComponent } from './StockageOlives/AddStockageOlives/add-stockage-olives/add-stockage-olives.component';
import { AllStockageOlivesComponent} from './StockageOlives/AllStockageOlives/all-stockage-olives/all-stockage-olives.component';
import { EditStockageOlivesComponent } from './StockageOlives/EditStockageOlives/edit-stockage-olives/edit-stockage-olives.component';
import { AllVenteHuileComponent } from './Vente/AllVente/all-vente-huile/all-vente-huile.component';
import { AddVenteHuileComponent } from './Vente/AddVente/add-vente-huile/add-vente-huile.component';
import { EditVenteHuileComponent } from './Vente/EditVente/edit-vente-huile/edit-vente-huile.component';
import { StockHuileComponent } from './Vente/stock-huile/stock-huile.component';
import { StockageHuileService } from '../Service/stockage-huile.service';
import { DiversTransactionService } from '../Service/divers-transaction.service';
import { FactureService } from '../Service/facture.service';
import { ReglementService } from '../Service/reglement.service';
import { GrignonsService } from '../Service/grignons.service';
import { CaissesService } from '../Service/caisses.service';
import { StatistiqueService } from '../Service/statistique.service';
import { ChartsModule } from 'ng2-charts';
// tslint:disable-next-line:import-spacing
import { AddDiversTransactionComponent } from
      './Clients/ClientDetails/DiversTransaction/AddDiversTransaction/add-divers-transaction/add-divers-transaction.component';
// tslint:disable-next-line:import-spacing
import { AllDiversTransactionComponent } from
      './Clients/ClientDetails/DiversTransaction/AllDiversTransaction/all-divers-transaction/all-divers-transaction.component';
// tslint:disable-next-line:import-spacing
import { EditDiversTransactionComponent } from
'./Clients/ClientDetails/DiversTransaction/EditDiversTransaction/edit-divers-transaction/edit-divers-transaction.component';

// tslint:disable-next-line:import-spacing
import { AllFactureTriturationComponent } from
'./Clients/ClientDetails/FactureTrituration/AllFactureTrituration/all-facture-trituration/all-facture-trituration.component';
// tslint:disable-next-line:max-line-length
import { AjouterReglementFactureComponent } from './Reglement/AjouterReglement/ajouter-reglement-facture/ajouter-reglement-facture.component';
import { AllReglementComponent } from './Reglement/AllReglement/all-reglement/all-reglement.component';
import { AddFactureComponent } from './Clients/ClientDetails/FactureTrituration/AjouterFacture/add-facture/add-facture.component';
import { AllGrignonsComponent } from './Grignons/AllGrignons/all-grignons/all-grignons.component';
import { AddGrignonsComponent } from './Grignons/AddGrignons/add-grignons/add-grignons.component';
import { EditGrignonsComponent } from './Grignons/EditGrignons/edit-grignons/edit-grignons.component';
import { AllCaisseComponent } from './Caisse/AllCaisse/all-caisse/all-caisse.component';
import { AddCaisseComponent } from './Caisse/AddCaisse/add-caisse/add-caisse.component';
import { EditCaisseComponent } from './Caisse/EditCaisse/edit-caisse/edit-caisse.component';
import { StatistiquesComponent } from './Statiques/statistiques/statistiques.component';
import { TriturationStatistiqueComponent } from './Statiques/trituration-statistique/trituration-statistique.component';
import { AchatStatistiqueComponent } from './Statiques/achat-statistique/achat-statistique.component';
import { VenteStatistiqueComponent } from './Statiques/vente-statistique/vente-statistique.component';
import { GrignonsStatistiqueComponent } from './Statiques/grignons-statistique/grignons-statistique.component';
export const MY_FORMAT: MatDateFormats = {
  parse: {
  dateInput: 'DD/MM/YYYY',
  },
  display: {
  dateInput: 'DD/MM/YYYY',
  monthYearLabel: 'MMM YYYY',
  dateA11yLabel: 'DD/MM/YYYY',
  monthYearA11yLabel: 'MMMM YYYY',
  },
  };
@NgModule({
  declarations: [
    AppComponent,
    HomeLayoutComponent, LoginLayoutComponent, LoginComponent, NavigationComponent,
    HomeComponent, AllClientsComponent, AddClientComponent , EditClientComponent,
    AllTriturationComponent, AddTriturationComponent, EditTriturationComponent,
    AllAchatComponent, AddAchatComponent, EditAchatComponent, ClientDetailsComponent,
    AddStockageOlivesComponent, AllStockageOlivesComponent, EditStockageOlivesComponent,
    AllVenteHuileComponent, AddVenteHuileComponent, EditVenteHuileComponent,
    StockHuileComponent, AddDiversTransactionComponent, AllDiversTransactionComponent,
    EditDiversTransactionComponent, AllFactureTriturationComponent, AjouterReglementFactureComponent,
    AllReglementComponent, AddFactureComponent, AllGrignonsComponent,
    AddGrignonsComponent, EditGrignonsComponent, AllCaisseComponent, AddCaisseComponent,
    EditCaisseComponent, StatistiquesComponent,
    TriturationStatistiqueComponent, AchatStatistiqueComponent, VenteStatistiqueComponent, GrignonsStatistiqueComponent
  ],
  imports: [
    HttpClientModule,
    AppMaterialModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    MatPaginatorModule,
    SatDatepickerModule,
    SatNativeDateModule,
    ChartsModule
  ],
  exports: [ MatExpansionModule ],
  providers: [StatistiqueService , CaissesService , GrignonsService , ReglementService , FactureService ,
     DiversTransactionService , StockageHuileService , AchatService , TriturationService ,
     ClientServiceService , EmployeeService , Globals, AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
     {provide: MatDialogRef, useValue: {}} ,
     { provide: MAT_DATE_LOCALE, useValue: 'fr-FR' },
     { provide: MAT_DATE_FORMATS, useValue: MY_FORMAT }],
  bootstrap: [AppComponent],
  entryComponents: [StatistiquesComponent , EditCaisseComponent , AddCaisseComponent , AllCaisseComponent ,
    EditGrignonsComponent , AddGrignonsComponent , AllGrignonsComponent  , AddFactureComponent
    , AllReglementComponent , AjouterReglementFactureComponent ,
    EditDiversTransactionComponent  , AddDiversTransactionComponent
    , EditVenteHuileComponent , AddVenteHuileComponent,
    EditStockageOlivesComponent, AddStockageOlivesComponent, AllStockageOlivesComponent,
     EditAchatComponent, AddAchatComponent , AllAchatComponent, AllClientsComponent ,
    AddClientComponent , EditClientComponent , AllTriturationComponent
                    , AddTriturationComponent , EditTriturationComponent , ClientDetailsComponent ]
})
export class AppModule { }
