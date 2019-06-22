import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameFieldComponent } from './components/game-field/game-field.component';
import {HttpClientModule} from '@angular/common/http';

@NgModule({
  declarations: [GameFieldComponent],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports: [GameFieldComponent]
})
export class HangmanGameModule { }
