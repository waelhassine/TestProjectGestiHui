import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TriturationStatistiqueComponent } from './trituration-statistique.component';

describe('TriturationStatistiqueComponent', () => {
  let component: TriturationStatistiqueComponent;
  let fixture: ComponentFixture<TriturationStatistiqueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TriturationStatistiqueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TriturationStatistiqueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
