import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Cell } from '../models/cell';

@Injectable({
  providedIn: 'root'
})
export class CellService {
  private loggedBeghavior: Subject<string> = new Subject<string>();

  constructor(private http: HttpClient) {}

  getCells() {
    return this.http.get<Cell[]>('https://localhost:5001/api/map');
  }
}
