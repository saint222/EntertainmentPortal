import { NotExistsComponent } from './../not-exists/not-exists.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [NotExistsComponent],
  imports: [CommonModule],
  exports: [NotExistsComponent]
})
export class SharedModule { }
