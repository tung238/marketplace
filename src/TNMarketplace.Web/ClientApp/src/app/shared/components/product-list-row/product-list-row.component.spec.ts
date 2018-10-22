import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductListRowComponent } from './product-list-row.component';

describe('ProductListRowComponent', () => {
  let component: ProductListRowComponent;
  let fixture: ComponentFixture<ProductListRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductListRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductListRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
