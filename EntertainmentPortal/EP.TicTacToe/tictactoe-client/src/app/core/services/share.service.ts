import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class ShareService {
  private gm: number;

  constructor(gm: number) {
    this.gm = gm;
  }

  get(): number {
    return this.gm;
  }

  set(gm: number) {
    this.gm = gm;
  }

}
