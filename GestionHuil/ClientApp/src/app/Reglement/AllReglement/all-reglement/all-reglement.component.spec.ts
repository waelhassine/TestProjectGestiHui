import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllReglementComponent } from './all-reglement.component';

describe('AllReglementComponent', () => {
  let component: AllReglementComponent;
  let fixture: ComponentFixture<AllReglementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllReglementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllReglementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
