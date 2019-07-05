import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  userInfoGroup: FormGroup;


  constructor(private accountService: AccountService, private fb: FormBuilder) {
    this.userInfoGroup = this.fb.group({
      email: ['', Validators.required ],
      password: ['', Validators.required ]
    });
  }
  ngOnInit() {
  }

  launchFbLogin() {
    this.accountService.loginFacebook().subscribe(jwt => localStorage.setItem('jwt', JSON.stringify(jwt)));
  }

  launchGoogleLogin() {
    this.accountService.loginGoogle().subscribe(jwt => localStorage.setItem('jwt', JSON.stringify(jwt)));
  }

  onSubmit(form: FormGroup) {
    this.accountService.loginBearer(form.value).subscribe(jwt => localStorage.setItem('jwt', JSON.stringify(jwt)));
  }



}
