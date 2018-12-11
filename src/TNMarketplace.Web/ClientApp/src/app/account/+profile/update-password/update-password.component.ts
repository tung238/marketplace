import { Component, OnInit, EventEmitter } from '@angular/core';
import { ProfileService } from '../profile.service';
import { UpdatePasswordModel } from '../profile.models';
import { ControlBase } from '../../../shared/forms/controls/control-base';
import { ControlTextbox } from '../../../shared/forms/controls/control-textbox';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'appc-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.scss'],
})
export class UpdatePasswordComponent implements OnInit {
  public controls: Array<ControlBase<string>> = [
    new ControlTextbox({
      key: 'oldPassword',
      label: 'Current password',
      placeholder: 'Current password',
      value: '',
      type: 'password',
      order: 1
    }),
    new ControlTextbox({
      key: 'newPassword',
      label: 'New password',
      placeholder: 'New password',
      value: '',
      type: 'password',
      required: true,
      order: 2
    }),
    new ControlTextbox({
      key: 'confirmPassword',
      label: 'Verify password',
      placeholder: 'Verify password',
      value: '',
      type: 'password',
      required: true,
      order: 3
    })
  ];

  public reset = new EventEmitter<boolean>();
  constructor(
    public profileService: ProfileService,
    private toastr: ToastrService,

  ) { }

  public ngOnInit() { }

  public save(model: UpdatePasswordModel): void {
    this.profileService.changePassword(model)
      .subscribe(() => {
        this.reset.emit(true);
        this.toastr.success('Thay đổi mật khẩu thành công.');
      });
  }
}
