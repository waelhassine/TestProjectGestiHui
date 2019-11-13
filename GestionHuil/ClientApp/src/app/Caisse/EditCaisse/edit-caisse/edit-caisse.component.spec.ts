import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCaisseComponent } from './edit-caisse.component';

describe('EditCaisseComponent', () => {
  let component: EditCaisseComponent;
  let fixture: ComponentFixture<EditCaisseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCaisseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCaisseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
