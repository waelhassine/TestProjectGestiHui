import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllAchatComponent } from './all-achat.component';

describe('AllAchatComponent', () => {
  let component: AllAchatComponent;
  let fixture: ComponentFixture<AllAchatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllAchatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllAchatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
