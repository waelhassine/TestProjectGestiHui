import { AuthenticationService } from '../../Service/authentication.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import {Globals} from '../globals';
@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {
  isLoggedIn$: Observable<boolean>;
  currentUser: any;

  constructor( public globals: Globals , private authenticationService: AuthenticationService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
   }

  ngOnInit() {
    this.isLoggedIn$ = this.authenticationService.isLoggedIn;
  }
  onLogout() {
    this.authenticationService.logout();
  }

}
