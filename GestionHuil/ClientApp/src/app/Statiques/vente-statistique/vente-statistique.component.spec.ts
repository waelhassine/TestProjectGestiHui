import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VenteStatistiqueComponent } from './vente-statistique.component';

describe('VenteStatistiqueComponent', () => {
  let component: VenteStatistiqueComponent;
  let fixture: ComponentFixture<VenteStatistiqueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VenteStatistiqueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VenteStatistiqueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
