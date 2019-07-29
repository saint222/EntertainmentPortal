import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {LayoutComponent} from './components/layout/layout.component';
import {FlexModule} from '@angular/flex-layout';
import {AboutDialogComponent} from './components/about-dialog/about-dialog.component';
import {CallDialogComponent} from './components/call-dialog/call-dialog.component';
import {GameSetupComponent} from './components/game-setup/game-setup.component';
import {CustomMaterialModule} from '../core/material.module';
import {MatSelectModule} from '@angular/material';
import {FormsModule} from '@angular/forms';


@NgModule({
  declarations: [
    LayoutComponent,
    AboutDialogComponent,
    CallDialogComponent,
    GameSetupComponent],
  exports: [
    LayoutComponent
  ],
  imports: [
    CommonModule,
    FlexModule,
    CustomMaterialModule,
    MatSelectModule,
    FormsModule
  ],
  entryComponents: [
    GameSetupComponent,
    AboutDialogComponent,
    CallDialogComponent
  ],
})
export class TictactoeGameModule {
}
