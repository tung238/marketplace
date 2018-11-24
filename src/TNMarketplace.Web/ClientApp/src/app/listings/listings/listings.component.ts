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
    }

    this.router.navigate([], {
      queryParams: params, queryParamsHandling: 'merge'
    });
  }
  onSortOrderChange(event) {
    console.log(event);
    this.prepareGetListings();
  }
  nzPageSizeChange(event) {
    if (event == 0) { return; }
    this.pageSize = event;
    this.prepareGetListings();
  }

  prepareGetListings() {
    // Trick the Router into believing it's last link wasn't previously loaded
    var urlSegments = this.router.url.split(/[.?/]/).filter(entry => entry.trim() != '');
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
      params["pf"], params["pt"], this.sortOrder, undefined, pageNumber, this.pageSize).subscribe((r) => {
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
      breadcrumb.push({ iconClass: region.iconClass, routeLink: `/${region.slug}`, name: region.name });
      if (area) {
        breadcrumb.push({ routeLink: `/${region.slug}/${area.slug}`, name: area.name });
      }
    }
    if (category) {
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
      }
      if (!this.priceRange){
        this.priceRange = [0, this.maxPrice];
      }
    }
    breadcrumb.unshift({ iconClass: 'fa fa-home', routeLink: '/', name: 'Home' });
    return breadcrumb;
  }
}
