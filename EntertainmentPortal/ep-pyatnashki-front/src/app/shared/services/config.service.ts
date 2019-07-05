import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {


// tslint:disable-next-line: max-line-length
  private jwt: string;
  constructor() { }

  setJWT(j: string) {
    this.jwt = j;
  }
  getJWT() {
    return this.jwt;
  }
}
