import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationGuard } from './guards/authentication-guard';

import { RoomsComponent } from './public/rooms/rooms.component';
import { RegisterComponent } from './public/register/register.component';

const routes: Routes = [
  { path: '', component: RegisterComponent },
  { path: 'rooms', component: RoomsComponent , canActivate: [AuthenticationGuard] },
  { path: '**', component: RegisterComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
