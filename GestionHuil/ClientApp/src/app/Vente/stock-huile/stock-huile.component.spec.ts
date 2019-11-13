import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StockHuileComponent } from './stock-huile.component';

describe('StockHuileComponent', () => {
  let component: StockHuileComponent;
  let fixture: ComponentFixture<StockHuileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StockHuileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockHuileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
