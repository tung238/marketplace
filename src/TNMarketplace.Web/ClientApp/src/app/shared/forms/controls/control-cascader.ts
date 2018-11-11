import { ControlBase } from './control-base';

export class ControlCascader extends ControlBase<string> {
    public options: Array<{ key: string, value: string }> = [];
    public nzLabelProperty: string = 'label';
    public nzValueProperty: string = 'value';
    
    constructor(options: any = {}) {
        super(options);
        this.type = 'cascader';
        
        this.options = options.options || [];
        this.nzLabelProperty = options.nzLabelProperty;
        this.nzValueProperty = options.nzValueProperty;
    }
}
