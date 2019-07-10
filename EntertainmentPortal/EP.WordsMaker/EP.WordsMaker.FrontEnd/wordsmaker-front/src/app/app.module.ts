
import { GameModule } from './game/modules/game/game.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// import { GetPlayerComponent } from './game/components/get-player/get-player.component';
import { PlayingFieldComponent } from './game/components/playing-field/playing-field.component';

@NgModule({
  declarations: [
    AppComponent,
    // GetPlayerComponent,
    PlayingFieldComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    GameModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
