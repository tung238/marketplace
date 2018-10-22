import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductListComponent } from './product-list/product-list.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '../../../node_modules/@angular/router';
import { NgZorroAntdModule } from '../../../node_modules/ng-zorro-antd';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdModule,
    SharedModule,
    RouterModule.forChild([
        { path: '', component: ProductListComponent, pathMatch: 'full', data: {  } }
    ])
],
  declarations: [ProductDetailComponent, ProductListComponent]
})
export class ProductModule { }
