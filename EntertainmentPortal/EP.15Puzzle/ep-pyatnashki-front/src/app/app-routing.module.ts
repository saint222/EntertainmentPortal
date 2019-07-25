import { LogoutService } from './account/services/logout.service';
import { AuthCallbackComponent } from './account/components/login/auth-callback/auth-callback.component';
import { AuthguardService } from './account/services/authguard.service';
import { NotExistsComponent } from './shared/not-exists/not-exists.component';
import { LeaderboardComponent } from './pyatnashki/components/leaderboard/leaderboard.component';
import { DeckComponent } from './pyatnashki/components/deck/deck.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';

const routes: Routes = [
  {path: 'login', component: DeckComponent, canActivate: [AuthguardService]},
  {path: 'logout', component: DeckComponent, canActivate: [LogoutService]},
  {path: 'auth-callback', component: AuthCallbackComponent},
  {path: 'deck', component: DeckComponent},
  {path: 'leaderboard', component: LeaderboardComponent},
  {path: '', redirectTo: 'deck', pathMatch: 'full' },
  {path: '**', component: NotExistsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
