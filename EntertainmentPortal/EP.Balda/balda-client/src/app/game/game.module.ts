import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MapComponent } from './components/map/map.component';
import { StartGameComponent } from './components/start-game/start-game.component';

@NgModule({
  declarations: [MapComponent, StartGameComponent],
  imports: [
    CommonModule
  ],
  exports: [MapComponent, StartGameComponent]
})
export class GameModule { }
