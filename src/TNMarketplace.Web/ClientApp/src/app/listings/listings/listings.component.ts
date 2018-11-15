import { Component, OnInit, OnDestroy } from '@angular/core';
import { ListingService } from '../../api';
import { Router, UrlSegment, Params, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'appc-listings',
  templateUrl: './listings.component.html',
  styleUrls: ['./listings.component.scss']
})
export class ListingsComponent implements OnInit, OnDestroy {
  // selectedTabIndex = 0;
  listingViewMode: number;
  isPhotoOnly: boolean = false;
  allListingsModel: any;
  pictureListingsModel: any;
  // nzPageIndex: number = 1;
  pageSize = 20;
  mySubscription: Subscription;
  constructor(private listingService: ListingService,
    private router: Router
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
      }
    });
  }

  nzPageIndexChange(event){
    console.log(event);
    if (event == 0){return;}
    this.router.navigate([], { 
      queryParams: {p: `${event}`} 
    });
    
    // this.prepareGetListings();
  }

  nzPageSizeChange(event){
    if (event == 0) { return;}
    this.pageSize = event;
    this.prepareGetListings();
  }

  prepareGetListings() {
    // Trick the Router into believing it's last link wasn't previously loaded
    var urlSegments = this.router.routerState.snapshot.root.children[0].url;
    var params = this.router.routerState.snapshot.root.children[0].queryParams;
    if (params["vm"] == "grid") {
      this.listingViewMode = 1;
    }
    // this.selectedTabIndex = 0;
    if (urlSegments != null && urlSegments.length > 0) {
      this.getListings(urlSegments, params);
      return;
    }
    this.router.navigate(["/"]);
  }

  getListings(paths: UrlSegment[], params: Params) {
    let pageNumber =  params["p"] != undefined &&  params["p"] > 0 ?  params["p"]: 1;
    let res = this.listingService.apiListingSearchGet(undefined, paths.map(c => c.path), params["s"], params["l"], this.isPhotoOnly,
      params["pf"], params["pt"], undefined, undefined, pageNumber, this.pageSize).subscribe((r) => {
        if (this.isPhotoOnly) {
          this.pictureListingsModel = r;
        } else {
          this.allListingsModel = r;
        }
        console.log(r);
      })
  }

  selectTab(photoOnly: boolean) {
    var urlSegments = this.router.routerState.snapshot.root.children[0].url;
    var params = this.router.routerState.snapshot.root.children[0].queryParams;
    this.isPhotoOnly = photoOnly;
    if (urlSegments != null && urlSegments.length > 0) {
      this.getListings(urlSegments, params);
      return;
    }
  }

}
