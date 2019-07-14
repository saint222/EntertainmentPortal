import { MapComponent } from './game/components/map/map.component';
import { StartPageComponent } from './authorization/components/start-page/start-page.component';
import { RegistrationComponent } from './authorization/components/registration/registration.component';
import { LoginComponent } from './authorization/components/login/login.component';
import { Routes, RouterModule } from '@angular/router';
import { StartGameComponent } from './game/components/start-game/start-game.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {path: 'startGame', component: StartGameComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegistrationComponent},
  {path: 'playground', component: MapComponent },
  {path: '', component: StartPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
