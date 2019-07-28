import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpResponseBase, HttpErrorResponse, HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

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
      userName: [''],
      password: ['']
    });
  }

  ngOnInit() {}

  get user() { return this.loginGroup.controls; }

  onSubmit(form: FormGroup) {
    this.authService.login(form.value).subscribe(p => {
      console.log(p);
      sessionStorage.setItem('id', p.id);
      this.router.navigateByUrl('welcome');
    },
    (err: HttpErrorResponse) => {
      this.errorText = err.error;
      return console.log(err.statusText);
    });
  }

  onFacebookClick() {

  }

  onGoogleClick() {
    this.authService.googleSignIn().subscribe(g => g);
  }

  onSignIn(googleUser) {
    const profile = googleUser.getBasicProfile();
    console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
    console.log('Name: ' + profile.getName());
    console.log('Image URL: ' + profile.getImageUrl());
    console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.
  }
}


