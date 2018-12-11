import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MetaFieldDetailComponent } from './meta-field-detail.component';

describe('MetaFieldDetailComponent', () => {
  let component: MetaFieldDetailComponent;
  let fixture: ComponentFixture<MetaFieldDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MetaFieldDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MetaFieldDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
