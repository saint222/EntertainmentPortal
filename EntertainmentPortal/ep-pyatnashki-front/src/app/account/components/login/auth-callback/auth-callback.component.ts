import { DeckService } from './../../../../pyatnashki/services/deck.service';
import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/services/account.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})
export class AuthCallbackComponent implements OnInit {
  error: boolean;
  // tslint:disable-next-line: max-line-length
  constructor( private accountService: AccountService, private deckService: DeckService, private router: Router, private route: ActivatedRoute) { }

  async ngOnInit() {
    if (this.route.snapshot.fragment.indexOf('error') >= 0) {
      this.error = true;
    }
    if (!this.error) {
      await this.accountService.completeAuthentication();
      this.deckService.getDeck().subscribe(d => {
        sessionStorage.setItem('deck', JSON.stringify(d));
      });
      await new Promise(resolve => setTimeout(resolve, 1000));
    }

    this.router.navigate(['/deck']);
  }

}
