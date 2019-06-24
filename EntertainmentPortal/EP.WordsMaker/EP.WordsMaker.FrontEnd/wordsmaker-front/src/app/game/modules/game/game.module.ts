import { AddPlayerComponent } from './../../components/add-player/add-player.component';
import { PlayerListComponent } from './../../components/player-list/player-list.component';
import { PlayerInfoComponent } from './../../components/player-info/player-info.component';
import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [PlayerInfoComponent, PlayerListComponent, AddPlayerComponent],
  imports: [CommonModule, HttpClientModule],
  exports: [PlayerListComponent, AddPlayerComponent]
})
export class GameModule { }
