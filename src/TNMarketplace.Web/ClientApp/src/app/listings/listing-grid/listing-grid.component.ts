import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'appc-listing-grid',
  templateUrl: './listing-grid.component.html',
  styleUrls: ['./listing-grid.component.scss']
})
export class ListingGridComponent implements OnInit {

  @Input() listings: [];
  constructor() { }

  ngOnInit() {
  }

}
