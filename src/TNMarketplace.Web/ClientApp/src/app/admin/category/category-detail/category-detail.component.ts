import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { DynamicFormComponent } from '@app/shared/forms/dynamic-form/dynamic-form.component';
import { Subject } from 'rxjs';
import { ControlBase } from '@app/shared/forms/controls/control-base';
import { ControlTextarea } from '@app/shared/forms/controls/control-textarea';
import { ControlTextbox } from '@app/shared/forms/controls/control-textbox';
import { ControlDropdown } from '@app/shared/forms/controls/control-dropdown';
import { ControlCheckbox } from '@app/shared/forms/controls/control-checkbox';
import { ControlPrice } from '@app/shared/forms/controls/control-price';
import { AdminListingService } from '@app/api';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { ControlSelect } from '@app/shared/forms/controls/control-select';

@Component({
  selector: 'appc-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.scss']
})
export class CategoryDetailComponent implements OnInit, AfterViewInit {
  headerText: string;
  controls: Array<ControlBase<any>> = [
    new ControlTextbox({
      key: 'name',
      label: 'Tên danh mục',
      placeholder: 'Tên danh mục',
      value: '',
      required: true,
      order: 0
    }),
    new ControlTextarea({
      key: 'description',
      label: 'Nội dung',
      placeholder: 'Nội dung',
      value: '',
      required: true,
      order: 1
    }),
    new ControlTextbox({
      key: 'slug',
      label: 'Slug',
      placeholder: 'Slug',
      value: '',
      required: true,
      order: 2
    }),
    new ControlDropdown({
      key: 'parent',
      label: 'Danh mục cha',
      placeholder: 'Danh mục cha',
      value: 0,
      required: true,
      order: 3
    }),
    new ControlCheckbox({
      key: 'enabled',
      label: 'Enabled',
      value: true,
      required: true,
      order: 4
    }),
    new ControlTextbox({
      key: 'iconClass',
      label: 'Icon Class',
      placeholder: 'Icon Class',
      value: '',
      required: false,
      order: 5
    }),
    new ControlPrice({
      key: 'maxPrice',
      label: 'Giá tối đa',
      placeholder: 'Giá tối đa',
      value: 0,
      required: false,
      order: 6
    }),
    new ControlPrice({
      key: 'ordering',
      label: 'Thứ tự',
      placeholder: 'Thứ tự',
      type: 'number',
      value: 0,
      required: true,
      order: 7
    }),
    new ControlSelect({
      key: 'priceRanges',
      label: 'Khoảng giá',
      value: [],
      required: false,
      placeholder: 'Khoảng giá tìm kiếm',
      order: 8,
      options: [
        {key: "1", value: "< 1 triệu"},
        {key: "2", value: "1-3 triệu"},
        {key: "3", value: "3-5 triệu"},
        {key: "4", value: "5-10 triệu"},
        {key: "5", value: "10-15 triệu"},
        {key: "6", value: "15-20 triệu"},
        {key: "7", value: "20-40 triệu"},
        {key: "8", value: "40-70 triệu"},
        {key: "9", value: "70-100 triệu"},
        {key: "10", value: "100-300 triệu"},
        {key: "11", value: "300-500 triệu"},
        {key: "12", value: "500-800 triệu"},
        {key: "13", value: "800 triệu - 1 tỷ"},
        {key: "14", value: "1-2 tỷ"},
        {key: "15", value: "2-3 tỷ"},
        {key: "16", value: "3-5 tỷ"},
        {key: "17", value: "5-7 tỷ"},
        {key: "18", value: "7-10 tỷ"},
        {key: "19", value: "> 10 tỷ"}
      ]
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
  ngAfterViewInit(): void {
    var id = this.route.snapshot.params['id'];
    if (id) {
      this.adminListingService.apiAdminAdminListingCategoryUpdateGet(id).subscribe(cc => {
        const data = cc.category;
        let title = (data.name) || ""
        this.titleService.setTitle(title + " - Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com");
        console.log(data);
        let ranges = (data.priceRanges || "").split(",").filter(entry => entry.trim() != '') || [];
        this.data = {
          'id': data.id,
          'name': data.name,
          'slug': data.slug,
          'description': data.description,
          'parent': data.parent,
          'iconClass': data.iconClass,
          'enabled': data.enabled,
          'maxPrice': data.maxPrice,
          'ordering': data.ordering,
          'priceRanges': ranges
        };
      })
    }
  }
  data: any;

  @ViewChild(DynamicFormComponent)
  public ngForm: DynamicFormComponent;

  formSumit(data){
    data.maxPrice = Number(data.maxPrice);
    data.priceRanges = data.priceRanges.join(",");
    this.adminListingService.apiAdminAdminListingCategoryUpdatePost(data).subscribe(c=>{
      this.toastr.success('Cập nhật thành công.');
      this.router.navigate(["/admin/danh-muc"]);
    })
  }

  constructor(
    private route: ActivatedRoute,
    private adminListingService: AdminListingService,
    private toastr: ToastrService,
    private router: Router,
    private titleService: Title
  ) { }

  ngOnInit() {
    
    const id = this.route.snapshot.params['id'];
    this.headerText = id ? "Sửa danh mục": "Thêm mới danh mục";
    this.adminListingService.apiAdminAdminListingCategoriesGet().subscribe(data => {
      var control = this.controls.find(c => c.key == "parent");
      if (control) {
        (control as ControlDropdown).options = data.filter(c=>{
          return c.id != id && c.parent == 0;
        }).map(c => { return { key: c.id, value: c.name } });
      }
    })
  }

}
