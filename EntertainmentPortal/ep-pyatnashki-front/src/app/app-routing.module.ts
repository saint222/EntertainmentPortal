import { NotExistsComponent } from './shared/not-exists/not-exists.component';
import { LeaderboardComponent } from './pyatnashki/components/leaderboard/leaderboard.component';
import { DeckComponent } from './pyatnashki/components/deck/deck.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';

const routes: Routes = [
  {path: 'deck', component: DeckComponent},
  {path: 'leaderboard', component: LeaderboardComponent},
  {path: '', redirectTo: 'deck', pathMatch: 'full' },
  { path: '**', component: NotExistsComponent }
]

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
