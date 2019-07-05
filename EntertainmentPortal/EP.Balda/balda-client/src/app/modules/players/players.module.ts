import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PlayerComponent} from './components/player/player.component';
import {GridModule} from '@angular/flex-layout';

@NgModule({
  declarations: [PlayerComponent],
  exports: [
    PlayerComponent
  ],
  imports: [
    CommonModule,
    GridModule
  ]
})
export class PlayersModule {
}
