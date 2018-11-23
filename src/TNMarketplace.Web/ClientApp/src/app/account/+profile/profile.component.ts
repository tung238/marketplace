import { Component } from '@angular/core';

import { ProfileService } from './profile.service';

@Component({
    selector: 'appc-profile',
    templateUrl: './profile.component.html'
})
export class ProfileComponent {
    menus = [
        { route: 'userinfo', text: 'Thông tin' },
        { route: 'updatepassword', text: 'Đổi mật khẩu' },
        { route: 'tin-dang', text: 'Tin đăng của bạn' },
        // { route: 'userphoto', text: 'User photo' },
        // { route: 'otheraccounts', text: 'Tài khoản liên kết' }
    ];

    constructor(public profileService: ProfileService) { }
}
