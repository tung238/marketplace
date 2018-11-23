import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListingAddComponent } from './listing-add/listing-add.component';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { SharedModule } from '@app/shared/shared.module';
import { RouterModule } from '@angular/router';
import { ListingService } from '@app/api';
import { AuthGuard } from '@app/auth.guard';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdModule,
    SharedModule,
    RouterModule.forChild([
      { path: '', component: ListingAddComponent}
  ])
  ],
  declarations: [ListingAddComponent],
  providers:[ListingService]
})
export class ListingAddModule { }
