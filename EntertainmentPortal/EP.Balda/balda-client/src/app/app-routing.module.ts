import { PlaygroundComponent } from './game/components/playground/playground.component';
import { MapComponent } from './game/components/map/map.component';
import { StartPageComponent } from './authorization/components/start-page/start-page.component';
import { RegistrationComponent } from './authorization/components/registration/registration.component';
import { LoginComponent } from './authorization/components/login/login.component';
import { Routes, RouterModule } from '@angular/router';
import { StartGameComponent } from './game/components/start-game/start-game.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {path: 'startGame/:userId', component: StartGameComponent},
  {path: 'playground/:userId:gameId:mapId', component: PlaygroundComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegistrationComponent},
  {path: '', component: StartPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
