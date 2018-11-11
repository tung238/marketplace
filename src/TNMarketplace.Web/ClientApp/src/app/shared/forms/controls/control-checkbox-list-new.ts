import { ControlBase } from './control-base';
import { NgForm } from '@angular/forms';

export class ControlCheckboxListNew extends ControlBase<string> {
    public type: string;
    public options: any[];
    constructor(options: any = {}) {
        super(options);
        this.type = 'checkboxlistnew';
        this.value = options.value;
        this.options = options.options;
    }

    // public updateCheckedOptions(option: any, form: NgForm) {
    //     const val: string[] = form.value[this.options.key];
    //     // Check if item already exists, then remove it
    //     if (val.indexOf(option) > -1) {
    //         val.splice(val.indexOf(option), 1);
    //     } else {
    //         val.push(option);
    //     }
    // }

}
