import { Component, OnInit, Input } from '@angular/core';
import { AppService } from '@app/app.service';

@Component({
  selector: 'appc-product-list-row',
  templateUrl: './product-list-row.component.html',
  styleUrls: ['./product-list-row.component.scss']
})
export class ProductListRowComponent implements OnInit {
  private _listingItem: any;
  get listingItem() {
    return this._listingItem;
  }

  @Input()
  set listingItem(item) {
    if (item.urlPicture == null || item.urlPicture.length == 0) {
      item.urlPicture = '/assets/images/m5.jpg';
    }
    this._listingItem = item;
  }
  constructor(
    private appService: AppService
  ) { }

  ngOnInit() {
  }
  generateRouteLink() {
    var str = this.listingItem.listingCurrent.title;
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
    str = `/${str.toLowerCase()}_id${this.listingItem.listingCurrent.id}.html`;
    var segments = [];
    let item = this.listingItem.listingCurrent;
    if (item.region) {
      segments.unshift(this.listingItem.listingCurrent.region.slug);
    }
    if (item.area) {
      segments.unshift(this.listingItem.listingCurrent.area.slug);
    }
    if (item.category) {
      if (item.category.parent == 0) {
        segments.unshift(item.category.slug);
      } else {
        let categories = this.appService.appData.categoriesTree;
        let category = categories.find(c => c.id == item.category.parent);
        if (category) {
          segments.unshift(category.slug);
          segments.unshift(item.category.slug);
        }
      }
    }
    segments.forEach(element => {
      str = `${element}/${str}`;
    });
    return `/${str}`;
  }
}
