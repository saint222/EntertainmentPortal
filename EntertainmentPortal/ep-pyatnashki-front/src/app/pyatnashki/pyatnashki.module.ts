import { DeckComponent } from './components/deck/deck.component';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { LeaderboardComponent } from './components/leaderboard/leaderboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TileComponent } from './components/tile/tile.component';

@NgModule({
  declarations: [DeckComponent, LeaderboardComponent, TileComponent],
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule, RouterModule],
  exports: [DeckComponent, LeaderboardComponent, TileComponent, RouterModule, ReactiveFormsModule]
})
export class PyatnashkiModule { }
