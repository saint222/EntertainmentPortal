import { DeckComponent } from './components/deck/deck.component';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [DeckComponent],
  imports: [CommonModule, HttpClientModule],
  exports: [DeckComponent]
})
export class PyatnashkiModule { }
