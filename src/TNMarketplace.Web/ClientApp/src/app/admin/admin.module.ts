import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegionModule } from './region/region.module';
import { AreaModule } from './area/area.module';
import { UserModule } from './user/user.module';
import { RoleModule } from './role/role.module';
import { EmailTemplateModule } from './email-template/email-template.module';
import { SettingModule } from './setting/setting.module';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RegionModule,
    AreaModule,
    UserModule,
    RoleModule,
    EmailTemplateModule,
    SettingModule,
    RouterModule.forChild([
      { path: '', loadChildren: './dashboard/dashboard.module#DashboardModule' },
      { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
      { path: 'danh-muc', loadChildren: './category/category.module#CategoryModule' },
      { path: 'thuoc-tinh', loadChildren: './meta-field/meta-field.module#MetaFieldModule' }

    ])
  ],
  declarations: [
  ]
})
export class AdminModule { }
