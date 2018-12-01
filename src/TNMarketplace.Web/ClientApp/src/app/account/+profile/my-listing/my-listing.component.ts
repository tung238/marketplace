import { Component, OnInit } from '@angular/core';
import { ListingService } from '@app/api';
import { AccountService } from '@app/core';
import { AppService } from '@app/app.service';
import { Router } from '@angular/router';
import { NotificationsService } from '@app/simple-notifications';

@Component({
  selector: 'appc-my-listing',
  templateUrl: './my-listing.component.html',
  styleUrls: ['./my-listing.component.scss']
})
export class MyListingComponent implements OnInit {

  sortName = null;
  sortValue = null;
  listOfSearchName = [];
  searchAddress: string;
  data = [];
  displayData = [ ...this.data ];

  sort(sort: { key: string, value: string }): void {
    this.sortName = sort.key;
    this.sortValue = sort.value;
    this.search();
  }

  search(): void {
    /** sort data **/
    if (this.sortName && this.sortValue) {
      this.displayData = this.data.sort((a, b) => (this.sortValue === 'ascend') ? (a[ this.sortName ] > b[ this.sortName ] ? 1 : -1) : (b[ this.sortName ] > a[ this.sortName ] ? 1 : -1));
    } else {
      this.displayData = this.data;
    }
  }

  constructor(private listingService: ListingService,
    private appService: AppService,
    private router: Router,
    private ns: NotificationsService,
    private accountService: AccountService) { }

  ngOnInit() {
    this.listingService.apiListingSearchGet(undefined, [], undefined, undefined, false,
    undefined, undefined, this.accountService.user.name, 0).subscribe(data=>{
      this.data = data.listings;
      this.search();
    })
  }
  editListing(id){
    this.router.navigate(['/dang-tin', id]);
  }
  deleteListing(id){
    this.listingService.apiListingListingDeletePost(id).subscribe(res=>{
      this.ns.success("","Xóa tin thành công");
      this.listingService.apiListingSearchGet(undefined, [], undefined, undefined, false,
        undefined, undefined, this.accountService.user.name, 0).subscribe(data=>{
          this.data = data.listings;
          this.search();
        })
    })
  }
  generateRouteLink(listing) {
    var str = listing.title;
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    str = str.replace(/ |,|;/g, "-");
    str = `/${str.toLowerCase()}_id${listing.id}.html`;
    var segments = [];
    if (listing.region) {
      segments.unshift(listing.region.slug);
    }
    if (listing.area) {
      segments.unshift(listing.area.slug);
    }
    if (listing.category) {
      if (listing.category.parent == 0) {
        segments.unshift(listing.category.slug);
      } else {
        let categories = this.appService.appData.categoriesTree;
        let category = categories.find(c => c.id = listing.category.parent);
        if (category) {
          segments.unshift(category.slug);
          segments.unshift(listing.category.slug);
        }
      }
    }
    segments.forEach(element => {
      str = `${element}/${str}`;
    });
    return `/${str}`;
  }

}
