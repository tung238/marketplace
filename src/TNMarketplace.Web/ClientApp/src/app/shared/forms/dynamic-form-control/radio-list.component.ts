import { Component, OnInit, forwardRef, OnChanges, AfterViewInit, OnDestroy, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

@Component({
  selector: 'appc-radio-list',
  template: `
  <div>
  <label *ngFor="let item of control.options.options">
      <input type="radio" [id]="item.key" [value]="item.key" [name]="control.label" [(ngModel)]="checkedItem">
        <span>{{item.key}}</span>
  </label>
</div>
  `,
  styles: [],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => RadioListComponent),
      multi: true
    }
  ],
})
export class RadioListComponent implements OnInit, ControlValueAccessor, OnChanges, AfterViewInit, OnDestroy{
  @Input()
  control: any;
  checkedItem: any;
   // tslint:disable-next-line:no-any
   onChange: (value: any) => void = () => null;
   // tslint:disable-next-line:no-any
   onTouched: () => any = () => null;

  ngOnDestroy(): void {
  }
  ngAfterViewInit(): void {

  }
  writeValue(obj: any): void {
    this.checkedItem = obj;
  }
  registerOnChange(fn: (_: boolean) => {}): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => {}): void {
    this.onTouched = fn;
  }
  ngOnChanges() {

  }
  setDisabledState?(isDisabled: boolean): void {
    
  }

  constructor() { }

  ngOnInit() {
  }

}
