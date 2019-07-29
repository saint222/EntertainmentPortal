import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  loginGroup: FormGroup;
  inputClass: 'something';
  errorText: string;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private http: HttpClient) {
    this.loginGroup = this.fb.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  ngOnInit() {
  }

  get user() { return this.loginGroup.controls; }

  onSubmit(form: FormGroup) {
    this.authService.login(form.value).subscribe(p => {
      console.log(p);
      this.router.navigateByUrl('welcome');
    },
    (err: HttpErrorResponse) => {
      this.errorText = err.error;
      return console.log(err.error[0]);
    });
  }

  onFacebookClick() {

  }

  onGoogleClick() {
  }

  get userName() {
    return this.loginGroup.get('userName');
  }

  get password() {
    return this.loginGroup.get('password');
  }
}


