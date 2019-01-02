import { OverlayModule } from '@angular/cdk/overlay';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NzSelect2OptionComponent } from './nz-select2-li.component';
import { NzSelect2Component } from './nz-select2.component';
import { NgZorroAntdModule } from 'ng-zorro-antd';

@NgModule({
  imports     : [ CommonModule, FormsModule, OverlayModule, NgZorroAntdModule ],
  declarations: [
    NzSelect2Component,
    NzSelect2OptionComponent
  ],
  exports     : [
    NzSelect2Component
  ]
})
export class NzSelect2Module {
}
