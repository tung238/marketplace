import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryModule } from './category/category.module';
import { RegionModule } from './region/region.module';
import { AreaModule } from './area/area.module';
import { UserModule } from './user/user.module';
import { RoleModule } from './role/role.module';
import { EmailTemplateModule } from './email-template/email-template.module';
import { MetaFieldModule } from './meta-field/meta-field.module';
import { SettingModule } from './setting/setting.module';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    CategoryModule,
    RegionModule,
    AreaModule,
    UserModule,
    RoleModule,
    EmailTemplateModule,
    MetaFieldModule,
    SettingModule,
    RouterModule.forChild([
      { path: '', loadChildren: './dashboard/dashboard.module#DashboardModule' },
      { path: 'category', loadChildren: './category/category.module#CategoryModule' }

    ])
  ],
  declarations: [
  ]
})
export class AdminModule { }
