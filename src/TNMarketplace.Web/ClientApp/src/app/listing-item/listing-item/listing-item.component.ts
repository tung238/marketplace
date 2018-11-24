import { Component, OnInit } from '@angular/core';
import { ListingService } from '../../api';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Location } from '@angular/common';
import { AppService } from '@app/app.service';

@Component({
  selector: 'appc-listing-item',
  templateUrl: './listing-item.component.html',
  styleUrls: ['./listing-item.component.scss']
})
export class ListingItemComponent implements OnInit {

  listingItem: any = {};
  array = [ 1, 2, 3 ];

  constructor(private listingService: ListingService, 
    private appService: AppService,
    private router: Router) {

   }

  ngOnInit() {
    var urlSegments = this.router.url.split('/').filter(entry => entry.trim() != '');
    if (urlSegments != null && urlSegments.length > 0){
      var id = this.getListingId();
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
  getListingId(): number{
    var urlSegments = this.router.url.split('/').filter(entry => entry.trim() != '');
    let str = urlSegments[urlSegments.length - 1];
    let arr = str.split(/id|.html/)
    var listingId = 0;
    arr.forEach(item=>{
      if (item){
        listingId = parseInt(item);
        return;
      }
    })
    return listingId;
  }
  getBreadCrumb(){
    var appData = this.appService.appData;
    if (!appData){
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
      if (!region){
        region = regions.find(re=>re.slug == element);
      }
      if (!category){
        category = categories.find(cat=>cat.slug == element);
      }
    });
    
    urlSegments.forEach(element=>{
      if (region && !area){
        area = region.children.find(c=>c.slug == element);
      }
      if(category && !subcategory){
        subcategory = category.children.find(ca=>ca.slug == element);
      }
    });
    if(region){
      breadcrumb.push({iconClass: region.iconClass, routeLink: `/${region.slug}`, name:region.name});
      if (area){
        breadcrumb.push({routeLink: `/${region.slug}/${area.slug}`, name: area.name});
      }
    }
    if(category){
      if (!region){
        breadcrumb.push({routeLink: `/${category.slug}`, name: category.name});
      }else{
        if (!area){
          breadcrumb.push({routeLink: `/${region.slug}/${category.slug}`, name: category.name});
        }else{
          breadcrumb.push({routeLink: `/${region.slug}/${area.slug}/${category.slug}`, name: category.name});
        }
      }
      if(subcategory){
        if (!region){
          breadcrumb.push({routeLink: `/${category.slug}/${subcategory.slug}`, name: subcategory.name});
        }else{
          if(!area){
            breadcrumb.push({routeLink: `/${region.slug}/${category.slug}/${subcategory.slug}`, name: subcategory.name});
          }else{
            breadcrumb.push({routeLink: `/${region.slug}/${area.slug}/${category.slug}/${subcategory.slug}`, name: subcategory.name});
          }
        }
      }
    }
    breadcrumb.push({name: `Tin đăng #${this.getListingId()}`});
    breadcrumb.unshift({iconClass: 'fa fa-home', routeLink:'/', name:'Home'});
    return breadcrumb;
  }
}
