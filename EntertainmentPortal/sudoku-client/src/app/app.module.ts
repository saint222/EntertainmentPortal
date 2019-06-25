import { ApiModule } from './typescript-angular-client/api.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NotExistsComponent } from './shared/not-exists/not-exists.component';

@NgModule({
  declarations: [
    AppComponent,
    NotExistsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ApiModule.forRoot(null)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
