import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameFieldComponent } from './components/game-field/game-field.component';
import {HttpClientModule} from '@angular/common/http';
import { StartScreenComponent } from './components/start-screen/start-screen.component';
import { LooseGameComponent } from './components/loose-game/loose-game.component';
import { WinGameComponent } from './components/win-game/win-game.component';
import { PageNotExistsComponent } from './components/page-not-exists/page-not-exists.component';

@NgModule({
  declarations: [GameFieldComponent, StartScreenComponent, LooseGameComponent, WinGameComponent, PageNotExistsComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  exports: [GameFieldComponent, StartScreenComponent, LooseGameComponent]
})
export class HangmanGameModule { }
