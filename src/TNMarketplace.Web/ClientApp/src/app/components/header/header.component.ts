import { Component, OnInit, ViewChild, ElementRef, Renderer2, OnDestroy } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

import { AccountService, DataService } from '@app/core';
import { AppService } from '../../app.service';
import { ProfileService } from '@app/account/+profile/profile.service';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';

@Component({
    selector: 'appc-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {
    @ViewChild('menu') public menu: ElementRef;

    public isCollapsed = true;
    selectedRegions: any[] = [];
    selectedCategories: any[] = [];
    selectedPriceRange: string;
    searchText: string;
    categories = [];
    priceRanges = [];
    options = [
        { key: "1", value: "< 1 triệu" },
        { key: "2", value: "1-3 triệu" },
        { key: "3", value: "3-5 triệu" },
        { key: "4", value: "5-10 triệu" },
        { key: "5", value: "10-15 triệu" },
        { key: "6", value: "15-20 triệu" },
        { key: "7", value: "20-40 triệu" },
        { key: "8", value: "40-70 triệu" },
        { key: "9", value: "70-100 triệu" },
        { key: "10", value: "100-300 triệu" },
        { key: "11", value: "300-500 triệu" },
        { key: "12", value: "500-800 triệu" },
        { key: "13", value: "800 triệu - 1 tỷ" },
        { key: "14", value: "1-2 tỷ" },
        { key: "15", value: "2-3 tỷ" },
        { key: "16", value: "3-5 tỷ" },
        { key: "17", value: "5-7 tỷ" },
        { key: "18", value: "7-10 tỷ" },
        { key: "19", value: "> 10 tỷ" }
    ];
    regions = [];
    mySubscription: Subscription;

    constructor(
        private accountService: AccountService,
        private dataService: DataService,
        private toastr: ToastrService,

        private appService: AppService,
        public oAuthService: OAuthService,
        private router: Router,
        private renderer: Renderer2
    ) { }

    ngOnDestroy() {
        if (this.mySubscription)
            this.mySubscription.unsubscribe();
    }
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

        this.mySubscription = this.router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                this.updateSelected();
            }
        });
    }

    updateSelected() {
        var urlSegments = this.router.url.split(/[.?/]/).filter(entry => entry.trim() != '');
        var region: any;
        var area: any;
        var category: any;
        var subcategory: any;
        for (var i = 0; i < urlSegments.length; i++) {
            region = this.regions.find(re => re.slug == urlSegments[i]);
            if (region) {
                urlSegments.splice(i, 1);
                break;
            }
        }
        for (var i = 0; i < urlSegments.length; i++) {
            category = this.categories.find(cat => cat.slug == urlSegments[i]);
            if (category) {
                urlSegments.splice(i, 1);
                break;
            }
        }
        if (region) {
            for (var i = 0; i < urlSegments.length; i++) {
                area = region.children.find(c => c.slug == urlSegments[i]);
                if (area) {
                    break;
                }
            }
        }
        if (category) {
            for (var i = 0; i < urlSegments.length; i++) {
                subcategory = category.children.find(ca => ca.slug == urlSegments[i]);
                if (subcategory) {
                    break;
                }
            }
        }

        if(region){
            if (area){
                this.selectedRegions = [region.id, area.id];
            }else{
                this.selectedRegions = [region.id, 0]
            }
        }else{
            this.selectedRegions = [];
        }
        if (category){
            if (subcategory){
                this.selectedCategories = [category.id, subcategory.id];
            }else{
                this.selectedCategories = [category.id, 0]
            }
        }else{
            this.selectedCategories = [];
        }

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
        if (this.selectedRegions.length == 0 && this.selectedCategories.length == 0) {
            this.toastr.warning("Chọn địa phương hoặc danh mục để tìm tin đăng");
            return;
        }
        if (!this.searchText) {
            this.searchText = "";
        }
        var category = null;
        var subcategory = null;
        var region = null;
        var area = null;
        if (this.selectedCategories.length > 0) {
            let categoryId = this.selectedCategories[0];
            category = this.categories.find(c=>c.id == categoryId);
        }
        if (category) {
            if (this.selectedCategories.length > 1) {
                let subcategoryId = this.selectedCategories[1];
                subcategory = category.children.find(cc=>cc.id == subcategoryId);
            }
        }
        if (this.selectedRegions.length > 0) {
            let regionId = this.selectedRegions[0];
            region = this.regions.find(r=>r.id == regionId);
        }
        if (region) {
            if (this.selectedRegions.length > 1) {
                let areaId = this.selectedRegions[1];
                area = region.children.find(a=>a.id == areaId);
            }
        }

        var query = ""
        if (subcategory && subcategory.id > 0) {
            query = `/${category.slug}/${subcategory.slug}`
        } else if (category) {
            query = `/${category.slug}`
        }
        if (area && area.id > 0) {
            query = `/${region.slug}/${area.slug}${query}`;
        } else if (region) {
            query = `/${region.slug}/${query}`;
        }
        var params = { s: `${this.searchText}` };
        if (this.selectedPriceRange != null) {
            params["priceRange"] = this.selectedPriceRange;
        }
        this.searchText = null;
        this.router.navigate([query], {
            queryParams: params
        });
    }

    onRegionsSelectionChange(event) {
        // this.selectedRegions = event;
    }
    onCategoriesSelectionChange(event) {
        var category = null;
        if (event.length > 0) {
            category = event[0];
        }
        if (category) {
            if (event.length > 1) {
                category = event[1];
            }
        }
        let rangeKeys = (category.priceRanges || "").split(",").filter(entry => entry.trim() != '');
        if (rangeKeys.length == 0) {
            rangeKeys = ["1", "2", "3", "4", "5", "6", "7"];
        }
        this.priceRanges = this.options.filter(o => rangeKeys.includes(o.key));
        this.selectedPriceRange = null;
    }

    myFunction() {
        if (this.menu.nativeElement.className === "topnav") {
            this.renderer.addClass(this.menu.nativeElement, "responsive")
        } else {
            this.renderer.removeClass(this.menu.nativeElement, "responsive")
        }
    }

}
