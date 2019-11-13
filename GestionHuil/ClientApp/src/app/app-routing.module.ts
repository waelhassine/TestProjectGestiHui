import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../Service/auth-guard' ;
import { HomeLayoutComponent } from '../app/home-layout/home-layout.component';

import { LoginComponent } from '../app/login/login.component';

import { HomeComponent } from '../app/home/home.component';
import {LoginLayoutComponent } from '../app/login-layout/login-layout.component';
import { AllClientsComponent} from '../app/Clients/AllClients/all-clients/all-clients.component';
import {AllTriturationComponent} from '../app/Trituration/AllTrituration/all-trituration/all-trituration.component';
import { AllAchatComponent } from '../app/Achat/AllAchat/all-achat/all-achat.component';
import { ClientDetailsComponent} from '../app/Clients/ClientDetails/client-details/client-details.component';
import { AllStockageOlivesComponent} from '../app/StockageOlives/AllStockageOlives/all-stockage-olives/all-stockage-olives.component';
import { AllVenteHuileComponent } from '../app/Vente/AllVente/all-vente-huile/all-vente-huile.component';
import { AllGrignonsComponent } from '../app/Grignons/AllGrignons/all-grignons/all-grignons.component';
import { AllCaisseComponent } from '../app/Caisse/AllCaisse/all-caisse/all-caisse.component';
import { StatistiquesComponent } from '../app/Statiques/statistiques/statistiques.component';
const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: 'login', component: LoginLayoutComponent, data: {title: 'First Component'},
    children: [
      {path: '', component: LoginComponent}
    ]
  },
  { path: 'main', component: HomeLayoutComponent, canActivate: [AuthGuard] ,
  children: [
  { path: 'dash', component: HomeComponent },
  { path: 'contact', component: AllClientsComponent},
  { path: 'contact/contactDetails/:id', component: ClientDetailsComponent},
  { path: 'trituration', component: AllTriturationComponent},
  { path: 'achat', component: AllAchatComponent},
  { path: 'stockageolive', component: AllStockageOlivesComponent},
  { path: 'vente', component: AllVenteHuileComponent},
  { path: 'grignons', component: AllGrignonsComponent},
  { path: 'caisses', component: AllCaisseComponent},
  { path: 'sta', component: StatistiquesComponent},
  ]
}
];
@NgModule({
  imports: [RouterModule.forRoot(
    routes,
    { useHash: false } // <-- debugging purposes only
  )],
  exports: [RouterModule]
})
export class AppRoutingModule { }
