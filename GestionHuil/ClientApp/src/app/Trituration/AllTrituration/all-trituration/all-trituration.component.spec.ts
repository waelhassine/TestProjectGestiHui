import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllTriturationComponent } from './all-trituration.component';

describe('AllTriturationComponent', () => {
  let component: AllTriturationComponent;
  let fixture: ComponentFixture<AllTriturationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllTriturationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllTriturationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
