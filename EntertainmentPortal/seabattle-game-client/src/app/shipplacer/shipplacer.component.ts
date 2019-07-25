import { ShipOrientation } from './../models/shipOrientation';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { BattlefieldService } from '../battlefield/services/battlefield.service';

@Component({
  selector: 'app-shipplacer',
  templateUrl: './shipplacer.component.html',
  styleUrls: ['./shipplacer.component.scss']
})
export class ShipplacerComponent implements OnInit {
  placerForm: FormGroup;
  constructor(private fb: FormBuilder, public battleFieldService: BattlefieldService) {
  }

  ngOnInit() {
    this.placerForm = this.fb.group({
      x: new FormControl(0, [Validators.required, Validators.min(0), Validators.max(9), Validators.maxLength(1)]),
      y: new FormControl(0, [Validators.required, Validators.min(0), Validators.max(9), Validators.maxLength(1)]),
      rank: new FormControl('', [Validators.required, Validators.min(1), Validators.max(4)]),
      orientation: new FormControl('', [Validators.required, Validators.min(0), Validators.max(1)])
      });
  }

  onSubmit(form: FormGroup) {
    form.value.playerId = '1';
    form.value.gameId = '1';
    this.battleFieldService.addShip(form);
  }
}
