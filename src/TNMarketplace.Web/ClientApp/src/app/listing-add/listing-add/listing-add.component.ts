import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ControlTextbox } from '@app/shared/forms/controls/control-textbox';
import { ControlBase } from '@app/shared/forms/controls/control-base';
import { ControlCascader } from '@app/shared/forms/controls/control-cascader';
import { AppService } from '@app/app.service';
import { ControlUpload } from '@app/shared/forms/controls/control-upload';
import { ControlTextarea } from '@app/shared/forms/controls/control-textarea';
import { ControlCheckboxListNew } from '@app/shared/forms/controls/control-checkbox-list-new';
import { ListingService } from '@app/api';
import { ListingUpdateModel } from '@app/api/model/listingUpdateModel';
import { Router, ActivatedRoute } from '@angular/router';
import { DynamicFormComponent } from '@app/shared/forms/dynamic-form/dynamic-form.component';
import { Subject } from 'rxjs';
import { ControlPrice } from '@app/shared/forms/controls/control-price';
import { Title } from '@angular/platform-browser';
import { ControlDropdown } from '@app/shared/forms/controls/control-dropdown';
import { ControlCheckboxList } from '@app/shared/forms/controls/control-checkbox-list';
import { ControlRadioList } from '@app/shared/forms/controls/control-radio-list';
import { element } from '@angular/core/src/render3/instructions';

@Component({
  selector: 'appc-listing-add',
  templateUrl: './listing-add.component.html',
  styleUrls: ['./listing-add.component.scss']
})
export class ListingAddComponent implements OnInit, AfterViewInit {
  current = 0;
  public controls: Array<ControlBase<any>> = [];
  regionsTree: any[];
  categoriesTree: any[];
  metaCategories: any[];
  listingTypes: any[];
  categorySubject: Subject<any[]> = new Subject();
  data: any;
  controlIndex: number = 0;

  @ViewChild(DynamicFormComponent)
  public ngForm: DynamicFormComponent;

  constructor(
    private appService: AppService,
    private listingService: ListingService,
    private route: ActivatedRoute,
    private titleService: Title,
    private router: Router
  ) { }

