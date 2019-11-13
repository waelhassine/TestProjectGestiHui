import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AchatStatistiqueComponent } from './achat-statistique.component';

describe('AchatStatistiqueComponent', () => {
  let component: AchatStatistiqueComponent;
  let fixture: ComponentFixture<AchatStatistiqueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AchatStatistiqueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AchatStatistiqueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
