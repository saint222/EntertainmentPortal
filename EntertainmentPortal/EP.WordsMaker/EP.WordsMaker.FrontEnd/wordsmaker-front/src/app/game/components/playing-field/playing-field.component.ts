import { Component, OnInit } from '@angular/core';
import { log } from 'util';
interface Dictionary {
  [index: number]: string;
}

@Component({
  selector: 'app-playing-field',
  templateUrl: './playing-field.component.html',
  styleUrls: ['./playing-field.component.scss']
})

export class PlayingFieldComponent implements OnInit {

  public keyWord: string;
  public keyWordLetters: string[];
  public CuttedLetters: Dictionary;
  public resultWord: string[];

  public CLearBtnDisable: boolean;

  constructor() {
    this.keyWord =  'СОКОВЫЖИМАЛКА';
    this.keyWordLetters = this.keyWord.split('');
    this.CuttedLetters = {};
    this.resultWord = [];

  }
  CellClick(index: number) {
    log(index.toString());
    if (this.keyWordLetters[index] === '_') {
      //this.keyWordLetters[index] = this.CuttedLetters[index];
      //this.CuttedLetters[index] = null;
      //this.resultWord.pop();
    }
    else
    {

      this.CuttedLetters[index] = this.keyWordLetters[index];
      this.resultWord.push(this.keyWordLetters[index]);
      this.keyWordLetters[index] = '_';
    }

  }
  ClearBtnClick()
  {
    this.keyWordLetters = this.keyWord.split('');
    this.resultWord.length = 0;
    this.CuttedLetters = {};
  }
  SubmitBtnClick() {

  }

  CheckResultWord() {
    return this.resultWord.length > 0 ? false : true;
  }

  ngOnInit() {

  }

}
