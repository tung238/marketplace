import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phone'
})
export class PhonePipe implements PipeTransform {

  transform(tel: any, args?: any): any {
    if (!tel) {
      return tel;
    }
    return tel;
    // var value = tel.toString().trim().replace(/^\+/, '');

    // if (value.match(/[^0-9]/)) {
    //     return tel;
    // }

    // var country, city, number;

    // switch (value.length) {
    //     case 9: // +1PPP####### -> C (PPP) ###-####
    //         country = 1;
    //         city = "0" + value.slice(0, 2);
    //         number = value.slice(2);
    //         break;
    //     case 10: // +1PPP####### -> C (PPP) ###-####
    //         country = 1;
    //         city = value.slice(0, 3);
    //         number = value.slice(3);
    //         break;

    //     case 11: // +CPPP####### -> CCC (PP) ###-####
    //         country = 1;
    //         city = value.slice(0, 4);
    //         number = value.slice(4);
    //         break;

    //     case 12: // +CCCPP####### -> CCC (PP) ###-####
    //         country = value.slice(0, 3);
    //         city = value.slice(3, 5);
    //         number = value.slice(5);
    //         break;

    //     default:
    //         return tel;
    // }

    // if (country == 1) {
    //     country = "";
    // }

    // number = number.slice(0, 3) + '-' + number.slice(3);

    // return (country + " (" + city + ") " + number).trim();
  }

}
