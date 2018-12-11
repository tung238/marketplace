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

@Component({
  selector: 'appc-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.scss']
})
export class CategoryDetailComponent implements OnInit, AfterViewInit {
  headerText: string;
  ngAfterViewInit(): void {
    var id = this.route.snapshot.params['id'];
    if (id) {
      this.adminListingService.apiAdminAdminListingCategoryUpdateGet(id).subscribe(cc => {
        const data = cc.category;
        let title = (data.name) || ""
        this.titleService.setTitle(title + " - Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com");
        console.log(data);
        this.data.next({
          'id': data.id,
          'name': data.name,
          'slug': data.slug,
          'description': data.description,
          'parent': data.parent,
          'iconClass': data.iconClass,
          'enabled': data.enabled,
          'maxPrice': data.maxPrice,
          'ordering': data.ordering,
        });
      })
    }
  }
  public controls: Array<ControlBase<any>> = [];
  data: Subject<any> = new Subject<any>();

  @ViewChild(DynamicFormComponent)
  public ngForm: DynamicFormComponent;

  formSumit(data){
    data.maxPrice = Number(data.maxPrice);
    this.adminListingService.apiAdminAdminListingCategoryUpdatePost(data).subscribe(c=>{
      this.toastr.success('Cập nhật thành công.');
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
    const controls: Array<ControlBase<any>> = [
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
      new ControlTextbox({
        key: 'id',
        label: '',
        placeholder: '',
        value: '',
        type: 'hidden',
        required: false,
        order: 7
      })
    ];
    const id = this.route.snapshot.params['id'];
    this.headerText = id ? "Sửa danh mục": "Thêm mới danh mục";
    this.controls = controls;
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
