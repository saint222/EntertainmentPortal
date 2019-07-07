import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-userinfo',
  templateUrl: './userinfo.component.html',
  styleUrls: ['./userinfo.component.scss']
})
export class UserinfoComponent implements OnInit {
  userGroup: FormGroup;
  user: User;

  constructor(private fb: FormBuilder, private userService: UserService) {

    this.userService.getInfo().subscribe(r => {
      this.userGroup = this.fb.group({
        name: [r.name, [Validators.required, Validators.minLength(4)]],
        country: [r.country, Validators.required]
      });
    });
  }

  ngOnInit() {}

  onSubmit(form: FormGroup) {
    this.userService.sendInfo(form.value).subscribe(r => {this.user = r; });
  }

}
