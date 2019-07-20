import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.sass']
})
export class HeaderComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }



  loginUser() {
    this.authService.loginUser();
  }

  logoutUser() {
    this.authService.logoutUser();
  }

  home() {
    this.router.navigateByUrl('/');
  }
}
