import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GameFieldComponent } from './game/components/game-field/game-field.component';
import { GetPlayerComponent } from './game/components/get-player/get-player.component';
import { RecordBoardComponent } from './game/components/record-board/record-board.component';
import { HomeComponent } from './game/components/home/home.component';

const routes: Routes = [
  {path: 'recordBoard', component: RecordBoardComponent},
  {path: 'home', component: HomeComponent},
  {path: 'game', component: GameFieldComponent},
  {path: '', redirectTo: 'home', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {enableTracing: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
