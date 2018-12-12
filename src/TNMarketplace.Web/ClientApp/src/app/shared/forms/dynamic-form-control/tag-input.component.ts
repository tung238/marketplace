import { Component, OnInit, ViewChild, ElementRef, OnChanges, AfterViewInit, OnDestroy, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'appc-tag-input',
  template: `
    <div class="clear-fix"></div>
    <nz-tag
      *ngFor="let tag of tags; let i = index;"
      [nzMode]="'closeable'"
      (nzAfterClose)="handleClose(tag)">
      {{ sliceTagName(tag) }}
    </nz-tag>
    <nz-tag
      *ngIf="!inputVisible"
      class="editable-tag"
      (click)="showInput()">
      <i nz-icon type="plus"></i> New Tag
    </nz-tag>
    <input
      #inputElement
      nz-input
      nzSize="small"
      *ngIf="inputVisible" type="text"
      [(ngModel)]="inputValue"
      style="width: 78px;"
      (blur)="handleInputConfirm()"
      (keydown.enter)="handleInputConfirm()">
  `,
  styles: [
    `.editable-tag ::ng-deep .ant-tag {
      background: rgb(255, 255, 255);
      border-style: dashed;
    }`],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TagInputComponent),
      multi: true
    }
  ],
})
export class TagInputComponent implements OnInit, ControlValueAccessor, OnChanges, AfterViewInit, OnDestroy {
  // tslint:disable-next-line:no-any
  onChange: (value: any) => void = () => null;
  // tslint:disable-next-line:no-any
  onTouched: () => any = () => null;

  @Input() tags = [];

  inputVisible = false;
  inputValue = '';

  @ViewChild('inputElement')
  inputElement: ElementRef;


  ngOnInit() {

  }

  ngOnChanges() {

  }
  ngOnDestroy() {

  }
  ngAfterViewInit() {

  }

  registerOnChange(fn: (_: boolean) => {}): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => {}): void {
    this.onTouched = fn;
  }

  writeValue(value: any[]) {
    this.tags = value;
  }

  handleClose(removedTag: {}): void {
    this.tags = this.tags.filter(tag => tag !== removedTag);
    this.onChange(this.tags);
  }

  sliceTagName(tag: string): string {
    const isLongTag = tag.length > 20;
    return isLongTag ? `${tag.slice(0, 20)}...` : tag;
  }

  showInput(): void {
    this.inputVisible = true;
    setTimeout(() => {
      this.inputElement.nativeElement.focus();
    }, 10);
  }

  handleInputConfirm(): void {
    if (this.inputValue && this.tags.indexOf(this.inputValue) === -1) {
      this.tags.push(this.inputValue);
      this.onChange(this.tags);
    }
    this.inputValue = '';
    this.inputVisible = false;
  }

}
