import {EventEmitter, Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ShareService {
  onMapClick: EventEmitter<number> = new EventEmitter<number>();
  private clicker = 0;

  private messageSource = new BehaviorSubject(0);
  currentMessage = this.messageSource.asObservable();

  constructor() {
  }

  public doClick(n: number) {
    this.clicker = n;
    this.onMapClick.emit(this.clicker);
  }

  changeMapSize(message: number) {
    this.messageSource.next(message);
  }

}
