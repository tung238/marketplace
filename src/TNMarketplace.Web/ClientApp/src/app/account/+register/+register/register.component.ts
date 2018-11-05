import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { ControlBase } from '../../../shared/forms/controls/control-base';
import { ControlTextbox } from '../../../shared/forms/controls/control-textbox';
import { DataService } from '../../../core/services/data.service';


@Component({
    selector: 'appc-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
    public controls: Array<ControlBase<any>>;

    constructor(
        public dataService: DataService,
        public router: Router,
        public route: ActivatedRoute
    ) { }

    public register(model: IRegisterModel): void {
        this.dataService.post('api/account/register', model)
            .subscribe(() => {
                this.router.navigate(['/dang-nhap'], { relativeTo: this.route, queryParams: { emailConfirmed: true } });
            });
    }

    public ngOnInit() {
        const controls: Array<ControlBase<any>> = [
            new ControlTextbox({
                key: 'email',
                label: 'Email',
                placeholder: 'Email',
                value: '',
                type: 'email',
                required: true,
                order: 1
            }),
            new ControlTextbox({
                key: 'password',
                label: 'Mật khẩu',
                placeholder: 'Mật khẩu',
                value: '',
                type: 'password',
                required: true,
                order: 2
            }),
            new ControlTextbox({
                key: 'confirmPassword',
                label: 'Nhập lại mật khẩu',
                placeholder: 'Nhập lại mật khẩu',
                value: '',
                type: 'password',
                required: true,
                order: 3
            }),
            new ControlTextbox({
                key: 'firstname',
                label: 'Họ',
                placeholder: 'Họ',
                value: '',
                type: 'text',
                required: true,
                order: 4
            }),
            new ControlTextbox({
                key: 'lastname',
                label: 'Tên',
                placeholder: 'Tên',
                value: '',
                type: 'text',
                required: true,
                order: 5
            }), 
            new ControlTextbox({
                key: 'mobile',
                label: 'Số điện thoại',
                placeholder: 'Số điện thoại',
                value: '',
                type: 'text',
                required: true,
                order: 6
            })
        ];

        this.controls = controls;
    }

}
