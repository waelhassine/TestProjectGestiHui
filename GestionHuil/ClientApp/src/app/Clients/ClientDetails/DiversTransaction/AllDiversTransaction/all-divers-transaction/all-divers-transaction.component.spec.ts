import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllDiversTransactionComponent } from './all-divers-transaction.component';

describe('AllDiversTransactionComponent', () => {
  let component: AllDiversTransactionComponent;
  let fixture: ComponentFixture<AllDiversTransactionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllDiversTransactionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllDiversTransactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
