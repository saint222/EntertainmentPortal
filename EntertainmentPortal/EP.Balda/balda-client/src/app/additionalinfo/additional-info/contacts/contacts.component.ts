import { Feedback } from './../../../game/models/feedback';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.sass']
})
export class ContactsComponent implements OnInit {
  photo: any = 'assets/pics/photo.jpg';
  feedbackGroup: FormGroup;
  textMessage: string;

  constructor(private fb: FormBuilder, private router: Router, private http: HttpClient) {
    this.feedbackGroup = this.fb.group({
      name: [''],
      email: [''],
      message: ['']
    });
  }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {
    this.sendFeedback(form.value).subscribe(p => {
      console.log(p);
      form.reset();
      this.textMessage = 'Your message successfully sent!';
      setTimeout(() => {
        this.textMessage = '';
    }, 5000);
    },
    (err: HttpErrorResponse) => {
      return console.log(err.error[0]);
    });
  }

  sendFeedback(feedback: Feedback) {
    const httpOptions = {
     headers: new HttpHeaders({
       'Content-Type': 'application/json',
     }),
     withCredentials: true
    };

    return this.http.post(`${environment.base_url}api/feedback`, feedback, httpOptions);
 }

 get name() {
  return this.feedbackGroup.get('name');
}

get email() {
  return this.feedbackGroup.get('email');
}


get message() {
  return this.feedbackGroup.get('message');
}
}
