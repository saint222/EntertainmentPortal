import { GetPlayerComponent } from './game/components/get-player/get-player/get-player.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PlayerListComponent } from './game/components/player-list/player-list.component';
import { PlayerInfoComponent } from './game/components/player-info/player-info.component';
import { AddPlayerComponent } from './game/components/add-player/add-player.component';

const routes: Routes = [
  {path: 'players', component: PlayerListComponent},
  {path: 'edit', component: AddPlayerComponent},
  {path: 'player/:id', component: PlayerInfoComponent},
  {path: 'get', component: GetPlayerComponent},
  {path: '', redirectTo: 'players', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {enableTracing: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
