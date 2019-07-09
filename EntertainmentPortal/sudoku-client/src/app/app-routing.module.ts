import { PlayerInfoComponent } from './typescript-angular-client/components/player-info/player-info.component';
import { PlayerComponent } from './typescript-angular-client/components/player/player.component';
import { CreateSessionComponent } from './typescript-angular-client/components/create-session/create-session.component';
import { SessionComponent } from './typescript-angular-client/components/session/session.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotExistsComponent } from './shared/not-exists/not-exists.component';
import { SignInComponent } from './typescript-angular-client/components/sign-in/sign-in.component';
import { RegisterComponent } from './typescript-angular-client/components/register/register.component';
import { HomeComponent } from './typescript-angular-client/components/home/home.component';
import { PrivacyComponent } from './typescript-angular-client/components/privacy/privacy.component';
import { ReristeredComponent } from './typescript-angular-client/components/reristered/reristered.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'registered', component: ReristeredComponent},
  {path: 'privacy', component: PrivacyComponent},
  {path: 'player', component: PlayerComponent},
  {path: 'player/:id', component: PlayerInfoComponent},
  {path: 'session/:id', component: SessionComponent},
  {path: 'create-session', component: CreateSessionComponent},
  {path: 'signIn', component: SignInComponent},
  {path: 'register', component: RegisterComponent},
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotExistsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
