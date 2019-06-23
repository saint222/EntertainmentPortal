import { Cell } from './../models/cell';
import { Session } from './../models/session';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private http: HttpClient) { }

  getSessionById(sessionId: number) {
    return this.http.get<Session>('http://localhost:58857/api/sessions/' + sessionId);
  }

  updateCell(cell: Cell) {
    return this.http.put<Cell>('http://localhost:58857/api/setCellValue/', cell);
  }
}
