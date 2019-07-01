import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LooseGameComponent } from './loose-game.component';

describe('LooseGameComponent', () => {
  let component: LooseGameComponent;
  let fixture: ComponentFixture<LooseGameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LooseGameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LooseGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
