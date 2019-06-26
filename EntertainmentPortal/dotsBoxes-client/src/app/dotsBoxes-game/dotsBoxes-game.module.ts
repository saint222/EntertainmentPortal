import { GameBoardComponent } from './components/gameBoard/gameBoard.component';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [GameBoardComponent],
  imports: [CommonModule, HttpClientModule],
  exports: [GameBoardComponent]
})
export class DotsBoxesGameModule { }
