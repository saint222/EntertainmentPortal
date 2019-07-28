import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.sass']
})
export class ContactsComponent implements OnInit {
  contactGroup: FormGroup;
  photo: any = 'assets/pics/photo.jpg';

  constructor() {
   }

  ngOnInit() {
  }
}
