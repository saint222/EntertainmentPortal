import { AuthService } from './../../api/auth.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerGroup: FormGroup;

  constructor(private service: RegisterService, private fb: FormBuilder, private route: ActivatedRoute) {


  }

  ngOnInit() {
  }

}
