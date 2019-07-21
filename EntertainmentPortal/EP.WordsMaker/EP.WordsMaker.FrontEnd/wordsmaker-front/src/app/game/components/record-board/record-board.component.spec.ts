import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordBoardComponent } from './record-board.component';

describe('RecordBoardComponent', () => {
  let component: RecordBoardComponent;
  let fixture: ComponentFixture<RecordBoardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecordBoardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecordBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
