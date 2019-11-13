import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDiversTransactionComponent } from './edit-divers-transaction.component';

describe('EditDiversTransactionComponent', () => {
  let component: EditDiversTransactionComponent;
  let fixture: ComponentFixture<EditDiversTransactionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditDiversTransactionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditDiversTransactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
