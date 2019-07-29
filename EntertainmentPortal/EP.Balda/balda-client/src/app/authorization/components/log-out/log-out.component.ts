import { Router } from '@angular/router';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-log-out',
  templateUrl: './log-out.component.html',
  styleUrls: ['./log-out.component.sass']
})
export class LogOutComponent implements OnInit {

  constructor(private authService: AuthService, private cookieService: CookieService, private router: Router) { }

  ngOnInit() {
    this.authService.logOut().subscribe(p => p);
    this.cookieService.deleteAll('/');
    setTimeout(() => {
      this.router.navigateByUrl('');
  }, 5000);
  }
}
