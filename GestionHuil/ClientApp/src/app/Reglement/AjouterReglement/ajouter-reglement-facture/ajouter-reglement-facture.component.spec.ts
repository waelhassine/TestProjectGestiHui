import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AjouterReglementFactureComponent } from './ajouter-reglement-facture.component';

describe('AjouterReglementFactureComponent', () => {
  let component: AjouterReglementFactureComponent;
  let fixture: ComponentFixture<AjouterReglementFactureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AjouterReglementFactureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AjouterReglementFactureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
