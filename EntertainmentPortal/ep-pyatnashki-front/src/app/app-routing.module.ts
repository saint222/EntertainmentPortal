import { UserinfoComponent } from './pyatnashki/components/userinfo/userinfo.component';
import { NotExistsComponent } from './shared/not-exists/not-exists.component';
import { LeaderboardComponent } from './pyatnashki/components/leaderboard/leaderboard.component';
import { DeckComponent } from './pyatnashki/components/deck/deck.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './account/components/login/login.component';

const routes: Routes = [
  {path: 'account', component: LoginComponent},
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
