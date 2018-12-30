import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

import { AccountService, DataService } from '@app/core';
import { AppService } from '../../app.service';
import { ProfileService } from '@app/account/+profile/profile.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'appc-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    public isCollapsed = true;
    selectedRegions: any[] = [];
    selectedCategories: any[] = [];
    searchText: string;
    categories = [];
    regions = [];
    constructor(
        private accountService: AccountService,
        private dataService: DataService,
        private toastr: ToastrService,

        private appService: AppService,
        public oAuthService: OAuthService,
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
        this.appService.getAppData().then(data => {
            this.regions = data.regionsTree;
            this.categories = data.categoriesTree;
        });
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
        console.log(this.selectedRegions);
        console.log(this.selectedCategories);
        if (!this.searchText){
            this.searchText = "";
        }
        var category = null;
        var subcategory = null;
        var region = null;
        var area = null;
        if (this.selectedCategories.length > 0) {
            category = this.selectedCategories[0];
        }
        if (category){
            if (this.selectedCategories.length > 1){
                subcategory = this.selectedCategories[1];
            }
        }
        if (this.selectedRegions.length > 0) {
            region = this.selectedRegions[0];
        }
        if (region){
            if (this.selectedRegions.length > 1){
               area = this.selectedRegions[1];
            }
        }

        var query = ""
        if (subcategory && subcategory.id > 0) {
            query = `/${category.slug}/${subcategory.slug}`
        } else if (category){
            query = `/${category.slug}`
        }
        if (area && area.id > 0){
            query = `/${region.slug}/${area.slug}${query}`;
        }else if (region){
            query = `/${region.slug}/${query}`;
        }
        this.router.navigate([query], { 
            queryParams: {s: `${this.searchText}`}});
    }

    onRegionsSelectionChange(event){
        this.selectedRegions = event;
    }
    onCategoriesSelectionChange(event){
        this.selectedCategories = event;
    }
}
