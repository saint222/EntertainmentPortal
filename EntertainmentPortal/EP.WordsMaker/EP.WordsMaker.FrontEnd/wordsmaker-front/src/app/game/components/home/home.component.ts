import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../../services/player.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  playerGroup: FormGroup;
  input_class: 'something';

  constructor(private playerService: PlayerService, private fb: FormBuilder) {
    this.playerGroup = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {
    this.playerService.addPlayer(form.value).
    subscribe( r => console.log(r));
  }

}
