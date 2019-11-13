import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllCaisseComponent } from './all-caisse.component';

describe('AllCaisseComponent', () => {
  let component: AllCaisseComponent;
  let fixture: ComponentFixture<AllCaisseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllCaisseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllCaisseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
