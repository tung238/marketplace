import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ControlTextbox } from '@app/shared/forms/controls/control-textbox';
import { ControlBase } from '@app/shared/forms/controls/control-base';
import { ControlCascader } from '@app/shared/forms/controls/control-cascader';
import { AppService } from '@app/app.service';
import { ControlUpload } from '@app/shared/forms/controls/control-upload';
import { ControlTextarea } from '@app/shared/forms/controls/control-textarea';
import { ControlCheckboxListNew } from '@app/shared/forms/controls/control-checkbox-list-new';
import { ListingService, Listing } from '@app/api';
import { ListingUpdateModel } from '@app/api/model/listingUpdateModel';
import { Router, ActivatedRoute } from '@angular/router';
import { DynamicFormComponent } from '@app/shared/forms/dynamic-form/dynamic-form.component';
import { Subject } from 'rxjs';
import { ControlPrice } from '@app/shared/forms/controls/control-price';

@Component({
  selector: 'appc-listing-add',
  templateUrl: './listing-add.component.html',
  styleUrls: ['./listing-add.component.scss']
})
export class ListingAddComponent implements OnInit, AfterViewInit  {
  current = 0;
  public controls: Array<ControlBase<any>> = [];
  regionsTree: any[];
  categoriesTree: any[];
  listingTypes: any[];
  data: Subject<any> = new Subject<any>();
  
  @ViewChild(DynamicFormComponent) 
  public ngForm: DynamicFormComponent;

  constructor(
    private appService: AppService,
    private listingService: ListingService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngAfterViewInit(){
    var id = this.route.snapshot.params['id'];
    if (id) {
      this.listingService.apiListingListingGet(id).subscribe(data => {
        console.log(data);
        let listing = data.listingCurrent;
        var filelist = data.pictures || []
        filelist.forEach(element => {
          element.uid = this.uuidv4()
        });
        this.data.next({
          'listingType': listing.listingTypeID,
          'regions': [listing.regionId, listing.areaId],
          'categories': [listing.category.parent, listing.categoryID],
          'title': listing.title,
          'description': listing.description,
          'price': listing.price.toString(),
          'images': filelist,
          'phone': listing.contactPhone,
          'id': listing.id
        });
        var control = this.controls.find(c => c.key == "images");
        if (control) {
          (control as ControlUpload).fileList = filelist
        }
      })
    }
  }

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
      new ControlPrice({
        key: 'price',
        label: 'Gía',
        placeholder: 'Giá',
        value: 0,
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
      }),
      new ControlTextbox({
        key: 'id',
        label: '',
        placeholder: '',
        value: '',
        type: 'hidden',
        required: false,
        order: 9
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
        if (c.key == 'listingTypes') {
          (c as ControlCheckboxListNew).options = this.listingTypes;
        }
      })
    })
    
  }
  uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
  listingAdd(event) {
    console.log(event);
    var control = this.controls.find(c => c.key == "images");
    var images = [];
    if (control) {
      images = (control as ControlUpload).fileList.map(f => {
        return { url: f.url, id: 0 };
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
    listingModel.price = Number(event.price);
    listingModel.regionIds = event.regions;
    listingModel.title = event.title;
    this.listingService.apiListingListingUpdatePost(listingModel).subscribe(res => {
      if (res.success) {
        this.router.navigateByUrl("/");
      }
    });
  }

}
