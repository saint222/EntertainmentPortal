import {EventEmitter, Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class ShareService {
  onMapClick: EventEmitter<number> = new EventEmitter<number>();
  private clickSize = 0;

  public doMapClick(sz: number) {
    this.clickSize = sz;
    this.onMapClick.emit(this.clickSize);
  }

}
