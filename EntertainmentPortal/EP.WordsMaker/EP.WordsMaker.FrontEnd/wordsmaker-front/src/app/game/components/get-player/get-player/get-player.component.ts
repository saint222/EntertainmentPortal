import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Player } from 'src/app/game/models/player';
import { PlayerService } from 'src/app/game/services/player.service';

@Component({
  selector: 'app-get-player',
  templateUrl: './get-player.component.html',
  styleUrls: ['./get-player.component.scss']
})
export class GetPlayerComponent implements OnInit {

  playerGroup: FormGroup;
  input_class: 'something';

  constructor(private playerService: PlayerService, private fb: FormBuilder) {
    this.playerGroup = this.fb.group({
      id: ['', Validators.required]
    })
  }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {
    this.playerService.getPlayer(form.value).subscribe(s => console.log(s))
  }
}
