import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CellComponent } from './battlefield/cell/cell.component';
import { FieldComponent } from './battlefield/field/field.component';
import { ButtonsComponent } from './game/buttons/buttons.component';
import { ShipplacerComponent } from './shipplacer/shipplacer.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BattlefieldService } from './battlefield/services/battlefield.service';


@NgModule({
  declarations: [
    AppComponent,
    CellComponent,
    FieldComponent,
    ButtonsComponent,
    ShipplacerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [BattlefieldService],
  bootstrap: [AppComponent]
})
export class AppModule { }
