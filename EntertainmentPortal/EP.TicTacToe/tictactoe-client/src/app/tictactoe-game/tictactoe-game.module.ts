import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './components/layout/layout.component';
import {FlexModule} from '@angular/flex-layout';



@NgModule({
  declarations: [LayoutComponent],
  exports: [
    LayoutComponent
  ],
  imports: [
    CommonModule,
    FlexModule
  ]
})
export class TictactoeGameModule { }
