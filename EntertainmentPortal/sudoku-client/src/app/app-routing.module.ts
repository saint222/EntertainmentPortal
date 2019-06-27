import { PlayerInfoComponent } from './typescript-angular-client/components/player-info/player-info.component';
import { PlayerComponent } from './typescript-angular-client/components/player/player.component';
import { CreateSessionComponent } from './typescript-angular-client/components/create-session/create-session.component';
import { SessionComponent } from './typescript-angular-client/components/session/session.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotExistsComponent } from './shared/not-exists/not-exists.component';

const routes: Routes = [
  {path: 'player', component: PlayerComponent},
  {path: 'player/:id', component: PlayerInfoComponent},
  {path: 'session/:id', component: SessionComponent},
  {path: 'create-session', component: CreateSessionComponent},
  { path: '', redirectTo: 'player', pathMatch: 'full' },
  { path: '**', component: NotExistsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
