import { Component, OnInit } from '@angular/core';
import { UserInfoModel } from '../profile.models';
import { ProfileService } from '../profile.service';
import { ControlBase } from '../../../shared/forms/controls/control-base';
import { ControlTextbox } from '../../../shared/forms/controls/control-textbox';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'appc-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
  data: any;
  public controls: Array<ControlBase<string>> = [
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
    })
  ];

  constructor(
    public profileService: ProfileService,
    private toastr: ToastrService,

  ) { }

  public ngOnInit() {
    this.profileService.userInfo().subscribe(data=>{
      this.data = data;
    })
   }

  public save(model: UserInfoModel): void {
    this.profileService.userInfo(model)
      .subscribe((res: UserInfoModel) => {
        this.toastr.success(`Cập nhật thông tin cá nhân thành công.`);
      });

  }

}
