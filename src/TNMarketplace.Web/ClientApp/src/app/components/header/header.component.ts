import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

import { AccountService, DataService } from '@app/core';
import { AppService } from '../../app.service';
import { ProfileService } from '@app/account/+profile/profile.service';
import { UserInfoModel } from '@app/account/+profile/profile.models';

@Component({
    selector: 'appc-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    public isCollapsed = true;
    public userInfo: UserInfoModel;
    public userName: string = "";
    constructor(
        private accountService: AccountService,
        private dataService: DataService,
        private appService: AppService,
        private oAuthService: OAuthService,
        private profileService: ProfileService,
        private router: Router
    ) { }

    public get isLoggedIn(): boolean {
        return this.accountService.isLoggedIn;
    }
    public get user(): IProfileModel | undefined {
        return this.accountService.user;

    }
    public get cultures(): ICulture[] {
        return this.appService.appData.cultures;
    }
    public get currentCulture(): ICulture {
        return this.cultures.filter(x => x.current)[0];
    }
    public ngOnInit(): void {
        this.profileService.userInfo().subscribe(info=>{
            this.userInfo = info;
            this.userName = `${info.firstName} ${info.lastName}`;
        })
     }

    public toggleNav() {
        this.isCollapsed = !this.isCollapsed;
        this.appService.annouceSideMenuState(this.isCollapsed);
    }

    public logout() {
        this.dataService.post('api/account/logout').subscribe(() => {
            this.oAuthService.logOut();
            this.router.navigate(['/dang-nhap']);
        });
    }
}
