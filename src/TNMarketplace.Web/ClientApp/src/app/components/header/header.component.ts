import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

import { AccountService, DataService } from '@app/core';
import { AppService } from '../../app.service';
import { ProfileService } from '@app/account/+profile/profile.service';
import { UserInfoModel } from '@app/account/+profile/profile.models';
import { NotificationsService } from '@app/simple-notifications';

@Component({
    selector: 'appc-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    public isCollapsed = true;
    selectedCategory: any;
    searchText: string;
    categories = [];
    constructor(
        private accountService: AccountService,
        private dataService: DataService,
        private notificationService: NotificationsService,
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
        
        this.appService.getAppData().then(res => {
            // var categories = [{ slug: "tat-ca-danh-muc", name: "Tất cả danh mục" }];
            var categories = [];
            res.categoriesTree.forEach(c => {
                categories.push({ slug: c.slug, name: c.name });
            })
            this.categories = categories;
            this.selectedCategory = categories[0].slug;
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

    doSearch() {
        console.log(this.searchText);
        console.log(this.selectedCategory);
        if (!this.searchText){
            this.searchText = "";
        }
        if (!this.selectedCategory){
            this.notificationService.alert("Cảnh báo", "Bạn chưa chọn danh mục");
            return;
        }
        var category = null;
        var subcategory = null;
        if (this.selectedCategory == "tat-ca-danh-muc") {
            category = this.selectedCategory;
        } else {
            this.appService.appData.categoriesTree.forEach(c => {
                if (c.slug == this.selectedCategory && category == null) {
                    category = c.slug;
                }
                c.children.forEach(child => {
                    if (child.slug == this.selectedCategory && subcategory == null) {
                        category = c.slug;
                        subcategory = child.slug;
                    }
                });
            })
        }

        var query = ""
        if (subcategory) {
            query = `/${category}/${subcategory}`
        } else {
            query = `/${category}`
        }
        this.router.navigate([query], { 
            queryParams: {s: `${this.searchText}`}});
    }
}
