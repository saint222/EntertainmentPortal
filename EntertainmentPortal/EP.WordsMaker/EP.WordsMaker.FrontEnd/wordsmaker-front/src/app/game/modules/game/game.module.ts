import { AddPlayerComponent } from './../../components/add-player/add-player.component';
import { PlayerListComponent } from './../../components/player-list/player-list.component';
import { PlayerInfoComponent } from './../../components/player-info/player-info.component';
import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [PlayerInfoComponent, PlayerListComponent, AddPlayerComponent],
  imports: [CommonModule, HttpClientModule, ReactiveFormsModule, RouterModule],
  exports: [PlayerInfoComponent ,PlayerListComponent, AddPlayerComponent, ReactiveFormsModule, RouterModule]
})
export class GameModule { }
