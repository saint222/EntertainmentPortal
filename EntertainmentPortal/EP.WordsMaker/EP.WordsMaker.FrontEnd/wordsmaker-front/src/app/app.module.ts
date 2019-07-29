import { AuthService } from './game/services/auth.service';

import { GameModule } from './game/modules/game/game.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { OAuthModule } from 'angular-oauth2-oidc';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { PlayingFieldComponent } from './game/components/playing-field/playing-field.component';
import { DashboardComponent } from './game/components/dashboard/dashboard.component';
import { GameFieldComponent } from './game/components/game-field/game-field.component';
import { LoginComponent } from './game/components/login/login.component';
import { HomeComponent } from './game/components/home/home.component';


@NgModule({
  declarations: [
    AppComponent,
    PlayingFieldComponent,
    DashboardComponent,
    GameFieldComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    GameModule,
    OAuthModule.forRoot(),
    //ReactiveFormsModule,
    FormsModule
  ],
  providers: [AuthService, PlayingFieldComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
