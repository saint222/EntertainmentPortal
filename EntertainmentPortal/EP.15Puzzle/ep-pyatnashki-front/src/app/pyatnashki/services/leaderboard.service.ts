import { environment } from 'src/environments/environment.prod';
import { Champion } from './../models/champion';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AccountService } from 'src/app/account/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  private url = `${environment.api_url}/api/record`;
  constructor(private http: HttpClient, private accountService: AccountService) { }

  getRecordsWithEmail() {
    return this.http.get<Champion[]>(this.url, {headers: new HttpHeaders ({
      'Email': this.accountService.getEmailnHeaderValue()
    }) , withCredentials: true });
  }
  getRecords() {
    return this.http.get<Champion[]>(this.url, {withCredentials: true });
  }
}
