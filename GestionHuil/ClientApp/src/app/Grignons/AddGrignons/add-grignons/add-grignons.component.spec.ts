import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGrignonsComponent } from './add-grignons.component';

describe('AddGrignonsComponent', () => {
  let component: AddGrignonsComponent;
  let fixture: ComponentFixture<AddGrignonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddGrignonsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddGrignonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
