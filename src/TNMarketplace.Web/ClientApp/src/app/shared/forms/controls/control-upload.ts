import { ControlBase } from './control-base';

export class ControlUpload extends ControlBase<string> {
    fileList: any[];

    constructor(options: any = {}) {
        super(options);
        this.type = 'upload';
        this.fileList = options.fileList;
    }

    fileListChange(event){
        if (event.type == 'success'){
            var file = this.fileList.find(w => w.uid === event.file.uid);
            file.url = file.response.url;
            // file.thumbUrl = file.response.thumbUrl;
        }
        console.log(event);
    }

}
