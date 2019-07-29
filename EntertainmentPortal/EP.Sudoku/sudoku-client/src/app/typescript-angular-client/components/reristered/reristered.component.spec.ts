import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReristeredComponent } from './reristered.component';

describe('ReristeredComponent', () => {
  let component: ReristeredComponent;
  let fixture: ComponentFixture<ReristeredComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReristeredComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReristeredComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
