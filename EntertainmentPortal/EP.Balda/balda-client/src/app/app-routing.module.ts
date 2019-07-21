import { WelcomeComponent } from './additionalinfo/additional-info/welcome/welcome.component';
import { ContactsComponent } from './additionalinfo/additional-info/contacts/contacts.component';
import { RulesComponent } from './additionalinfo/additional-info/rules/rules.component';
import { LayoutComponent } from './layout/layout/layout.component';
import { GameOverComponent } from './game/components/game-over/game-over.component';
import { PlaygroundComponent } from './game/components/playground/playground.component';
import { StartPageComponent } from './authorization/components/start-page/start-page.component';
import { RegistrationComponent } from './authorization/components/registration/registration.component';
import { LoginComponent } from './authorization/components/login/login.component';
import { Routes, RouterModule } from '@angular/router';
import { StartGameComponent } from './game/components/start-game/start-game.component';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {path: 'startGame/:userId', component: LayoutComponent,
    children: [
      { path: '', component:
      StartGameComponent}
    ]},
    {path: 'rules', component: LayoutComponent,
    children: [
      { path: '', component:
      RulesComponent}
    ]},
    {path: 'contacts', component: LayoutComponent,
    children: [
      { path: '', component:
      ContactsComponent}
    ]},
    {path: 'welcome', component: LayoutComponent,
    children: [
      { path: '', component:
      WelcomeComponent}
    ]},
  {path: 'playground/:userId:gameId:mapId', component: LayoutComponent,
  children: [
    { path: '', component:
    PlaygroundComponent}
  ]},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegistrationComponent},
  {path: 'gameOver', component: LayoutComponent,
  children: [
    { path: '', component:
    GameOverComponent}
  ]},
  {path: '', component: StartPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
