import { ActivatedRoute, Router } from '@angular/router';
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

  constructor(private fb: FormBuilder, private sessionService: SessionsService, private router: Router, private route: ActivatedRoute) {
    this.sessionGroup = this.fb.group({
      level: ['Easy', Validators.required]
    });
  }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {
    this.sessionService.sessionsCreateSession(form.value).subscribe(c => {
        console.log(c);
        this.router.navigate(['/registered/session', c.Id]);
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }
}
