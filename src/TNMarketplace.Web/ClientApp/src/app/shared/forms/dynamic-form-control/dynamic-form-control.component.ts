import { Component, Input } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';

import { ValidationService } from '../validation.service';
import { ControlBase } from '../controls/control-base';

@Component({
    selector: 'appc-dynamic-control',
    templateUrl: './dynamic-form-control.component.html',
    styleUrls: ['./dynamic-form-control.component.scss']
})
export class DynamicFormControlComponent {
    @Input() public control: ControlBase<string | boolean>;
    @Input() public form: FormGroup;
    @Input() public frm: NgForm;
    constructor() { }

    get valid() {
        var c = this.form.controls[this.control.key];
        if (!c){
            return false;
        }
        return this.form.controls[this.control.key].valid && (this.form.controls[this.control.key].touched || this.frm.submitted);
    }

    get invalid() {
        // console.log(this.control.key);
        var c = this.form.controls[this.control.key];
        if (!c){
            return true;
        }
        return !this.form.controls[this.control.key].valid && (this.form.controls[this.control.key].touched || this.frm.submitted);
    }

    errorMessage(control: ControlBase<any>): string {
        // valid || (pristine && !submitted)
        const c: any = this.form.get(this.control.key);
        if (c) {
            for (const propertyName in c.errors) {
                if (c.errors.hasOwnProperty(propertyName) && (c.touched || this.frm.submitted)) {
                    return ValidationService.getValidatorErrorMessage(propertyName, this.control.minlength || this.control.maxlength);
                }
            }
        }
        return '';
    }
}
