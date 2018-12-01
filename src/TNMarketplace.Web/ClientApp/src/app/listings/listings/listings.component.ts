import { Component, OnInit, OnDestroy } from '@angular/core';
import { ListingService } from '../../api';
import { Router, Params, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AppService } from '@app/app.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'appc-listings',
  templateUrl: './listings.component.html',
  styleUrls: ['./listings.component.scss']
})
export class ListingsComponent implements OnInit, OnDestroy {
  // selectedTabIndex = 0;
  listingViewMode: number;
  isLoading = true;
  isPhotoOnly: boolean = false;
  sortOrder = 0;
  priceRange: number[];
  searchText = "";
  allListingsModel: any;
  // nzPageIndex: number = 1;
  pageSize = 20;
  mySubscription: Subscription;
  listOfOption = [];
  listOfSelectedValue = [];
  maxPrice = 100000000;
  categorySlug: any;
  subCategorySlug: any;
  regionSlug: any;
  areaSlug: any;
  regions = [];
  areas = [];
  breadcrumb = [];
  regionChange(value: string): void {
    this.areaSlug = null;
    if(value){

      let r = this.regions.find(r=>r.slug == value);
      if(r){
        this.areas = r.children
      }else{
        this.areas = [];
      }
    }else{
      this.areas = [];
    }
  }



  constructor(private listingService: ListingService,
    private appService: AppService,
    private router: Router,
    private route: ActivatedRoute
  ) {
  }

  ngOnDestroy() {
    if (this.mySubscription)
      this.mySubscription.unsubscribe();
  }

