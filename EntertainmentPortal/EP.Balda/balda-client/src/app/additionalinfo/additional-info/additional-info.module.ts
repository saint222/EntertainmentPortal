import { RulesComponent } from './rules/rules.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsComponent } from './contacts/contacts.component';

@NgModule({
  declarations: [WelcomeComponent, ContactsComponent, RulesComponent],
  imports: [
    CommonModule
  ],
  exports: [
    WelcomeComponent, ContactsComponent, RulesComponent
  ]
})
export class AdditionalInfoModule { }
