import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditGrignonsComponent } from './edit-grignons.component';

describe('EditGrignonsComponent', () => {
  let component: EditGrignonsComponent;
  let fixture: ComponentFixture<EditGrignonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditGrignonsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditGrignonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
