import { AccountService } from './../../../account/services/account.service';
import { Champion } from './../../models/champion';
import { LeaderboardService } from './../../services/leaderboard.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.scss']
})
export class LeaderboardComponent implements OnInit {

  records: Champion[] = [];
  constructor(private leaderboardService: LeaderboardService, private accountService: AccountService) { }

  ngOnInit() {
    if (this.accountService.IsLoggedIn()) {
      this.leaderboardService.getRecordsWithEmail().subscribe(d => this.records = d);
    } else {
      this.leaderboardService.getRecords().subscribe(d => this.records = d);
    }
  }
}
