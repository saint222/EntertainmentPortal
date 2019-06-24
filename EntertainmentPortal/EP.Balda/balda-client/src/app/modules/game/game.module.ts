import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {GameComponent} from './components/game/game.component';
import {MatGridListModule} from '@angular/material';

@NgModule({
  declarations: [GameComponent],
  exports: [
    GameComponent
  ],
  imports: [
    CommonModule,
    MatGridListModule
  ]
})
export class GameModule {
}
