import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidationErrors } from '@angular/forms';
import { IdentityService } from '../../services/identity.service';
import { filter, switchMap, mergeMap, catchError, tap } from 'rxjs/operators';
import { iif, of, throwError, Subject, observable, Observable, empty } from 'rxjs';
import { LoginDto } from 'src/app/models/LoginDto';
import { Store } from '@ngrx/store';
import { FetchUserLogin } from '../../store/actions/backend.actions';
import { reducers, IdentityState } from '../../store/stores';
import { AppState } from 'src/app/app.state';
import { LoginStore } from '../../store/reducers/login.reducers';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  loginStore$: Observable<LoginStore>;


  constructor(private store : Store<AppState>, private router : Router) { }


  ngOnInit() {
    this.loginStore$ = this.store.select(o => o.identity.login)
  }

  login() {
    this.store.dispatch(new FetchUserLogin(
      { 
        email : this.loginForm.get('email').value,
        password : this.loginForm.get('password').value
      }))
  }
  goToRegister(){
    this.router.navigate(['/register'])
  }

}
