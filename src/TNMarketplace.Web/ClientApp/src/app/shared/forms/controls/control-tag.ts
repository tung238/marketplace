import { ControlBase } from './control-base';

export class ControlTag extends ControlBase<string> {
    public options: Array<{ key: string, value: string }> = [];
    constructor(options: any = {}) {
        super(options);
        this.type = 'tag';
        this.options = options.options || [];
    }
}
