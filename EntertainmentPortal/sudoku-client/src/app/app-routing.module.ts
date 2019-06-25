import { CreateSessionComponent } from './typescript-angular-client/components/create-session/create-session.component';
import { SessionComponent } from './typescript-angular-client/components/session/session.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotExistsComponent } from './shared/not-exists/not-exists.component';

const routes: Routes = [
  {path: 'session', component: SessionComponent},
  {path: 'session/:id', component: SessionComponent},
  {path: 'create-session', component: CreateSessionComponent},
  { path: '', redirectTo: 'session', pathMatch: 'full' },
  { path: '**', component: NotExistsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