  ngOnInit() {
    this.listingViewMode = 2;
    var appData = this.appService.appData;
    if (!appData) {
      return [];
    }
    this.regions = appData.regionsTree;
    
    this.prepareGetListings();
    this.mySubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.prepareGetListings();
        var params = this.router.routerState.snapshot.root.children[0].queryParams;
        var priceFrom = params["pf"];
        var priceTo = params["pt"];
        if (priceFrom && priceTo) {
          this.priceRange = [priceFrom, priceTo];
        }
      }
    });
  }

  nzPageIndexChange(event) {
    console.log(event);
    if (event == 0) { return; }
    var params = this.router.routerState.snapshot.root.children[0].queryParams;
    this.router.navigate([], {
      queryParams: { p: `${event}` }, queryParamsHandling: 'merge'
    });

    // this.prepareGetListings();
  }
  doSearch() {

    var params = { s: this.searchText };
    if (this.priceRange) {
      var priceFrom = this.priceRange[0];
      var priceTo = this.priceRange[1];
      if (priceFrom != 0 || priceTo != this.maxPrice) {
        params["pf"] = priceFrom;
        params["pt"] = priceTo;
      }
      params["p"] = 1;
    }
    if(this.sortOrder){
      params["order"] = this.sortOrder;
    }

    var url = (this.regionSlug != null ? `/${this.regionSlug}`: "");
    url += (this.areaSlug != null ? `/${this.areaSlug}`: "");
    url += this.categorySlug != null ? `/${this.categorySlug}`: "";
    url += (this.subCategorySlug != null ? `/${this.subCategorySlug}`:"");
    this.router.navigate([url], {
      queryParams: params, queryParamsHandling: 'merge'
    });
  }
  onSortOrderChange(event) {
    console.log(event);
    var params = { p: 1,order: this.sortOrder };

    var url = (this.regionSlug != null ? `/${this.regionSlug}`: "");
    url += (this.areaSlug != null ? `/${this.areaSlug}`: "");
    url += this.categorySlug != null ? `/${this.categorySlug}`: "";
    url += (this.subCategorySlug != null ? `/${this.subCategorySlug}`:"");
    this.router.navigate([url], {
      queryParams: params, queryParamsHandling: 'merge'
    });
  }
  nzPageSizeChange(event) {
    if (event == 0) { return; }
    this.pageSize = event;
    this.prepareGetListings();
  }

  prepareGetListings() {
    this.breadcrumb = this.getBreadCrumb();

    // Trick the Router into believing it's last link wasn't previously loaded
    var urlSegments = []
    
    if(this.regionSlug){
      urlSegments.push(this.regionSlug);
    }
    if(this.areaSlug){
      urlSegments.push(this.areaSlug);
    }
    if (this.categorySlug){
      urlSegments.push(this.categorySlug);
    }
    if (this.subCategorySlug){
      urlSegments.push(this.subCategorySlug);
    }
    var params = this.router.routerState.snapshot.root.children[0].queryParams;
    if (params["vm"] == "grid") {
      this.listingViewMode = 1;
    }
    if (params["order"]) {
      this.sortOrder = params["order"];
    }
    // this.selectedTabIndex = 0;
    if (urlSegments != null && urlSegments.length > 0) {
      this.getListings(urlSegments, params);
      return;
    }
    this.router.navigate(["/"]);
  }

  getListings(paths: string[], params: Params) {
    let pageNumber = params["p"] != undefined && params["p"] > 0 ? params["p"] : 1;
    let res = this.listingService.apiListingSearchGet(undefined, paths, params["s"], params["l"], this.isPhotoOnly,
      params["pf"], params["pt"], undefined, this.sortOrder, undefined, pageNumber, this.pageSize).subscribe((r) => {
        this.allListingsModel = [];
        this.isLoading = false;
        this.allListingsModel = r;
        console.log(r);
      })
  }
  formatter(value) {
    return (new CurrencyPipe("vi-VN")).transform(value, "VND");
  }
  getBreadCrumb() {
    var appData = this.appService.appData;
    if (!appData) {
      return [];
    }
    var breadcrumb = [];
    var categories = appData.categoriesTree;
    var regions = appData.regionsTree;
    var urlSegments = this.router.url.split(/[.?/]/).filter(entry => entry.trim() != '');
    var region: any;
    var area: any;
    var category: any;
    var subcategory: any;
    urlSegments.forEach(element => {
      if (!region) {
        region = regions.find(re => re.slug == element);
      }
      if (!category) {
        category = categories.find(cat => cat.slug == element);
      }
    });

    urlSegments.forEach(element => {
      if (region && !area) {
        area = region.children.find(c => c.slug == element);
      }
      if (category && !subcategory) {
        subcategory = category.children.find(ca => ca.slug == element);
      }
    });
    if (region) {
      this.regionSlug = region.slug;
      this.regionChange(this.regionSlug);
      breadcrumb.push({ iconClass: region.iconClass, routeLink: `/${region.slug}`, name: region.name });
      if (area) {
        this.areaSlug = area.slug;
        breadcrumb.push({ routeLink: `/${region.slug}/${area.slug}`, name: area.name });
      }else{
        this.areaSlug = null;
      }
    }
    if (category) {
      this.categorySlug = category.slug;
      if (category.maxPrice > 0) {
        this.maxPrice = category.maxPrice;
      }
      if (!region) {
        breadcrumb.push({ routeLink: `/${category.slug}`, name: category.name });
      } else {
        if (!area) {
          breadcrumb.push({ routeLink: `/${region.slug}/${category.slug}`, name: category.name });
        } else {
          breadcrumb.push({ routeLink: `/${region.slug}/${area.slug}/${category.slug}`, name: category.name });
        }
      }
      if (subcategory) {
        this.subCategorySlug = subcategory.slug;
        if (subcategory.maxPrice > 0) {
          this.maxPrice = category.maxPrice;
        }
        if (!region) {
          breadcrumb.push({ routeLink: `/${category.slug}/${subcategory.slug}`, name: subcategory.name });
        } else {
          if (!area) {
            breadcrumb.push({ routeLink: `/${region.slug}/${category.slug}/${subcategory.slug}`, name: subcategory.name });
          } else {
            breadcrumb.push({ routeLink: `/${region.slug}/${area.slug}/${category.slug}/${subcategory.slug}`, name: subcategory.name });
          }
        }
      }else{
        this.subCategorySlug = null;
      }
      if (!this.priceRange){
        this.priceRange = [0, this.maxPrice];
      }
    }
    breadcrumb.unshift({ iconClass: 'fa fa-home', routeLink: '/', name: 'Home' });
    return breadcrumb;
  }
}
