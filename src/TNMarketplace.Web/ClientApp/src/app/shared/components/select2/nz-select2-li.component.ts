import { ChangeDetectionStrategy, Component, Input, SecurityContext, ViewEncapsulation } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Select2Option } from './types';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation  : ViewEncapsulation.None,
  selector       : '[nz-select2-option]',
  templateUrl    : './nz-select2-li.component.html',
  host           : {
    '[attr.title]'                           : 'option.title || getOptionLabel()',
    '[class.ant-cascader-menu-item]'         : 'true',
    '[class.ant-cascader-menu-item-active]'  : 'activated',
    '[class.ant-cascader-menu-item-expand]'  : '!option.isLeaf',
    '[class.ant-cascader-menu-item-disabled]': 'option.disabled'
  }
})
export class NzSelect2OptionComponent {
  @Input() option: Select2Option;
  @Input() activated = false;
  @Input() highlightText: string;
  @Input() nzLabelProperty = 'label';

  constructor(private sanitizer: DomSanitizer) {}

  getOptionLabel(): string {
    return this.option ? this.option[ this.nzLabelProperty ] : '';
  }

  isBack(): boolean{
    return (this.option.parent || {}).id == this.option.id;
  }

  renderHighlightString(str: string): string {
    const safeHtml = this.sanitizer.sanitize(SecurityContext.HTML, `<span class="ant-cascader-menu-item-keyword">${this.highlightText}</span>`);
    if (!safeHtml) {
      throw new Error(`[NG-ZORRO] Input value "${this.highlightText}" is not considered security.`);
    }
    return str.replace(new RegExp(this.highlightText, 'g'), safeHtml);
  }
}
