import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegisterUserPageComponent } from './pages/register-user-page/register-user-page.component';
import { ConsentPageComponent } from './pages/consent-page/consent-page.component';

const routes: Routes = [
  { path:'Account/Login', component : LoginPageComponent },
  { path:'register', component : RegisterUserPageComponent },
  { path:'consent', component : ConsentPageComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IdentityRoutingModule { }
