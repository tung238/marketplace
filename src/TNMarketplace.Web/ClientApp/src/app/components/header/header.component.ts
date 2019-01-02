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
    selectedPriceRange: string;
    searchText: string;
    categories = [];
    priceRanges = [];
    options = [
        {key: "1", value: "< 1 triệu"},
        {key: "2", value: "1-3 triệu"},
        {key: "3", value: "3-5 triệu"},
        {key: "4", value: "5-10 triệu"},
        {key: "5", value: "10-15 triệu"},
        {key: "6", value: "15-20 triệu"},
        {key: "7", value: "20-40 triệu"},
        {key: "8", value: "40-70 triệu"},
        {key: "9", value: "70-100 triệu"},
        {key: "10", value: "100-300 triệu"},
        {key: "11", value: "300-500 triệu"},
        {key: "12", value: "500-800 triệu"},
        {key: "13", value: "800 triệu - 1 tỷ"},
        {key: "14", value: "1-2 tỷ"},
        {key: "15", value: "2-3 tỷ"},
        {key: "16", value: "3-5 tỷ"},
        {key: "17", value: "5-7 tỷ"},
        {key: "18", value: "7-10 tỷ"},
        {key: "19", value: "> 10 tỷ"}
      ];
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
       
            let rangeKeys = ["1", "2", "3", "4", "5", "6", "7"];
            this.priceRanges = this.options.filter(o => rangeKeys.includes(o.key));
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
        if (this.selectedRegions.length == 0 && this.selectedCategories.length == 0){
            this.toastr.warning("Chọn địa phương hoặc danh mục để tìm tin đăng");
            return;
        }
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
        var params = {s: `${this.searchText}`};
        if (this.selectedPriceRange != null){
            params["priceRange"] = this.selectedPriceRange;
        }
        this.searchText = null;
        this.router.navigate([query], { 
            queryParams: params});
    }

    onRegionsSelectionChange(event){
        this.selectedRegions = event;
    }
    onCategoriesSelectionChange(event){
        this.selectedCategories = event;
        var category = null;
        if (this.selectedCategories.length > 0) {
            category = this.selectedCategories[0];
        }
        if (category){
            if (this.selectedCategories.length > 1){
                category = this.selectedCategories[1];
            }
        }
        let rangeKeys = (category.priceRanges || "").split(",").filter(entry => entry.trim() != '');
        if (rangeKeys.length == 0){
            rangeKeys = ["1", "2", "3", "4", "5", "6", "7"];
        }
        this.priceRanges = this.options.filter(o => rangeKeys.includes(o.key));
        this.selectedPriceRange = null;
    }

}
