import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './tictactoe-game/components/home/home.component';
import {BoardComponent} from './tictactoe-game/components/board/board.component';
import {MenuComponent} from './tictactoe-game/components/menu/menu.component';


const routes: Routes = [
  // {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: '', component: HomeComponent},
  {path: 'home', component: HomeComponent},
  {path: 'menu', component: MenuComponent},
  {path: 'board', component: BoardComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes, {enableTracing: true})],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
