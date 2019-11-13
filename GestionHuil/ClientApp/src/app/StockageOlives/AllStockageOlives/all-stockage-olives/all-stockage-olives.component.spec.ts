import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllStockageOlivesComponent } from './all-stockage-olives.component';

describe('AllStockageOlivesComponent', () => {
  let component: AllStockageOlivesComponent;
  let fixture: ComponentFixture<AllStockageOlivesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllStockageOlivesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllStockageOlivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
