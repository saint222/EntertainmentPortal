import { LoginComponent } from './components/login/login.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AuthCallbackComponent } from './components/login/auth-callback/auth-callback.component';

@NgModule({
  declarations: [LoginComponent, AuthCallbackComponent],
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  exports: [LoginComponent, RouterModule, ReactiveFormsModule]
})
export class AccountModule { }
