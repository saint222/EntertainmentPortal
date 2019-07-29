import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {ShareService} from '../../../core/services/share.service';
import {Player} from '../../Models/player';

export interface GameMap {
  value: number;
  viewValue: string;
}

export interface WinnerChain {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-game-setup',
  templateUrl: './game-setup.component.html',
  styleUrls: ['./game-setup.component.css']
})
export class GameSetupComponent implements OnInit {
  selectedMap: number;
  selectedOpponent: string;
  selectedChain: number;

  opponents: Player[] = [
    {userName: 'Alice', userId: '1', userEmail: ''},
    {userName: 'Bob', userId: '2', userEmail: ''},
    {userName: 'Alex', userId: '3', userEmail: ''},
    {userName: 'Nick', userId: '4', userEmail: ''},
  ];
  maps: GameMap[] = [
    {value: 3, viewValue: '3 x 3'},
    {value: 4, viewValue: '4 x 4'},
    {value: 5, viewValue: '5 x 5'},
    {value: 6, viewValue: '6 x 6'},
    {value: 7, viewValue: '7 x 7'},
    {value: 8, viewValue: '8 x 8'},
    {value: 9, viewValue: '9 x 9'},
    {value: 10, viewValue: '10 x 10'}
  ];
  chains: WinnerChain[] = [
    {value: 2, viewValue: '* *'},
    {value: 3, viewValue: '* * *'},
    {value: 4, viewValue: '* * * *'},
    {value: 5, viewValue: '* * * * *'},
    {value: 6, viewValue: '* * * * * *'}
  ];


  constructor(public dialogRef: MatDialogRef<GameSetupComponent>,
              private share: ShareService,
              @Inject(MAT_DIALOG_DATA) public data: any) {

  }

  ngOnInit(): void {
  }

  newSender() {
    this.share.changeMapSize(this.selectedMap);
  }

  save() {
    this.newSender();
    this.dialogRef.close('SAVED');
  }
}
