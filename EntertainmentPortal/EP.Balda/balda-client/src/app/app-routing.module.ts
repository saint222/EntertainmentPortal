import { LogOutComponent } from './authorization/components/log-out/log-out.component';
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
  {path: 'startGame', component: LayoutComponent,
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
  {path: 'playground', component: LayoutComponent,
  children: [
    { path: '', component:
    PlaygroundComponent}
  ]},
  {path: 'gameOver', component: LayoutComponent,
  children: [
    { path: '', component:
    GameOverComponent}
  ]},
  {path: 'register', component: StartPageComponent,
  children: [
    { path: '', component: RegistrationComponent}
  ]},
  {path: 'logOut', component: StartPageComponent,
  children: [
    { path: '', component: LogOutComponent}
  ]},
  {path: '', component: StartPageComponent,
  children: [
    { path: '', component: LoginComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
