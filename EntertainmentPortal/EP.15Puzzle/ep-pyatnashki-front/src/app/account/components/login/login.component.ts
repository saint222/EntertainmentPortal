import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

// tslint:disable-next-line: ban-types
  public response: Object;

  constructor(private http: HttpClient, private accountService: AccountService) {

  }
  ngOnInit() {



    this.http.get('https://localhost:44380/api/auth/test', { headers: {Authorization: this.accountService.getAuthorizationHeaderValue()} })
      .subscribe(response => this.response = response);
  }



}
