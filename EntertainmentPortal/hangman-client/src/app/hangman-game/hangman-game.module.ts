import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameFieldComponent } from './components/game-field/game-field.component';
import {HttpClientModule} from '@angular/common/http';
import { StartScreenComponent } from './components/start-screen/start-screen.component';
import { LooseGameComponent } from './components/loose-game/loose-game.component';
import { WinGameComponent } from './components/win-game/win-game.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PageNotExistsComponent } from './components/page-not-exists/page-not-exists.component';

@NgModule({
  declarations: [GameFieldComponent, StartScreenComponent, LooseGameComponent, WinGameComponent, LoginComponent, RegisterComponent, PageNotExistsComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  exports: [GameFieldComponent, StartScreenComponent, LooseGameComponent, LoginComponent, RegisterComponent]
})
export class HangmanGameModule { }
