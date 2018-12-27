import { NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppSharedModule } from '../appshared';
// Components
import { SocialLoginComponent } from './components/social-login/social-login.component';
import { DynamicFormComponent } from './forms/dynamic-form/dynamic-form.component';
import { DynamicFormControlComponent } from './forms/dynamic-form-control/dynamic-form-control.component';
import { ErrorSummaryComponent } from './forms/error-summary/error-summary.component';
// Pipes
import { UppercasePipe } from './pipes/uppercase.pipe';
// Services
import { FormControlService } from './forms/form-control.service';
import { SubMenuComponent } from './components/sub-menu/sub-menu.component';
import { ProductGridRowComponent } from './components/product-grid-row/product-grid-row.component';
import { ProductListRowComponent } from './components/product-list-row/product-list-row.component';
import { PhonePipe } from './pipes/phone.pipe';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { MultilinePipe } from './pipes/multiline.pipe';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { CurrencyMaskDirective } from './directives/currency-mask.directive';
import { TagInputComponent } from './forms/dynamic-form-control/tag-input.component';
import { CheckboxListComponent } from './forms/dynamic-form-control/checkbox-list.component';
import { RadioListComponent } from './forms/dynamic-form-control/radio-list.component';
import { NzSelect2Module } from './components/select2/nz-select2.module';

@NgModule({
  imports: [
    CommonModule,
    NzSelect2Module,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    AppSharedModule,
    NgZorroAntdModule
    // No need to export as these modules don't expose any components/directive etc'
  ],
  declarations: [
    SocialLoginComponent,
    DynamicFormComponent,
    DynamicFormControlComponent,
    ErrorSummaryComponent,
    UppercasePipe,
    SubMenuComponent,
    ProductGridRowComponent,
    ProductListRowComponent,
    PhonePipe,
    MultilinePipe,
    BreadcrumbComponent,
    CurrencyMaskDirective,
    TagInputComponent,
    CheckboxListComponent,
    RadioListComponent
  ],
  exports: [
    // Modules
    CommonModule,
    FormsModule,
    NzSelect2Module,
    ReactiveFormsModule,
    AppSharedModule,
    // Providers, Components, directive, pipes
    SocialLoginComponent,
    DynamicFormComponent,
    DynamicFormControlComponent,
    ErrorSummaryComponent,
    SubMenuComponent,
    ProductGridRowComponent,
    ProductListRowComponent,
    UppercasePipe,
    BreadcrumbComponent,
    PhonePipe,
    MultilinePipe,
    NgZorroAntdModule
  ],
  providers: [
    FormControlService,
    CurrencyPipe
  ]

})
export class SharedModule { }
