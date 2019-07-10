import { Routes, RouterModule } from '@angular/router';
import { StartGameComponent } from './game/components/start-game/start-game.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {path: 'startGame', component: StartGameComponent},
  {path: '', redirectTo: 'startScreen', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
