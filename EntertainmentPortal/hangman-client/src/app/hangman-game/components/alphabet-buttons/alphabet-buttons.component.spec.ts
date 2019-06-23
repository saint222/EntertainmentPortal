import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlphabetButtonsComponent } from './alphabet-buttons.component';

describe('AlphabetButtonsComponent', () => {
  let component: AlphabetButtonsComponent;
  let fixture: ComponentFixture<AlphabetButtonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlphabetButtonsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlphabetButtonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
