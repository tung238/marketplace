import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'appc-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss']
})
export class BreadcrumbComponent implements OnInit {
  @Input()
  breadcrumb: any[];

  constructor() { }

  ngOnInit() {
  }

}
