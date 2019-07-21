import { AdditionalInfoModule } from './additionalinfo/additional-info/additional-info.module';
import { AuthModule } from './authorization/auth.module';
import { HttpClientModule  } from '@angular/common/http';
import { GameModule } from './game/game.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from './layout/layout.module';
import { WelcomeComponent } from './additionalinfo/additional-info/welcome/welcome.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    GameModule,
    AuthModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    LayoutModule,
    AdditionalInfoModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
