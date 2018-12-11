import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { RouterModule } from '@angular/router';
import { ApiModule } from '@app/api';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      { path: '', component: CategoryListComponent },
      { path: 'tao-moi', component: CategoryDetailComponent },
      { path: 'sua/:id', component: CategoryDetailComponent }

    ])
  ],
  declarations: [CategoryListComponent, CategoryDetailComponent]
})
export class CategoryModule { }
