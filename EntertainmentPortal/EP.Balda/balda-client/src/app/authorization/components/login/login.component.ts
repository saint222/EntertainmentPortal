import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  loginGroup: FormGroup;
  inputClass: 'something';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginGroup = this.fb.group({
      userName: [''],
      password: ['']
    });
  }

  ngOnInit() {}

  get user() { return this.loginGroup.controls; }

  onSubmit(form: FormGroup) {
    this.authService.login(form.value).subscribe(
    user => {
      console.log(user),
      this.router.navigate(['/startGame']);
    } ,
    (err: HttpResponseBase) => {
      console.log(err.statusText);
    });
  }
}


