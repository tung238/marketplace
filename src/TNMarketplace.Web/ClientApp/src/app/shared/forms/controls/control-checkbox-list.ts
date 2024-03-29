import { ControlBase } from './control-base';
import { NgForm } from '@angular/forms';

export class ControlCheckboxList extends ControlBase<string> {
    public type: string;

    constructor(private options: any = {}) {
        super(options);
        this.type = 'checkboxlist';
        this.value = options.value;
    }
}
