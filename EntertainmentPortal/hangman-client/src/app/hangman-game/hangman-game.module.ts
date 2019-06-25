import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameFieldComponent } from './components/game-field/game-field.component';
import {HttpClientModule} from '@angular/common/http';
import { StartScreenComponent } from './components/start-screen/start-screen.component';
import { EndgameScreenComponent } from './components/endgame-screen/endgame-screen.component';

@NgModule({
  declarations: [GameFieldComponent, StartScreenComponent, EndgameScreenComponent],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports: [GameFieldComponent, StartScreenComponent, EndgameScreenComponent]
})
export class HangmanGameModule { }
