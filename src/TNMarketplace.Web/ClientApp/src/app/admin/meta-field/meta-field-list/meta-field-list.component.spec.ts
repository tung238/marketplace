import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MetaFieldListComponent } from './meta-field-list.component';

describe('MetaFieldListComponent', () => {
  let component: MetaFieldListComponent;
  let fixture: ComponentFixture<MetaFieldListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MetaFieldListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MetaFieldListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
