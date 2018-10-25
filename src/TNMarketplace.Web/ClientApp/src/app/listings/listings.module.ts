import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListingListComponent } from './listing-list/listing-list.component';
import { ListingGridComponent } from './listing-grid/listing-grid.component';
import { ListingsComponent } from './listings/listings.component';
import { RouterModule } from '../../../node_modules/@angular/router';
import { NgZorroAntdModule } from '../../../node_modules/ng-zorro-antd';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    NgZorroAntdModule,
    RouterModule.forChild([
      { path: '', component: ListingsComponent, pathMatch: 'full', data: { state: 'create' } }
  ])
  ],
  declarations: [ListingListComponent, ListingGridComponent, ListingsComponent]
})
export class ListingsModule { }
