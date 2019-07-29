import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { RegistrationComponent } from './components/registration/registration.component';
import { StartPageComponent } from './components/start-page/start-page.component';

@NgModule({
  declarations: [LoginComponent, RegistrationComponent, StartPageComponent],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule, RouterModule],
  exports: [
    LoginComponent,
    RegistrationComponent,
    StartPageComponent,
    RouterModule,
    ReactiveFormsModule
  ]
})

export class AuthModule {
 }
