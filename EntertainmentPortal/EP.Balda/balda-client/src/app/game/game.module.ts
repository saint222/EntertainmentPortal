import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartGameComponent } from './components/start-game/start-game.component';
import { PlaygroundComponent } from './components/playground/playground.component';
import { GameOverComponent } from './components/game-over/game-over.component';

@NgModule({
  declarations: [StartGameComponent, PlaygroundComponent, GameOverComponent],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: []
})
export class GameModule { }
