import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListingItemComponent } from './listing-item/listing-item.component';
import { RouterModule } from '../../../node_modules/@angular/router';
import { ListingService } from '../api';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdModule,
    SharedModule,
    RouterModule.forChild([
      { path: '', component: ListingItemComponent}
  ])
  ],
  declarations: [ListingItemComponent],
  providers:[
    ListingService
  ]
})
export class ListingItemModule { }
