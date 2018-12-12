import { Component, OnInit } from '@angular/core';
import { ListingService, AdminListingService } from '@app/api';
import { Router } from '@angular/router';

@Component({
  selector: 'appc-meta-field-list',
  templateUrl: './meta-field-list.component.html',
  styleUrls: ['./meta-field-list.component.scss']
})
export class MetaFieldListComponent implements OnInit {
  sortName = null;
  sortValue = null;
  listOfSearchName = [];
  searchAddress: string;
  data = [];
  controlTypeMapp = ["Dropdown list", "Radio list", "Checkbox list", "Textbox", "Multiline texbox"];
  displayData = [ ...this.data ];

  constructor(
    private listingService: ListingService,
    private router: Router,
    private adminListingService: AdminListingService
    ) { }

  ngOnInit() {
    this.adminListingService.apiAdminAdminListingCustomFieldsGet().subscribe(res=>{
      this.data = res;
      this.data.forEach(d=>{
        d.typeName = this.controlTypeMapp[d.controlTypeID];
        d.categories = d.metaCategories.map(m=>{
          return m.category.name;
        }).join(", ");
      })
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
  edit(data){
    this.router.navigate(["/admin/thuoc-tinh/sua", data.id]);
  }
  delete(id){
    // this.adminListingService.api
  }
  cancel(){

  }
}
