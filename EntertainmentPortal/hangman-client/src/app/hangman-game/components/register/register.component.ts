import { GameService } from './../../services/game.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass']
})
export class RegisterComponent implements OnInit {
  userDataGroup: FormGroup;

  constructor(private fb: FormBuilder, private gameService: GameService) {
    this.userDataGroup = this.fb.group({
      username: ['', [Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
   }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {
    console.log(form.value);
    this.gameService.registerUser(form.value).subscribe(b => {
      console.log(b);
    },
      (err: HttpResponseBase) => console.log(err.statusText));
  }



}
