import { EndgameScreenComponent } from './hangman-game/components/endgame-screen/endgame-screen.component';
import { GameFieldComponent } from './hangman-game/components/game-field/game-field.component';
import { StartScreenComponent } from './hangman-game/components/start-screen/start-screen.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {path: 'startScreen', component: StartScreenComponent},
  {path: 'gameSession', component: GameFieldComponent},
  {path: 'endScreen', component: EndgameScreenComponent},
  {path: '', redirectTo: 'startScreen', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
