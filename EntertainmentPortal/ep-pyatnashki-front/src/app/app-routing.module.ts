import { AuthCallbackComponent } from './account/components/login/auth-callback/auth-callback.component';
import { AuthguardService } from './account/services/authguard.service';
import { UserinfoComponent } from './pyatnashki/components/userinfo/userinfo.component';
import { NotExistsComponent } from './shared/not-exists/not-exists.component';
import { LeaderboardComponent } from './pyatnashki/components/leaderboard/leaderboard.component';
import { DeckComponent } from './pyatnashki/components/deck/deck.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './account/components/login/login.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent, canActivate: [AuthguardService]},
  {path: 'auth-callback', component: AuthCallbackComponent},
  {path: 'deck', component: DeckComponent},
  {path: 'leaderboard', component: LeaderboardComponent},
  {path: 'userinfo', component: UserinfoComponent},
  {path: '', redirectTo: 'deck', pathMatch: 'full' },
  {path: '**', component: NotExistsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
