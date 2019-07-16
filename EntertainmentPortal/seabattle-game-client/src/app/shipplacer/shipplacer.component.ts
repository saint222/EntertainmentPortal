import { ShipOrientation } from './../models/shipOrientation';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BattlefieldService } from '../battlefield/services/battlefield.service';

@Component({
  selector: 'app-shipplacer',
  templateUrl: './shipplacer.component.html',
  styleUrls: ['./shipplacer.component.scss']
})
export class ShipplacerComponent implements OnInit {
  placerForm: FormGroup;
  oneLast: number;
  twoLast: number;
  threeLast: number;
  fourLast: number;
  constructor(private fb: FormBuilder, private battleFieldService: BattlefieldService) {
    this.placerForm = this.fb.group({
      x: [''],
      y: [''],
      rank: [''],
      orientation: ['']
      });
    this.oneLast = 4;
    this.twoLast = 3;
    this.threeLast = 2;
    this.fourLast = 1;
  }

  ngOnInit() {
  }

   onSubmit(form: FormGroup) {
    form.value.playerId = '1';
    form.value.gameId = '1';
    console.log(`X = ${form.value.x} Y = ${form.value.y} Rank = ${form.value.rank} Player = ${form.value.playerId}`);
    this.battleFieldService.addShip(form);
   }

}
