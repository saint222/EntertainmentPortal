import { Player } from './../../model/player';
import { Component, OnInit} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PlayersService } from '../../api/players.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-player-form',
  templateUrl: './player-form.component.html',
  styleUrls: ['./player-form.component.scss']
})
export class PlayerFormComponent implements OnInit {
  playerGroup: FormGroup;
  error: string;

  constructor(private playerService: PlayersService, private fb: FormBuilder, private router: Router) {
    this.playerGroup = this.fb.group({
      nickName: ['', [Validators.required, Validators.minLength(1)]],
      iconId: ['1', Validators.required]
      });
    }

  ngOnInit() {
    this.playerService.playersGetPlayerByUserId().subscribe(c => {
      console.log(c);
      this.router.navigate(['/registered/player']);
    },
    (err: HttpErrorResponse) => {
      console.log(err);
    }
    );
  }

  onSubmit(form: FormGroup) {
    this.playerService.playersCreatePlayer(form.value).subscribe(c => {
      console.log(c);
      this.router.navigate(['/registered/player', c.id]);
    },
    (err: HttpErrorResponse) => {
      console.log(err);
    }
    );
  }
}
