import { HttpClientModule } from '@angular/common/http';
import { AccountService } from './account/services/account.service';
import { AuthguardService } from './account/services/authguard.service';

import { AccountModule } from './account/account.module';
import { PyatnashkiModule } from './pyatnashki/pyatnashki.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, PyatnashkiModule, AccountModule, SharedModule, HttpClientModule],
  providers: [AuthguardService, AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
