import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameFieldComponent } from './components/game-field/game-field.component';
import {HttpClientModule} from '@angular/common/http';
import { AlphabetButtonsComponent } from './components/alphabet-buttons/alphabet-buttons.component';
import { StartGameComponent } from './components/start-game/start-game.component';

@NgModule({
  declarations: [GameFieldComponent, AlphabetButtonsComponent, StartGameComponent],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports: [StartGameComponent]
})
export class HangmanGameModule { }
