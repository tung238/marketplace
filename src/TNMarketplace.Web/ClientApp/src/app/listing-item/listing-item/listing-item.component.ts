import { Component, OnInit } from '@angular/core';
import { ListingService } from '../../api';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'appc-listing-item',
  templateUrl: './listing-item.component.html',
  styleUrls: ['./listing-item.component.scss']
})
export class ListingItemComponent implements OnInit {

  listingItem: any = {};
  array = [ 1, 2, 3 ];

  constructor(private listingService: ListingService, 
    private router: Router) {

   }

  ngOnInit() {
    var urlSegments = this.router.routerState.snapshot.root.children[0].url;
    if (urlSegments != null && urlSegments.length > 0){
      var lastPath = urlSegments[urlSegments.length - 1] ;
      var id = parseInt (lastPath.path.replace(".html", ""));
      if (isNaN(id)){
        this.router.navigate(["/not-found"]);
      }
      this.getListingById(id);
      return;
    }
    this.router.navigate(["/not-found"]);
  }

  getListingById(listingId: number){
    this.listingService.apiListingListingGet(listingId).subscribe((response: any) =>{
      this.listingItem = response;
      console.log(response);
    })
  }

}
