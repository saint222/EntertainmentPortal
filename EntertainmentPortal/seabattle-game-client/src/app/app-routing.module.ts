import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { ShipplacerComponent} from './shipplacer/shipplacer.component'
import { DemoGuard } from 'src/demo.guard';

const routes: Routes = [
  { path: 'app', component: AuthComponent },
  { path: 'app/:id',
    component: ShipplacerComponent,
    canActivate: [DemoGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
