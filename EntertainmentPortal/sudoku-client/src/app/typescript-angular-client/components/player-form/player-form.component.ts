import { Player } from './../../model/player';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PlayersService } from '../../api/players.service';

@Component({
  selector: 'app-player-form',
  templateUrl: './player-form.component.html',
  styleUrls: ['./player-form.component.scss']
})
export class PlayerFormComponent implements OnInit {
  playerGroup: FormGroup;
  player: Player;

  constructor(private playerService: PlayersService, private fb: FormBuilder, private router: Router) {
    this.playerGroup = this.fb.group({
      nickName: ['', [Validators.required, Validators.minLength(1)]]
      });
    }

  ngOnInit() {
  }
  onSubmit(form: FormGroup) {
    this.playerService.playersCreatePlayer(form.value).subscribe(c => {
        console.log(c);
        this.router.navigate(['/registered/player', c.id]);
      },
      );
  }
}
