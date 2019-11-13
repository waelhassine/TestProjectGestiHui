import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GrignonsStatistiqueComponent } from './grignons-statistique.component';

describe('GrignonsStatistiqueComponent', () => {
  let component: GrignonsStatistiqueComponent;
  let fixture: ComponentFixture<GrignonsStatistiqueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GrignonsStatistiqueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GrignonsStatistiqueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
