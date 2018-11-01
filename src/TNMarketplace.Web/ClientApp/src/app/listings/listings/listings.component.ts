import { Component, OnInit } from '@angular/core';
import { ListingService } from '../../api';
import { Router, UrlSegment, Params } from '@angular/router';

@Component({
  selector: 'appc-listings',
  templateUrl: './listings.component.html',
  styleUrls: ['./listings.component.scss']
})
export class ListingsComponent implements OnInit {

  listingViewMode: number;
  allListings = [];
  listingsWithPicture = [];
  constructor(private listingService: ListingService,
    private router: Router
    ){
   }

  ngOnInit() {
    this.listingViewMode = 1;
    var urlSegments = this.router.routerState.snapshot.root.children[0].url;
    var params = this.router.routerState.snapshot.root.children[0].queryParams;
    if (params["vm"] == "grid"){
      this.listingViewMode = 2;
    }
    var photoOnly = params["po"];
    if (urlSegments != null && urlSegments.length > 0){
      this.getListings(urlSegments, params, photoOnly);
      return;
    }
    this.router.navigate(["/"]);
  }

  getListings(paths: UrlSegment[], params: Params, photoOnly?: boolean){

    let res = this.listingService.apiListingSearchGet(undefined, paths.map(c=> c.path), params["s"], params["l"], photoOnly, 
    params["pf"], params["pt"]).subscribe((r) =>{
      if (photoOnly){
        this.listingsWithPicture = r.listingsPageList;
      }else{
        this.allListings = r.listingsPageList;
      }
      console.log(r);
    })
  }

  selectTab(photoOnly: boolean){
    var urlSegments = this.router.routerState.snapshot.root.children[0].url;
    var params = this.router.routerState.snapshot.root.children[0].queryParams;
    if (urlSegments != null && urlSegments.length > 0){
      this.getListings(urlSegments, params, photoOnly);
      return;
    }
  }

}
