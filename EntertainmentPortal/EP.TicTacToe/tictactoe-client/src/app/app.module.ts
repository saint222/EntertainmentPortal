import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {AppComponent} from './app.component';
import {HomeComponent} from './tictactoe-game/components/home/home.component';
import {MenuComponent} from './tictactoe-game/components/menu/menu.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {CustomMaterialModule} from './core/material.module';
import {AppRoutingModule} from './app-routing.module';
import {BoardComponent} from './tictactoe-game/components/board/board.component';
import {FormsModule} from '@angular/forms';
import {OAuthModule} from 'angular-oauth2-oidc';
import {AuthService} from './core/services/auth.service';
import {TictactoeGameModule} from './tictactoe-game/tictactoe-game.module';
import {ShareService} from './core/services/share.service';
import {HttpClientModule} from '@angular/common/http';
import {ApiModule} from './core/api.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    BoardComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CustomMaterialModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    OAuthModule.forRoot(),
    TictactoeGameModule,
    ApiModule.forRoot(null)
  ],
  providers: [AuthService, ShareService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
