import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllGrignonsComponent } from './all-grignons.component';

describe('AllGrignonsComponent', () => {
  let component: AllGrignonsComponent;
  let fixture: ComponentFixture<AllGrignonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllGrignonsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllGrignonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
