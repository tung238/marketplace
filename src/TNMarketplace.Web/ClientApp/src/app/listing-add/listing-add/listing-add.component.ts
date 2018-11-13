import { Component, OnInit } from '@angular/core';
import { ControlTextbox } from '@app/shared/forms/controls/control-textbox';
import { ControlBase } from '@app/shared/forms/controls/control-base';
import { ControlCascader } from '@app/shared/forms/controls/control-cascader';
import { AppService } from '@app/app.service';
import { ControlUpload } from '@app/shared/forms/controls/control-upload';
import { ControlTextarea } from '@app/shared/forms/controls/control-textarea';
import { forEach } from '@angular/router/src/utils/collection';
import { ControlCheckboxListNew } from '@app/shared/forms/controls/control-checkbox-list-new';
import { ListingService, Listing } from '@app/api';
import { ListingUpdateModel } from '@app/api/model/listingUpdateModel';
import { Router } from '@angular/router';

@Component({
  selector: 'appc-listing-add',
  templateUrl: './listing-add.component.html',
  styleUrls: ['./listing-add.component.scss']
})
export class ListingAddComponent implements OnInit {
  current = 0;
  public controls: Array<ControlBase<any>> = [];
  regionsTree: any[];
  categoriesTree: any[];
  listingTypes:any[];

  constructor(
    private appService: AppService,
    private listingService: ListingService,
    private router: Router
  ) { }

  ngOnInit() {

    const controls: Array<ControlBase<any>> = [
      new ControlCheckboxListNew({
        key: 'listingType',
        label: 'Loại tin',
        options: this.listingTypes,
        
        nzLabelProperty: 'name',
        nzValueProperty: 'id',
        required: true,
        order: 1
      }),
      new ControlCascader({
        key: 'regions',
        label: 'Tỉnh, thành phố',
        options: this.regionsTree,
        
        nzLabelProperty: 'name',
        nzValueProperty: 'id',
        required: true,
        order: 2
      }),
      new ControlCascader({
        key: 'categories',
        // value:[42, 20],
        label: 'Danh mục tin đăng',
        options: this.categoriesTree,
        nzLabelProperty: 'name',
        nzValueProperty: 'id',
        required: true,
        order: 3
      }),
      new ControlTextbox({
        key: 'title',
        label: 'Tiêu đề',
        placeholder: 'Tiêu đề',
        value: '',
        required: true,
        order: 4
      }),
      new ControlTextarea({
        key: 'description',
        label: 'Nội dung',
        placeholder: 'Nội dung',
        value: '',
        required: true,
        order: 5
      }),
      new ControlTextarea({
        key: 'price',
        label: 'Gía',
        placeholder: 'Giá',
        value: '',
        type: 'number',
        required: true,
        order: 6
      }),
      new ControlUpload({
        key: 'images',
        label: 'Hình ảnh',
        type: 'upload',
        fileList: [],
        required: false,
        order: 7
      }),
      new ControlTextbox({
        key: 'phone',
        label: 'Số điện thoại',
        placeholder: 'Số điện thoại',
        value: '',
        type: 'text',
        required: true,
        order: 8
      })
    ];

    this.controls = controls;
    this.appService.getAppData().then(data => {
      this.regionsTree = data.regionsTree;
      this.categoriesTree = data.categoriesTree;
      this.listingTypes = data.listingTypes;
      this.controls.forEach(c => {
        if (c.key == 'regions') {
          (c as ControlCascader).options = this.regionsTree;
        }
        if (c.key == 'categories') {
          (c as ControlCascader).options = this.categoriesTree;
        }
        if(c.key == 'listingTypes'){
          (c as ControlCheckboxListNew).options = this.listingTypes;
        }
      })
    })
  }

  listingAdd(event) {
    console.log(event);
    var control = this.controls.find(c => c.key == "images");
    var images = [];
    if (control) {
      images = (control as ControlUpload).fileList.map(f => {
        return {url: f.url, id: 0};
      });
    }
    var listingModel: ListingUpdateModel = {};
    listingModel.id = event.id || 0;
    listingModel.active = event.active;
    listingModel.categoryIds = event.categories;
    listingModel.contactEmail = "";
    listingModel.contactName = "";
    listingModel.contactPhone = event.phone;
    listingModel.description = event.description;
    listingModel.enabled = event.enabled;
    listingModel.listingTypeId = event.listingType;
    listingModel.pictures = images;
    listingModel.price = event.price;
    listingModel.regionIds = event.regions;
    listingModel.title = event.title;
    this.listingService.apiListingListingUpdatePost(listingModel).subscribe(res=>{
      if (res.success){
        this.router.navigateByUrl("/");
      }
    });
  }

}
