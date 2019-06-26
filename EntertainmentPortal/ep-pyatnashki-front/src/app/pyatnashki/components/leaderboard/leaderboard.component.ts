import { Record } from './../../models/record';
import { LeaderboardService } from './../../services/leaderboard.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.scss']
})
export class LeaderboardComponent implements OnInit {

  records: Record[]
  constructor(private leaderboardService: LeaderboardService) { }

  ngOnInit() {
    this.leaderboardService.getRecords().subscribe(d => this.records = d);
  }
  getRecords() {
    this.leaderboardService.getRecords().subscribe(d => this.records = d);
  }

}
