import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../api/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private accountService: AccountService) {}

  addUser (){
    this.accountService.AddUser;
  }
  ngOnInit() {
  }

}
