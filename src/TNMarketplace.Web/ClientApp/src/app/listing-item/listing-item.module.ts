import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListingItemComponent } from './listing-item/listing-item.component';
import { RouterModule } from '../../../node_modules/@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: ListingItemComponent, pathMatch: 'full', data: { state: 'create' } }
  ])
  ],
  declarations: [ListingItemComponent]
})
export class ListingItemModule { }