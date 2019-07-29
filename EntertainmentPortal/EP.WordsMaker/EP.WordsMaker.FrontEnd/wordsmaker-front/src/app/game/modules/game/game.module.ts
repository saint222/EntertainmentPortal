import { PlayingFieldComponent } from './../../components/playing-field/playing-field.component';
import { GetPlayerComponent } from './../../components/get-player/get-player.component';
import { HomeComponent } from '../../components/home/home.component';
import { RecordBoardComponent } from '../../components/record-board/record-board.component';
import { PlayerInfoComponent } from './../../components/player-info/player-info.component';
import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [PlayerInfoComponent, RecordBoardComponent, HomeComponent, GetPlayerComponent, PlayingFieldComponent],
  imports: [CommonModule, HttpClientModule, ReactiveFormsModule, RouterModule],
  exports: [PlayerInfoComponent, RecordBoardComponent, HomeComponent, GetPlayerComponent, PlayingFieldComponent]
})
export class GameModule { }
