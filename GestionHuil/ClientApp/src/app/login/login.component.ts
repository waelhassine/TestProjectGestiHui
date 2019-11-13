import { AuthenticationService } from '../../Service/authentication.service';
import {Globals} from '../globals';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {MatSnackBar} from '@angular/material/snack-bar';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  private formSubmitAttempt: boolean;
  loading = false;
    submitted = false;

    constructor(private snackBar: MatSnackBar,
                public globals: Globals , private fb: FormBuilder ,
                private authenticationService: AuthenticationService, private router: Router ) {}

  ngOnInit() {
    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  isFieldInvalid(field: string) {
    return (
      (!this.form.get(field).valid && this.form.get(field).touched) ||
      (this.form.get(field).untouched && this.formSubmitAttempt)
    );
  }

  onSubmit(loginform) {
    this.submitted = true;

    // stop here if form is invalid
    if (this.form.invalid) {
        return;
    }

    this.loading = true;
    this.authenticationService.login(loginform.email, loginform.password)
    .subscribe(
        data => {
           console.log(data);  if (data.fonction === 'Technicien') {
                console.log(data);
                this.globals.setCurrency(false) ;
                console.log(this.globals.getCurrency());
              } else if (data.fonction === 'Responsable' ) {
                this.globals.setCurrency(true) ;
                console.log(this.globals.getCurrency());
              }
           this.openSnackBar('Bonjour vous êtes connecté' , 'Ok');
           this.router.navigate(['main/contact']);
        },
        error => {
          this.openSnackBar(error , 'Ok'); console.log(error);
          this.loading = false;
        });
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }


}
