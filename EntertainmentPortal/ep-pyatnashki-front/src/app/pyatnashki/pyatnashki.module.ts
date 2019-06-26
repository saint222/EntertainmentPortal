import { DeckComponent } from './components/deck/deck.component';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { LeaderboardComponent } from './components/leaderboard/leaderboard.component';

@NgModule({
  declarations: [DeckComponent, LeaderboardComponent],
  imports: [CommonModule, HttpClientModule],
  exports: [DeckComponent, LeaderboardComponent]
})
export class PyatnashkiModule { }
