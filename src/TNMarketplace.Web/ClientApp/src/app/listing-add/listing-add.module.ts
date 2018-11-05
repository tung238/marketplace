import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListingAddComponent } from './listing-add/listing-add.component';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { SharedModule } from '@app/shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdModule,
    SharedModule,
    RouterModule.forChild([
      { path: '', component: ListingAddComponent, pathMatch: 'full', data: { state: 'create' } }
  ])
  ],
  declarations: [ListingAddComponent]
})
export class ListingAddModule { }
