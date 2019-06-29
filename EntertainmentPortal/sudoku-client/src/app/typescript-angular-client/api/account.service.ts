import { Injectable } from '@angular/core';
import { HttpClient } from 'selenium-webdriver/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  protected basePath = 'http://localhost:58857';
  public AddUser(){}

  constructor() { }
}
