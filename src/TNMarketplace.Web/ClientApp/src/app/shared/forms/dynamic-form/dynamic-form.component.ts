import { Component, Input, Output, EventEmitter, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { FormGroup, NgForm, FormControl, Validators, ValidatorFn } from '@angular/forms';
import { Subject } from 'rxjs';

import { FormControlService } from '../form-control.service';
import { ControlBase } from '../controls/control-base';
import { ValidationService } from '../validation.service';

@Component({
    selector: 'appc-dynamic-form',
    styleUrls: ['./dynamic-form.component.scss'],
    templateUrl: './dynamic-form.component.html'
})
export class DynamicFormComponent implements OnInit, OnDestroy {
    private sortedControls: Array<ControlBase<any>> = [];

    private _controls: Array<ControlBase<any>> = [];
    private existingControls: Array<ControlBase<any>> = [];
    private _data: any;
    @Input() public set data(value){
        if (this._data != value){
            this._data = value;
            if (this.sortedControls){
                this.formatDateToDisplay(this.data, this.sortedControls);
            }
            if (this.form){
                this.form.patchValue(this.data);
            }
        }
    }
    public get data(){
        return this._data;
    }
    @Input() public set controls(value: Array<ControlBase<any>>){
        if(this._controls == value || value == null || value.length == 0){
            return;
        }
        this.existingControls = this._controls;
        this._controls = value;
        this.sortedControls = this.controls.sort((a, b) => a.order - b.order);
        this.toControlGroup(this.sortedControls);
        if (this.data) {
            this.formatDateToDisplay(this.data, this.sortedControls);
            this.form.patchValue(this.data);
        }
    }
    public get controls(){
        return this._controls;
    }
    @Input() public reset = new Subject<boolean>();
    @Input() public btnText = 'Save'; // Default value at least
    @Input() public cancelText = 'Cancel'; // Default value at least
    @Input() public displayCancel = false; // By default cancel button will be hidden

    @Input() public formClass = 'form-horizontal';
    // Note: don't keep name of output events as same as native events such as submit etc.
    @Output() public onSubmit: EventEmitter<any> = new EventEmitter<any>();
    @Output() public onCancel: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('formDir') public formDir: NgForm;
    public form: FormGroup;

    constructor(public _controlService: FormControlService) { }

    public ngOnInit() {
        this.sortedControls = this.controls.sort((a, b) => a.order - b.order);
        this.toControlGroup(this.sortedControls);

        // if (this.data) {
        //     this.data.subscribe(model => {
        //         if (model) {
        //             this.formatDateToDisplay(model, this.sortedControls);
        //             this.form.patchValue(model);
        //         }
        //     });
        // }
        // this.form = new FormGroup({});
        if (this.data) {
            this.formatDateToDisplay(this.data, this.sortedControls);
            this.form.patchValue(this.data);
        }
        this.reset.subscribe(reset => {
            if (reset) {
                this.formDir.resetForm();
            }
        });

    }

    public submit() {
        if (this.form.valid) {
            this.formatDateToSave(this.form);
            this.onSubmit.emit(this.form.value);
        }
    }
    public cancel() {
        this.onCancel.next();
    }

    private formatDateToDisplay(model: any, controls: Array<ControlBase<any>>) {
        const dateField = controls.filter(x => x.type === 'calendar');
        if (dateField && dateField.length > 0) {
            for (const control of dateField) {
                const controlKey = control.key;
                const stringDate = model[controlKey];
                const date = new Date(stringDate);
                model[controlKey] = { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() };
            }
        }
    }

    private formatDateToSave(form: FormGroup) {
        const dateField = this.sortedControls.filter(x => x.type === 'calendar');
        if (dateField && dateField.length > 0) {
            for (const control of dateField) {
                const controlKey = control.key;
                const objectDate = this.form.value[controlKey];
                const date = new Date(objectDate.year, objectDate.month - 1, objectDate.day);
                this.form.value[controlKey] = date;
            }
        }
    }

    public toControlGroup(controls: Array<ControlBase<any>>) {
        const group: any = {};

        //remove existing custom fields
        if (this.form){
        Object.keys(this.form.controls).forEach(key=>{
            var existingControl = this.existingControls.find(c=> c.key == key && c.isCustomField == true);
            if(existingControl){
                this.form.removeControl(key);
            }
        });
    }
        controls.forEach(control => {
            //no need to re-add base input fields
            var existingControl = this.existingControls.find(c=> c.key == control.key);
            if (existingControl){
                return;
            }
            const validators: ValidatorFn[] = [];
            // Required
            if (control.required) {
                validators.push(Validators.required);
            }
            // Minlength
            if (control.minlength) {
                validators.push(Validators.minLength(control.minlength));
            }
            // Maxlength
            if (control.maxlength) {
                validators.push(Validators.maxLength(control.maxlength));
            }
            // Email
            if (control.type === 'email') {
                validators.push(Validators.email);
            }
            // Password
            if (control.type === 'password' && control.required) {
                validators.push(ValidationService.passwordValidator);
            }
            group[control.key] = new FormControl(control.value || '', validators);
        });
        if (this.form && Object.keys(this.form.controls).length > 0){
            Object.keys(group).forEach(key=>{
                this.form.addControl(key, group[key]);
            })
        }else{
            this.form = new FormGroup(group, ValidationService.passwordMatchValidator);
        }
    }

    ngOnDestroy() {
        this.reset.unsubscribe();
    }
}
