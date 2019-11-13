import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVenteHuileComponent } from './add-vente-huile.component';

describe('AddVenteHuileComponent', () => {
  let component: AddVenteHuileComponent;
  let fixture: ComponentFixture<AddVenteHuileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddVenteHuileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddVenteHuileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
