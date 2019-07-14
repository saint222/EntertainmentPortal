import { Router } from '@angular/router';
import { HttpResponseBase } from '@angular/common/http';
import { AuthService } from './../../services/auth.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.sass']
})
export class RegistrationComponent implements OnInit {
  registerGroup: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerGroup = this.fb.group({
      userName: [''],
      email: [''],
      password: [''],
      passwordConfirm: ['']
    });
   }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {
    console.log(form.value);
    this.authService.registerUser(form.value).subscribe(b => {
      console.log(b),
      this.router.navigate(['/startGame']);
    },
    (err: HttpResponseBase) => console.log(err.statusText));
  }

}
