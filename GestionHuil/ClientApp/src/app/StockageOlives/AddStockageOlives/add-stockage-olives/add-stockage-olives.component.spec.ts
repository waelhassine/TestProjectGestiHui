import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddStockageOlivesComponent } from './add-stockage-olives.component';

describe('AddStockageOlivesComponent', () => {
  let component: AddStockageOlivesComponent;
  let fixture: ComponentFixture<AddStockageOlivesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddStockageOlivesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddStockageOlivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
