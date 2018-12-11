import { ControlBase } from './control-base';

export class ControlSelect extends ControlBase<string> {
    public options: Array<{ key: string, value: string }> = [];

    constructor(options: any = {}) {
        super(options);
        this.type = 'select';
        this.options = options.options || [];
    }
}
