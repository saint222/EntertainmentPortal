import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {MenuComponent} from './tictactoe-game/components/menu/menu.component';
import {HomeComponent} from './tictactoe-game/components/home/home.component';
// import {UserComponent} from './tictactoe-game/components/user/user.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'menu', component: MenuComponent},
  {path: '', redirectTo: '/home', pathMatch: 'full'}
];

@NgModule({
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: []
})

export class AppRoutingModule {
}
