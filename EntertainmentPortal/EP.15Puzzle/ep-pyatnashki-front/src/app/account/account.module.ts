import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthCallbackComponent } from './components/login/auth-callback/auth-callback.component';

@NgModule({
  declarations: [AuthCallbackComponent],
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  exports: [RouterModule, ReactiveFormsModule]
})
export class AccountModule { }
