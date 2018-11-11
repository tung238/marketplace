import { AbstractControl } from "@angular/forms";

export class ValidationService {

    public static getValidatorErrorMessage(code: string, fieldLength: number | undefined) {
        const config: any = {
            required: 'Thông tin này là bắt buộc',
            minlength: 'Tối thiểu ' + fieldLength + ' ký tự',
            maxlength: 'Tối đa ' + fieldLength + ' ký tự',
            invalidCreditCard: 'Số tài khoản không đúng',
            email: 'Email không đúng',
            invalidPassword: 'Mật khẩu ít nhất 6 ký tự',
            passwordNotMatch: 'Xác nhận mật khẩu không đúng'
        };
        return config[code];
    }

    public static creditCardValidator(control: any) {
        // Visa, MasterCard, American Express, Diners Club, Discover, JCB
        if (control.value &&
            control.value.match(/^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/)) {
            return undefined;
        } else {
            return { invalidCreditCard: true };
        }
    }

    public static passwordValidator(control: any): any {
        // {6,100}           - Assert password is between 6 and 100 characters
        // (?=.*[0-9])       - Assert a string has at least one number
        if (control.value && control.value.match(/^[a-zA-Z0-9!"@#$%^&*]{6,100}$/)) {
            return undefined;
        } else {
            return { invalidPassword: true };
        }
    }

    public static passwordMatchValidator(control: AbstractControl): any {
        if (control.get('password') && control.get('confirmPassword')) {
            let password = control.get('password').value; // to get value in input tag
            let confirmPassword = control.get('confirmPassword').value; // to get value in input tag
            if (password != confirmPassword) {
                control.get('confirmPassword').setErrors({ passwordNotMatch: true })
            }
        }
        return null;
    }
}
