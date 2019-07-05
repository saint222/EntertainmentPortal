import { AuthService } from './api/auth.service';
import { RouterModule } from '@angular/router';
import { CellComponent } from './components/cell/cell.component';
import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { PlayersService } from './api/players.service';
import { SessionsService } from './api/sessions.service';
import { SessionComponent } from './components/session/session.component';
import { CommonModule } from '@angular/common';
import { CreateSessionComponent } from './components/create-session/create-session.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PlayerComponent } from './components/player/player.component';
import { PlayerInfoComponent } from './components/player-info/player-info.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { PrivacyComponent } from './components/privacy/privacy.component';
import { UserComponent } from './components/user/user.component';

@NgModule({
  declarations: [SessionComponent, CellComponent,
    CreateSessionComponent, PlayerComponent,
    PlayerInfoComponent, SignInComponent,
    RegisterComponent, HomeComponent,
    ConfirmEmailComponent, PrivacyComponent, UserComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule
  ],
  exports: [
    SignInComponent,
    SessionComponent,
    CreateSessionComponent,
    PlayerComponent,
    RouterModule,
    ReactiveFormsModule
],
  providers: [
    AuthService,
    PlayersService,
    SessionsService
   ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        };
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule,
                 @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
            'See also https://github.com/angular/angular/issues/20575');
        }
    }
}
