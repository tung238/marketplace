import { ControlBase } from './control-base';

export class ControlPrice extends ControlBase<string> {
    constructor(options: any = {}) {
        super(options);
        this.type = options.type || 'price';
    }
}
