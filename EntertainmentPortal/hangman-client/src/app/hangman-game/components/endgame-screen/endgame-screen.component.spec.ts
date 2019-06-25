import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EndgameScreenComponent } from './endgame-screen.component';

describe('EndgameScreenComponent', () => {
  let component: EndgameScreenComponent;
  let fixture: ComponentFixture<EndgameScreenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EndgameScreenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EndgameScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
