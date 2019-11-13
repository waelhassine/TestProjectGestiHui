import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTriturationComponent } from './edit-trituration.component';

describe('EditTriturationComponent', () => {
  let component: EditTriturationComponent;
  let fixture: ComponentFixture<EditTriturationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditTriturationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditTriturationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
