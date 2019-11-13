import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllFactureTriturationComponent } from './all-facture-trituration.component';

describe('AllFactureTriturationComponent', () => {
  let component: AllFactureTriturationComponent;
  let fixture: ComponentFixture<AllFactureTriturationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllFactureTriturationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllFactureTriturationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
