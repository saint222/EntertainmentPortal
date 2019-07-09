import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from './../../api/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerGroup: FormGroup;
  error: string;

  constructor(private authService: AuthService, private fb: FormBuilder, private router: Router) {

    this.registerGroup = this.fb.group({
      name: ['', Validators.required],
      email: ['', Validators.email],
      passwords: this.fb.group({
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required],
      }, {validator: this.comparePasswords })
    });


  }

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('confirmPassword');
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('password').value != confirmPswrdCtrl.value) {
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      } else {
        confirmPswrdCtrl.setErrors(null);
      }
    }
  }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {

    var body = {
      Name: form.value.name,
      Email: form.value.email,
      Password: form.value.passwords.password
    };

    this.authService.authRegister(body).subscribe(c => {
      console.log(c);
      this.router.navigate(['/registered']);
    },
    (err: HttpErrorResponse) => {
      this.error = err.error[0].description;
      console.log(this.error);
      return console.log(err.error);

    }
  );
  }

}
