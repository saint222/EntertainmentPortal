import { PageNotExistsComponent } from './hangman-game/components/page-not-exists/page-not-exists.component';
import { LooseGameComponent } from './hangman-game/components/loose-game/loose-game.component';
import { WinGameComponent } from './hangman-game/components/win-game/win-game.component';
import { GameFieldComponent } from './hangman-game/components/game-field/game-field.component';
import { StartScreenComponent } from './hangman-game/components/start-screen/start-screen.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {path: 'startScreen', component: StartScreenComponent},
  {path: 'gameSession', component: GameFieldComponent},
  {path: 'win', component: WinGameComponent},
  {path: 'loose', component: LooseGameComponent},
  {path: '', redirectTo: 'startScreen', pathMatch: 'full'},
  {path: '**', component: PageNotExistsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
