import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotExistsComponent } from './not-exists/not-exists.component';

@NgModule({
  declarations: [NotExistsComponent],
  imports: [CommonModule],
  exports: [NotExistsComponent]
})
export class SharedModule { }
