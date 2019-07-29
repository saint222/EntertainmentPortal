import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CellComponent } from './battlefield/cell/cell.component';
import { FieldComponent } from './battlefield/field/field.component';
import { ShipplacerComponent } from './shipplacer/shipplacer.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AuthComponent, AuthService } from './auth/auth.component';
import { ShipService } from './Services/ship.service';
import { EnemyService } from './Services/enemy.service';
import { AuthInterceptor } from './interceptor';


@NgModule({
  declarations: [
    AppComponent,
    CellComponent,
    FieldComponent,
    ShipplacerComponent,
    AuthComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    OAuthModule.forRoot()
  ],
  providers: [AuthService,
              ShipService,
              EnemyService,
              { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
