import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStockageOlivesComponent } from './edit-stockage-olives.component';

describe('EditStockageOlivesComponent', () => {
  let component: EditStockageOlivesComponent;
  let fixture: ComponentFixture<EditStockageOlivesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditStockageOlivesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditStockageOlivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
