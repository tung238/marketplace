import { Component, OnInit } from '@angular/core';
import { ListingService } from '../../api';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { Location } from '@angular/common';
import { AppService } from '@app/app.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'appc-listing-item',
  templateUrl: './listing-item.component.html',
  styleUrls: ['./listing-item.component.scss']
})
export class ListingItemComponent implements OnInit {

  listingItem: any = {};
  listingMetas1: any[] = [];
  listingMetas2: any[] = [];
  regionRouteLink ="";
  areaRouteLink = "";
  categoryRouteLink = "";

  array = [ 1, 2, 3 ];

  constructor(private listingService: ListingService, 
    private appService: AppService,
    private titleService: Title,
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
      var listingMetas = response.listingCurrent.listingMetas;
      listingMetas.forEach(element => {
        element.value = element.value.split(/[\[",\]]/).filter(entry => entry.trim() != '').join(", ");
      });
      this.listingMetas1 = listingMetas.filter(e=>e.metaField.controlTypeID != 2 && e.metaField.controlTypeID != 4);
      this.listingMetas2 = listingMetas.filter(e=>e.metaField.controlTypeID == 2 || e.metaField.controlTypeID == 4);
      let title = (response.listingCurrent.title) || ""; 
      this.titleService.setTitle(title + " - Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com");
      console.log(response);
    })
  }
  getListingId(): number{
    var urlSegments = this.router.url.split('/').filter(entry => entry.trim() != '');
    let str = urlSegments[urlSegments.length - 1];
    let arr = str.split(/id|.html/)
    var listingId = 0;
    for(var item in arr){
      if (!isNaN(arr[item] as any)){
        listingId = parseInt(arr[item]);
        break;
      }
    }
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
    var regionRouteLink ="";
    var areaRouteLink = "";
    var categoryRouteLink = "";
    if (region){
      regionRouteLink += `/${region.slug}`;
      if(!area){
        categoryRouteLink += `/${region.slug}`;
        areaRouteLink += `/${region.slug}`;
      }else{
        categoryRouteLink += `/${region.slug}/${area.slug}`;
        areaRouteLink += `/${region.slug}/${area.slug}`;
      }
    }
    if (category){
      if(!subcategory){
        regionRouteLink += `/${category.slug}`;
        areaRouteLink += `/${category.slug}`;
        categoryRouteLink += `/${category.slug}`;
      }else{
        regionRouteLink += `/${category.slug}/${subcategory.slug}`;
        areaRouteLink += `/${category.slug}/${subcategory.slug}`;
        categoryRouteLink += `/${category.slug}/${subcategory.slug}`;
      }
    }
    this.regionRouteLink = regionRouteLink;
    this.areaRouteLink = areaRouteLink;
    this.categoryRouteLink = categoryRouteLink;
    return breadcrumb;
  }
}
