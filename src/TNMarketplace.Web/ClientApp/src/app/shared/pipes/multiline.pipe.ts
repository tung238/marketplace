import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'multiline'
})
export class MultilinePipe implements PipeTransform {

  transform(text: any, args?: any): any {
    if (!text) {
      return text;
    }
    var value = text.toString().replace(/(?:\r\n|\r|\n|\\n)/g, '\n');

    return value;
  }

}
