import { HttpErrorResponse } from '@angular/common/http';
import { SessionsService } from './../../api/sessions.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-session',
  templateUrl: './create-session.component.html',
  styleUrls: ['./create-session.component.scss']
})
export class CreateSessionComponent implements OnInit {
  sessionGroup: FormGroup;

  constructor(private fb: FormBuilder, private sessionService: SessionsService) {
    this.sessionGroup = this.fb.group({
      level: ['0', Validators.required]
    });
  }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {
    console.log(form.value);
    this.sessionService.sessionsCreateSession(form.value).subscribe(
      c => console.log(c),
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }
}
