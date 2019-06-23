import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SessionComponent } from './components/session/session.component';
import { CellComponent } from './components/cell/cell.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [SessionComponent, CellComponent],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports:[SessionComponent]
})
export class GameboardModule { }