  ngAfterViewInit() {
    var id = this.route.snapshot.params['id'];
    this.categorySubject.subscribe(cats => {
      this.onCategoryChange(cats);
    })
    this.listingService.apiListingListingUpdateGet(id).subscribe(data => {
      let title = (data.listingCurrent.title) || ""
      this.titleService.setTitle(title + " - Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com");
      console.log(data);
      let listing = data.listingCurrent;
      this.metaCategories = data.metaCategories;
      var filelist = data.pictures || []
      filelist.forEach(element => {
        element.uid = this.uuidv4()
      });

      var categoryID = listing.categoryID;
      var control = this.controls.find(c => c.key == "images");
      if (control) {
        (control as ControlUpload).fileList = filelist
      }
      var controls: Array<ControlBase<any>> = [
        new ControlCheckboxListNew({
          key: 'listingType',
          label: 'Loại tin',
          options: this.listingTypes,

          nzLabelProperty: 'name',
          nzValueProperty: 'id',
          required: true,
          order: this.controlIndex++
        }),
        new ControlCascader({
          key: 'regions',
          label: 'Tỉnh, thành phố',
          options: this.regionsTree,

          nzLabelProperty: 'name',
          nzValueProperty: 'id',
          required: true,
          order: this.controlIndex++
        }),
        new ControlCascader({
          key: 'categories',
          // value:[42, 20],
          label: 'Danh mục tin đăng',
          options: this.categoriesTree,
          nzLabelProperty: 'name',
          nzValueProperty: 'id',
          required: true,
          onChange: this.categorySubject,
          order: this.controlIndex++
        }),
        new ControlTextbox({
          key: 'title',
          label: 'Tiêu đề',
          placeholder: 'Tiêu đề',
          value: '',
          required: true,
          order: this.controlIndex++
        }),
        new ControlTextarea({
          key: 'description',
          label: 'Nội dung',
          placeholder: 'Nội dung',
          value: '',
          required: true,
          order: this.controlIndex++
        }),
        new ControlPrice({
          key: 'price',
          label: 'Gía',
          placeholder: 'Giá',
          value: 0,
          required: true,
          order: this.controlIndex++
        }),
        new ControlUpload({
          key: 'images',
          label: 'Hình ảnh',
          fileList: [],
          required: false,
          order: this.controlIndex++
        }),
        new ControlTextbox({
          key: 'phone',
          label: 'Số điện thoại',
          placeholder: 'Số điện thoại',
          value: '',
          type: 'text',
          required: true,
          order: this.controlIndex++
        }),
        new ControlTextbox({
          key: 'location',
          label: 'Địa chỉ',
          placeholder: 'Địa chỉ',
          value: '',
          type: 'text',
          required: false,
          order: this.controlIndex++
        }),
        new ControlTextbox({
          key: 'id',
          label: '',
          placeholder: '',
          value: '',
          type: 'hidden',
          required: false,
          order: this.controlIndex++
        })
      ];
      var seen = new Set();
      this.metaCategories.forEach(element => {
        var k = element.metaField.id;
        if (!seen.has(k) && element.categoryID == categoryID) {
          var formItem = this.buildFormControl(element.metaField);
          //hide form element if not for selected category
          // formItem.isHidden = element.categoryID != categoryID;
          controls.push(formItem);
          seen.add(k);
        }
      });
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
      });

      if (id) {
        //update from for this item category
        this.onCategoryChange([listing.category.parent, listing.categoryID]);
        //publish data to form
        var data: any = {
          'listingType': listing.listingTypeID,
          'regions': [listing.regionId, listing.areaId],
          'categories': [listing.category.parent, listing.categoryID],
          'title': listing.title,
          'description': listing.description,
          'price': listing.price.toString(),
          'images': filelist,
          'phone': listing.contactPhone,
          'location': listing.location,
          'id': listing.id
        };
        //add custom fiels values
        seen = new Set();
        this.metaCategories.forEach(element => {
          var k = element.metaField.id;
          if (!seen.has(k) && element.categoryID == categoryID) {
            var value = listing.listingMetas.find(m => m.fieldID == k);
            if (value) {
              data[k] = JSON.parse(value.value);
            }
            seen.add(k);
          }
        });

        this.data = data;
      }
    });
  }

  onCategoryChange(categories: any[]) {
    if (categories.length != 2) {
      return;
    }
    var categoryID = categories[1]
    var seen = new Set();
    
    //remove existing custom fields
    this.controls.forEach((element, index) => {
      if ((element as ControlBase<any>).isCustomField) {
        this.controls.splice(index, 1);
      }
    });
    var controls = [...this.controls];
    this.metaCategories.forEach(element => {
      var k = element.metaField.id;
      if (!seen.has(k) && element.categoryID == categoryID) {
        var formItem = this.buildFormControl(element.metaField);
        controls.push(formItem);
        seen.add(k);
      }
    });
    this.controls = controls;
  }

  buildFormControl(meta): ControlBase<any> {
    switch (meta.controlTypeID) {
      case 0:
        return new ControlDropdown({
          key: meta.id.toString(),
          label: meta.name,
          placeholder: meta.placeholder,
          required: meta.required,
          isCustomField: true,
          options: JSON.parse(meta.options).map(o => { return { key: o, value: o } }),
          order: this.controlIndex++
        });
      case 1:
        return new ControlRadioList({
          key: meta.id.toString(),
          label: meta.name,
          required: meta.required,
          isCustomField: true,
          options: JSON.parse(meta.options).map(o => { return { key: o, value: o } }),
          order: this.controlIndex++
        });
      case 2:
        return new ControlCheckboxList({
          key: meta.id.toString(),
          label: meta.name,
          required: meta.required,
          isCustomField: true,
          options: JSON.parse(meta.options).map(o => { return { key: o, value: o } }),
          order: this.controlIndex++
        });
      case 3:
        return new ControlTextbox({
          key: meta.id.toString(),
          label: meta.name,
          placeholder: meta.placeholder,
          required: meta.required,
          isCustomField: true,
          order: this.controlIndex++
        });
      default:
        return new ControlTextarea({
          key: meta.id.toString(),
          label: meta.name,
          placeholder: meta.placeholder,
          required: meta.required,
          isCustomField: true,
          order: this.controlIndex++
        });
    }
  }

  ngOnInit() {


  }
  uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
  formSumit(event) {
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
    listingModel.location = event.location;

    var categoryID = event.categories[1]
    var customFields = [];
    var seen = new Set();
    this.metaCategories.forEach(element => {
      var k = element.metaField.id;
      if (!seen.has(k) && element.categoryID == categoryID) {
        customFields.push({ metaId: Number(k), value: JSON.stringify(event[k]) });
        seen.add(k);
      }
    });
    listingModel.customFields = customFields;
    this.listingService.apiListingListingUpdatePost(listingModel).subscribe(res => {
      if (res.success) {
        this.router.navigateByUrl("/");
      }
    });
  }

}
