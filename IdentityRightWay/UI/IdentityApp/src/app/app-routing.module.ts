import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IdentityModule } from './features/identity/identity.module';

const routes: Routes = [
  { path : '' , loadChildren: './features/identity/identity.module#IdentityModule' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
