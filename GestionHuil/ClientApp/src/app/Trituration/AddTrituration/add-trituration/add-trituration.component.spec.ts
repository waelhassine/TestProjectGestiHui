import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTriturationComponent } from './add-trituration.component';

describe('AddTriturationComponent', () => {
  let component: AddTriturationComponent;
  let fixture: ComponentFixture<AddTriturationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddTriturationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTriturationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
