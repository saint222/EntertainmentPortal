import { Component} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PlayersService } from '../../api/players.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-player-form',
  templateUrl: './player-form.component.html',
  styleUrls: ['./player-form.component.scss']
})
export class PlayerFormComponent {
  playerGroup: FormGroup;
  error: string;

  constructor(private playerService: PlayersService, private fb: FormBuilder, private router: Router) {
    this.playerGroup = this.fb.group({
      nickName: ['', [Validators.required, Validators.minLength(1)]]
      });
    }
  ngOnInit() {
  }
  /*onSubmit(form: FormGroup) {

    const body = {
      nickName: form.value.nickName
    };
    this.playerService.playersCreatePlayer(body).subscribe(c => {
        console.log(c);
        this.router.navigate(['/registered/player', c.id]);
      },
      (err: HttpErrorResponse) => {
        this.error = err.error[0].description;
        console.log(this.error);
        return console.log(err.error);
      }
      );
  }*/

  onSubmit(form: FormGroup) {
    this.playerService.playersCreatePlayer(form.value).subscribe(c => {
      console.log(c);
      this.router.navigate(['/registered/player', c.id]);
    },
    (err: HttpErrorResponse) => {
      this.error = err.error[0].description;
      console.log(this.error);
      return console.log(err.error);
    }
    );
  }
}
