import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WinGameComponent } from './win-game.component';

describe('WinGameComponent', () => {
  let component: WinGameComponent;
  let fixture: ComponentFixture<WinGameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WinGameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WinGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
