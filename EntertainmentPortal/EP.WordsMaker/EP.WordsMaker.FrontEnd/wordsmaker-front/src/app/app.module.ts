
import { GameModule } from './game/modules/game/game.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { OAuthModule } from 'angular-oauth2-oidc';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
// import { GetPlayerComponent } from './game/components/get-player/get-player.component';
import { PlayingFieldComponent } from './game/components/playing-field/playing-field.component';
import { DashboardComponent } from './game/components/dashboard/dashboard.component';
import { GameFieldComponent } from './game/components/game-field/game-field.component';


@NgModule({
  declarations: [
    AppComponent,
    // GetPlayerComponent,
    PlayingFieldComponent,
    DashboardComponent,
    GameFieldComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    GameModule,
    OAuthModule.forRoot(),
    //ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
