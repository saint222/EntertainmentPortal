import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShipplacerComponent } from './shipplacer.component';

describe('ShipplacerComponent', () => {
  let component: ShipplacerComponent;
  let fixture: ComponentFixture<ShipplacerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShipplacerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShipplacerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
