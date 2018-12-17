import { ControlBase } from './control-base';
import { Subject } from 'rxjs';

export class ControlCascader extends ControlBase<string> {
    public options: Array<{ key: string, value: string }> = [];
    public nzLabelProperty: string = 'label';
    public nzValueProperty: string = 'value';
    public onChange: Subject<any>;
    constructor(options: any = {}) {
        super(options);
        this.type = 'cascader';
        
        this.options = options.options || [];
        this.nzLabelProperty = options.nzLabelProperty;
        this.nzValueProperty = options.nzValueProperty;
        this.onChange = options.onChange;
    }

    public onSelectionChange(value: any){
        if (value && this.onChange){
            this.onChange.next(value.map(c=>c.id));
        }
        console.log(value);
    }
}
