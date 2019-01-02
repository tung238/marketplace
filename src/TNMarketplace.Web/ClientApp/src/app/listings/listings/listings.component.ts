import { Component, OnInit, OnDestroy } from '@angular/core';
import { ListingService } from '../../api';
import { Router, Params, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AppService } from '@app/app.service';
import { CurrencyPipe } from '@angular/common';
import { Title } from '@angular/platform-browser';

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
  options = [
    {key: "1", pf:0, pt: 1000000},
    {key: "2", pf:1000000, pt: 3000000},
    {key: "3", pf:3000000, pt:5000000},
    {key: "4", pf:5000000, pt:10000000},
    {key: "5", pf:10000000, pt:15000000},
    {key: "6", pf:15000000, pt:20000000},
    {key: "7", pf:20000000, pt:40000000},
    {key: "8", pf:40000000, pt:70000000},
    {key: "9", pf:70000000, pt:100000000},
    {key: "10", pf:100000000, pt:300000000},
    {key: "11", pf:300000000, pt:500000000},
    {key: "12", pf:500000000, pt:800000000},
    {key: "13", pf:800000000, pt:1000000000},
    {key: "14", pf:1000000000, pt:2000000000},
    {key: "15", pf: 2000000000, pt: 3000000000},
    {key: "16", pf:3000000000, pt: 5000000000},
    {key: "17", pf:5000000000, pt: 7000000000},
    {key: "18", pf:7000000000, pt: 10000000000},
    {key: "19", pf:10000000000, pt: 99000000000}
  ];

  regionChange(value: string): void {
    this.areaSlug = null;
    if (value) {

      let r = this.regions.find(r => r.slug == value);
      if (r) {
        this.areas = r.children
      } else {
        this.areas = [];
      }
    } else {
      this.areas = [];
    }
  }



  constructor(private listingService: ListingService,
    private appService: AppService,
    private titleService: Title,
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
  // doSearch() {

  //   var params = { s: this.searchText };
  //   if (this.priceRange) {
  //     var priceFrom = this.priceRange[0];
  //     var priceTo = this.priceRange[1];
  //     if (priceFrom != 0 || priceTo != this.maxPrice) {
  //       params["pf"] = priceFrom;
  //       params["pt"] = priceTo;
  //     }
  //     params["p"] = 1;
  //   }
  //   if (this.sortOrder) {
  //     params["order"] = this.sortOrder;
  //   }

  //   var url = (this.regionSlug != null ? `/${this.regionSlug}` : "");
  //   url += (this.areaSlug != null ? `/${this.areaSlug}` : "");
  //   url += this.categorySlug != null ? `/${this.categorySlug}` : "";
  //   url += (this.subCategorySlug != null ? `/${this.subCategorySlug}` : "");
  //   this.router.navigate([url], {
  //     queryParams: params, queryParamsHandling: 'merge'
  //   });
  // }
  onSortOrderChange(event) {
    console.log(event);
    var params = { p: 1, order: this.sortOrder };

    var url = (this.regionSlug != null ? `/${this.regionSlug}` : "");
    url += (this.areaSlug != null ? `/${this.areaSlug}` : "");
    url += this.categorySlug != null ? `/${this.categorySlug}` : "";
    url += (this.subCategorySlug != null ? `/${this.subCategorySlug}` : "");
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

    if (this.regionSlug) {
      urlSegments.push(this.regionSlug);
    }
    if (this.areaSlug) {
      urlSegments.push(this.areaSlug);
    }
    if (this.categorySlug) {
      urlSegments.push(this.categorySlug);
    }
    if (this.subCategorySlug) {
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
    let priceRange = this.options.find(o=>o.key == params["priceRange"]) || {key:"", pf: undefined, pt: undefined};
    let res = this.listingService.apiListingSearchGet(undefined, paths, params["s"], params["l"], this.isPhotoOnly,
    priceRange.pf, priceRange.pt, undefined, this.sortOrder, undefined, pageNumber, this.pageSize).subscribe((r) => {
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
    //clear data
    this.regionSlug = null;
    this.areaSlug = null;
    this.categorySlug = null;
    this.subCategorySlug = null;
    var appData = this.appService.appData;
    let title = "Chuyên trang mua bán ";

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
    for(var i =0; i<urlSegments.length; i++){
      region = regions.find(re => re.slug == urlSegments[i]);
      if (region) {
        urlSegments.splice(i, 1);
        break;
      }
    }
    for(var i =0; i<urlSegments.length; i++){
      category = categories.find(cat => cat.slug == urlSegments[i]);
      if (category) {
        urlSegments.splice(i, 1);
        break;
      }
    }
    if (region) {
      for(var i =0; i<urlSegments.length; i++){
        area = region.children.find(c => c.slug == urlSegments[i]);
        if (area) {
          urlSegments.splice(i, 1);
          break;
        }
      }
    }
    if (category) {
      for(var i =0; i<urlSegments.length; i++){
        subcategory = category.children.find(ca => ca.slug == urlSegments[i]);
        if (subcategory) {
          urlSegments.splice(i, 1);
          break;
        }
      }
    }
    if (region) {
      this.regionSlug = region.slug;
      this.regionChange(this.regionSlug);
      breadcrumb.push({ iconClass: region.iconClass, routeLink: `/${region.slug}`, name: region.name });
      if (area) {
        this.areaSlug = area.slug;
        breadcrumb.push({ routeLink: `/${region.slug}/${area.slug}`, name: area.name });
      } else {
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
        title += subcategory.name;
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
      } else {
        this.subCategorySlug = null;
        title += category.name;
      }
      if (!this.priceRange) {
        this.priceRange = [0, this.maxPrice];
      }
    }

    if (region) {
      if (area) {
        title += ` ${area.nameWithType}, ${region.nameWithType}`;
      } else {
        title += ` ${region.nameWithType}`;
      }
    }
    breadcrumb.unshift({ iconClass: 'fa fa-home', routeLink: '/', name: 'Home' });
    this.titleService.setTitle(title + " - Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com");
    return breadcrumb;
  }
}
