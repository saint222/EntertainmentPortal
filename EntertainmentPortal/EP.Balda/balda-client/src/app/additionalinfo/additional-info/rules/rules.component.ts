import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-rules',
  templateUrl: './rules.component.html',
  styleUrls: ['./rules.component.sass']
})
export class RulesComponent implements OnInit {
  image1: any = 'assets/pics/1.jpg';
  image2: any = 'assets/pics/2.jpg';
  image3: any = 'assets/pics/3.jpg';
  image4: any = 'assets/pics/4.jpg';

  constructor() { }

  ngOnInit() {
  }

}
