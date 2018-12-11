import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicFormComponent } from '@app/shared/forms/dynamic-form/dynamic-form.component';
import { ControlBase } from '@app/shared/forms/controls/control-base';
import { Subject } from 'rxjs';
import { AdminListingService } from '@app/api';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';
import { ControlTextbox } from '@app/shared/forms/controls/control-textbox';
import { ControlTextarea } from '@app/shared/forms/controls/control-textarea';
import { ControlDropdown } from '@app/shared/forms/controls/control-dropdown';
import {ControlSelect} from '@app/shared/forms/controls/control-select';
import { ControlCheckbox } from '@app/shared/forms/controls/control-checkbox';

@Component({
  selector: 'appc-meta-field-detail',
  templateUrl: './meta-field-detail.component.html',
  styleUrls: ['./meta-field-detail.component.scss']
})
export class MetaFieldDetailComponent implements OnInit {
  headerText: string;
  ngAfterViewInit(): void {
    var id = this.route.snapshot.params['id'];
    if (id) {
      this.adminListingService.apiAdminAdminListingCustomFieldGet(id).subscribe(cc => {
        const data = cc.category;
        let title = (data.name) || ""
        this.titleService.setTitle(title + " - Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com");
        console.log(data);
        this.data.next({
          'id': data.id,
          'controlTypeID': data.controlTypeID,
          'name': data.name,
          'options': data.options,
          'required': data.parent,
          'searchable': data.iconClass,
          'categories': data.categories
        });
      })
    }
  }
  public controls: Array<ControlBase<any>> = [];
  data: Subject<any> = new Subject<any>();

  @ViewChild(DynamicFormComponent)
  public ngForm: DynamicFormComponent;

  formSumit(data){
    this.adminListingService.apiAdminAdminListingCustomFieldUpdatePost(data).subscribe(c=>{
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
        label: 'Tên thuộc tính',
        placeholder: 'Tên thuộc tính',
        value: '',
        required: true,
        order: 0
      }),
      new ControlDropdown({
        key: 'controlTypeID',
        label: 'Kiểu',
        placeholder: 'Chọn kiểu',
        value: '',
        required: true,
        order: 1,
        options: [
          {id: 0, value: 'Dropdown list'},
          {id: 1, value: 'Radio list'},
          {id: 2, value: 'Checkbox list'},
          {id: 3, value: 'Textbox'},
          {id: 4, value: 'Multiline texbox'},
        ]
      }),
      new ControlTextarea({
        key: 'options',
        label: 'Lựa chọn',
        placeholder: 'Lựa chọn',
        value: '',
        required: true,
        order: 2
      }),
      new ControlCheckbox({
        key: 'required',
        label: 'Required',
        value: true,
        required: false,
        order: 3
      }),
      new ControlCheckbox({
        key: 'searchable',
        label: 'Searchable',
        value: true,
        required: false,
        order: 4
      }),
      new ControlSelect({
        key: 'categories',
        label: 'Danh mục',
        value: 0,
        required: true,
        order: 5
      }),
      new ControlTextbox({
        key: 'id',
        label: '',
        value: '',
        type: 'hidden',
        required: false,
        order: 6
      })
    ];
    const id = this.route.snapshot.params['id'];
    this.headerText = id ? "Sửa thuộc tính": "Thêm mới thuộc tính";
    this.controls = controls;

    this.adminListingService.apiAdminAdminListingCategoriesGet().subscribe(data => {
      var control = this.controls.find(c => c.key == "categories");
      if (control) {
        (control as ControlSelect).options = data.filter(c=>{
          return c.parent != 0;
        }).map(c => { return { key: c.id, value: c.name } });
      }
    })
  }

}
