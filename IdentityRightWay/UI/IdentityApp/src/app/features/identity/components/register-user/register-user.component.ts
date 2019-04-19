import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AppState } from 'src/app/app.state';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { RegisterStore } from '../../store/reducers/register.reducer';
import { FetchUserRegister } from '../../store/actions/backend.actions';
import { v4 as uuid } from 'uuid';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss']
})
export class RegisterUserComponent implements OnInit {
  registerStore$: Observable<RegisterStore>;

  constructor(private store : Store<AppState>) { }

  registerForm = new FormGroup({
    name: new FormControl('', Validators.required),
    fristName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required),
  });


  ngOnInit() {
    this.registerStore$ = this.store.select(o => o.identity.register)
  }

  register(){
    this.store.dispatch(new FetchUserRegister({
      confirmPassword : this.registerForm.get('confirmPassword').value,
      password : this.registerForm.get('password').value,
      name : this.registerForm.get('name').value,
      lastName : this.registerForm.get('lastName').value,
      fristName : this.registerForm.get('fristName').value,
      email : this.registerForm.get('email').value,
      id : uuid()
    }))
  }

}
