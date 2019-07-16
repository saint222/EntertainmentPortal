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
  constructor(private fb: FormBuilder, public battleFieldService: BattlefieldService) {
    this.placerForm = this.fb.group({
      x: [''],
      y: [''],
      rank: [''],
      orientation: ['']
      });
  }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {
    form.value.playerId = '1';
    form.value.gameId = '1';
    this.battleFieldService.addShip(form);
  }
}
