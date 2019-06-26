import { Record } from './../models/record';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  private url = 'https://localhost:44380/api/record';
  constructor(private http: HttpClient) { }

  getRecords() {
    return this.http.get<Record[]>(this.url, { withCredentials: true });
  }
}
