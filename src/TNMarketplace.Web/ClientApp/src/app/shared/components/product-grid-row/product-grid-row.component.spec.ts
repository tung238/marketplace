import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductGridRowComponent } from './product-grid-row.component';

describe('ProductGridRowComponent', () => {
  let component: ProductGridRowComponent;
  let fixture: ComponentFixture<ProductGridRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductGridRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductGridRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
