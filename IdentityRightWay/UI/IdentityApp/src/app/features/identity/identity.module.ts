import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IdentityRoutingModule } from './identity-routing.module';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { ForgotPasswordPageComponent } from './pages/forgot-password-page/forgot-password-page.component';
import { ResetPasswordPageComponent } from './pages/reset-password-page/reset-password-page.component';
import { LoginComponent } from './components/login/login.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { IdentityService } from './services/identity.service';
import { ConsentPageComponent } from './pages/consent-page/consent-page.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';
import { StoreModule } from '@ngrx/store';
import { reducers } from './store/stores';
import { EffectsModule } from '@ngrx/effects';
import { BackendEffects } from './store/effects/backend.effects';
import { RegisterUserPageComponent } from './pages/register-user-page/register-user-page.component';
import { ErrorInterceptor } from 'src/app/core/ErrorInterceptor';

@NgModule({
  declarations: [
    LoginPageComponent, 
    ForgotPasswordPageComponent, 
    ResetPasswordPageComponent, 
    LoginComponent, 
    ForgotPasswordComponent, 
    ResetPasswordComponent, ConsentPageComponent, RegisterUserComponent, RegisterUserPageComponent],
  imports: [
    CommonModule,
    IdentityRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    StoreModule.forFeature('identity', reducers),
    EffectsModule.forFeature([BackendEffects]),
  ],
  providers : [
    IdentityService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ]
})
export class IdentityModule { }
