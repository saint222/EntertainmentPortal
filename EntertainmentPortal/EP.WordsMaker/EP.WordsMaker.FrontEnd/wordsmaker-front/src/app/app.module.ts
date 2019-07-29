import { AuthService } from './game/services/auth.service';
import { GameModule } from './game/modules/game/game.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './game/components/login/login.component';
import { PlayingFieldComponent } from './game/components/playing-field/playing-field.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    //PlayingFieldComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    OAuthModule.forRoot(),
    //ReactiveFormsModule,
    FormsModule,
    GameModule
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
