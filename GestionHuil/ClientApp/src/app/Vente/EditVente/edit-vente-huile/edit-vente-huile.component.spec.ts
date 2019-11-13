import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditVenteHuileComponent } from './edit-vente-huile.component';

describe('EditVenteHuileComponent', () => {
  let component: EditVenteHuileComponent;
  let fixture: ComponentFixture<EditVenteHuileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditVenteHuileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditVenteHuileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
