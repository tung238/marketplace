import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MetaFieldListComponent } from './meta-field-list/meta-field-list.component';
import { MetaFieldDetailComponent } from './meta-field-detail/meta-field-detail.component';
import { RouterModule } from '@angular/router';
import { ApiModule } from '@app/api';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      { path: '', component: MetaFieldListComponent },
      { path: 'tao-moi', component: MetaFieldDetailComponent },
      { path: 'sua/:id', component: MetaFieldDetailComponent }

    ])
  ],
  declarations: [MetaFieldListComponent, MetaFieldDetailComponent]
})
export class MetaFieldModule { }
