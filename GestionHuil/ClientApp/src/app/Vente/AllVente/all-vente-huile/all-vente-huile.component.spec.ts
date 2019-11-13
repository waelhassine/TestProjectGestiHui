import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllVenteHuileComponent } from './all-vente-huile.component';

describe('AllVenteHuileComponent', () => {
  let component: AllVenteHuileComponent;
  let fixture: ComponentFixture<AllVenteHuileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllVenteHuileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllVenteHuileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
