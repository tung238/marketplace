import { Component, OnInit } from '@angular/core';
import { ListingService } from '../../api';
import { Router, UrlSegment, Params } from '@angular/router';

@Component({
  selector: 'appc-listings',
  templateUrl: './listings.component.html',
  styleUrls: ['./listings.component.scss']
})
export class ListingsComponent implements OnInit {

  allListingViewMode: number;
  listingWithPictureViewMode: number;
  constructor(private listingService: ListingService,
    private router: Router
    ){
   }

  ngOnInit() {
    this.allListingViewMode = 1;
    this.listingWithPictureViewMode = 1;
    var urlSegments = this.router.routerState.snapshot.root.children[0].url;
    var params = this.router.routerState.snapshot.root.children[0].queryParams;
    if (urlSegments != null && urlSegments.length > 0){
      this.getListings(urlSegments, params);
      return;
    }
    this.router.navigate(["/"]);
  }

  getListings(paths: UrlSegment[], params: Params){
    let res = this.listingService.apiListingSearchGet(undefined, paths.map(c=> c.path), params["s"], params["l"], params["photoOnly"], 
    params["pf"], params["pt"]).subscribe(response =>{
      console.log(response);
    })
  }

}
