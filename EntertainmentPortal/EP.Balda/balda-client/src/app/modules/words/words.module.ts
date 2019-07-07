import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {WordsComponent} from './components/words/words.component';
import {GridModule} from '@angular/flex-layout';

@NgModule({
  declarations: [WordsComponent],
  exports: [
    WordsComponent
  ],
  imports: [
    CommonModule,
    GridModule
  ]
})
export class WordsModule {
}
