import { Component, OnInit, forwardRef, OnChanges, AfterViewInit, OnDestroy, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

@Component({
  selector: 'appc-checkbox-list',
  template: `
  <div>
  <label *ngFor="let item of control.options.options">
      <input class="custom-control-input" type="checkbox" [id]="item.key" [value]="item.key" [checked]="checkedList.indexOf(item.key)> -1"
          (change)="updateCheckedOptions(item.key, $event)"> {{item.value}}
  </label>
</div>
  `,
  styles: [],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CheckboxListComponent),
      multi: true
    }
  ],
})
export class CheckboxListComponent implements OnInit, ControlValueAccessor, OnChanges, AfterViewInit, OnDestroy {

  @Input()
  control: any;
  checkedList: any[] = [];
  // tslint:disable-next-line:no-any
  onChange: (value: any) => void = () => null;
  // tslint:disable-next-line:no-any
  onTouched: () => any = () => null;

  ngOnDestroy(): void {
  }
  ngAfterViewInit(): void {
  }
  registerOnChange(fn: (_: boolean) => {}): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => {}): void {
    this.onTouched = fn;
  }

  writeValue(value: any[]) {
    
    this.checkedList = Array.isArray(value) ? value : [];
  }
  ngOnChanges() {

  }

  setDisabledState?(isDisabled: boolean): void {
  }

  constructor() { }

  ngOnInit() {
  }

  public updateCheckedOptions(option: any) {
    // Check if item already exists, then remove it
    if (this.checkedList.indexOf(option) > -1) {
      this.checkedList.splice(this.checkedList.indexOf(option), 1);
    } else {
      this.checkedList.push(option);
    }
    this.onChange(this.checkedList);
  }
}
