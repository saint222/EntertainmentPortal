import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MenuComponent } from './tictactoe-game/components/menu/menu.component';
import { HomeComponent } from './tictactoe-game/components/home/home.component';
// import {UserComponent} from './tictactoe-game/components/user/user.component';

const routes: Routes = [
  // { path: 'home', component: HomeComponent},
  {path: 'menu', component: MenuComponent},
  // { path: 'user', component: UserComponent },
  {path: '', redirectTo: 'menu', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {enableTracing: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
