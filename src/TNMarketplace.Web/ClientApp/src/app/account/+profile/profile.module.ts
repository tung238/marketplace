import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProfileComponent } from './profile.component';
import { ProfileService } from './profile.service';
import { UserInfoComponent } from './user-info/user-info.component';
import { UpdatePasswordComponent } from './update-password/update-password.component';
import { UserPhotoComponent } from './user-photo/user-photo.component';
import { OtherAccountsComponent } from './other-accounts/other-accounts.component';
import { SharedModule } from '../../shared/shared.module';
import { MyListingComponent } from './my-listing/my-listing.component';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { AuthGuard } from '@app/auth.guard';

@NgModule({
    imports: [
        SharedModule,
        NgZorroAntdModule,
        RouterModule.forChild([
            {
                path: '', component: ProfileComponent, children: [
                    { path: '', redirectTo: 'userinfo' },
                    { path: 'userinfo', component: UserInfoComponent, canActivate: [AuthGuard] },
                    { path: 'updatepassword', component: UpdatePasswordComponent, canActivate: [AuthGuard] },
                    { path: 'userphoto', component: UserPhotoComponent, canActivate: [AuthGuard] },
                    { path: 'otheraccounts', component: OtherAccountsComponent, canActivate: [AuthGuard] },
                    { path: 'tin-dang', component: MyListingComponent, canActivate: [AuthGuard] },
                    { path: 'tin-dang/:id', loadChildren: '../../listing-add/listing-add.module#ListingAddModule', canActivate: [AuthGuard] },

                ]
            },
        ])
    ],
    declarations: [
        ProfileComponent,
        UserInfoComponent,
        UpdatePasswordComponent,
        UserPhotoComponent,
        OtherAccountsComponent,
        MyListingComponent
    ],
    providers: [ProfileService]
})
export class ProfileModule { }
