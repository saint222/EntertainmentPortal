import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisteredDemoComponent } from './registered-demo.component';

describe('RegisteredDemoComponent', () => {
  let component: RegisteredDemoComponent;
  let fixture: ComponentFixture<RegisteredDemoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisteredDemoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisteredDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
