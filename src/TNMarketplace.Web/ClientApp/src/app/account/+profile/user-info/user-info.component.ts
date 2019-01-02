import { Component, OnInit } from '@angular/core';
import { UserInfoModel } from '../profile.models';
import { ProfileService } from '../profile.service';
import { ControlBase } from '../../../shared/forms/controls/control-base';
import { ControlTextbox } from '../../../shared/forms/controls/control-textbox';
import { ToastrService } from 'ngx-toastr';
import { ControlCascader } from '@app/shared/forms/controls/control-cascader';
import { AppService } from '@app/app.service';
import { ControlCheckbox } from '@app/shared/forms/controls/control-checkbox';

@Component({
  selector: 'appc-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
  data: any;
  public controls: Array<ControlBase<any>> = [
    new ControlTextbox({
      key: 'firstName',
      label: 'First name',
      placeholder: 'Firstname',
      value: '',
      type: 'text',
      required: true,
      order: 1
    }),
    new ControlTextbox({
      key: 'lastName',
      label: 'Last name',
      placeholder: 'Lastname',
      value: '',
      type: 'text',
      required: true,
      order: 2
    }),
    new ControlTextbox({
      key: 'phoneNumber',
      label: 'Phone number',
      placeholder: 'Phone number',
      value: '',
      type: 'text',
      required: false,
      order: 3
    }),
    new ControlCheckbox({
      key: 'isBroker',
      label: 'Môi giới?',
      value: false,
      required: false,
      order: 4
    }),
    new ControlCascader({
      key: 'regions',
      label: 'Tỉnh, thành phố',
      nzLabelProperty: 'name',
      nzValueProperty: 'id',
      required: false,
      order: 5
    }),
    new ControlTextbox({
      key: 'location',
      label: 'Địa chỉ',
      placeholder: 'Địa chỉ',
      value: '',
      type: 'text',
      required: false,
      order: 6
    })
  ];

  constructor(
    public profileService: ProfileService,
    private appService: AppService,
    private toastr: ToastrService,

  ) { }

  public ngOnInit() {
    this.profileService.userInfo().subscribe(data=>{
      if (data.areaId && data.regionId){
        data.regions = [data.regionId, data.areaId];
      }
      this.data = data;
    })
    let regions = this.appService.appData.regionsTree;
    var control = this.controls.find(c => c.key == "regions");
      if (control) {
        (control as ControlCascader).options = regions;
      }
   }

  public save(model: UserInfoModel): void {
    if (model.regions.length == 2){
      model.regionId = model.regions[0];
      model.areaId = model.regions[1];
    }
    this.profileService.userInfo(model)
      .subscribe((res: UserInfoModel) => {
        this.toastr.success(`Cập nhật thông tin cá nhân thành công.`);
      });

  }

}
