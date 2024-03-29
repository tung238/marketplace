import { Component, OnInit } from '@angular/core';
import { ListingService, AdminListingService } from '@app/api';
import { Router } from '@angular/router';

@Component({
  selector: 'appc-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  sortName = null;
  sortValue = null;
  listOfSearchName = [];
  searchAddress: string;
  data = [];
  displayData = [ ...this.data ];

  constructor(
    private listingService: ListingService,
    private router: Router,
    private adminListingService: AdminListingService
    ) { }

  ngOnInit() {
    this.adminListingService.apiAdminAdminListingCategoriesGet().subscribe(res=>{
      this.data = res;
      this.search()
    })

  }

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
  viewCategory(category){
    if (category.parent > 0){
      const parentCat = this.data.find(c=>c.id == category.parent);
      this.router.navigate([`/${parentCat.slug}/${category.slug}`]);
    }else{
      this.router.navigate([`/${category.slug}`]);
    }
  }
  editCategory(category){
    this.router.navigate(["/admin/danh-muc/sua", category.id]);
  }
  confirm(id){
    // this.adminListingService.api
  }
  cancel(){

  }

}
