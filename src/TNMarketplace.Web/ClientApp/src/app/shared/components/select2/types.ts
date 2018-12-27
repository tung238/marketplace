export type NzSelect2ExpandTrigger = 'click' | 'hover';
export type NzSelect2TriggerType = 'click' | 'hover';
export type NzSelect2Size = 'small' | 'large' | 'default' ;

// tslint:disable:no-any
export interface Select2Option {
  value?: any;
  label?: string;
  title?: string;
  disabled?: boolean;
  loading?: boolean;
  isLeaf?: boolean;
  parent?: Select2Option;
  children?: Select2Option[];

  [ key: string ]: any;
}
// tslint:enable:no-any

export interface Select2SearchOption extends Select2Option {
  path: Select2Option[];
}

export interface NzShowSearchOptions {
  filter?(inputValue: string, path: Select2Option[]): boolean;
  sorter?(a: Select2Option[], b: Select2Option[], inputValue: string): number;
}
