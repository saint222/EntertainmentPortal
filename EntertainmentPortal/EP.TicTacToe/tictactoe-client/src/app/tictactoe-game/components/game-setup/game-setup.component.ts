import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

@Component({
  selector: 'app-game-setup',
  templateUrl: './game-setup.component.html',
  styleUrls: ['./game-setup.component.css']
})
export class GameSetupComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<GameSetupComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) {

  }

  ngOnInit(): void {
  }

  save() {
    this.dialogRef.close('SAVED');
  }
}
