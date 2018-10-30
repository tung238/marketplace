import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'appc-listing-list',
  templateUrl: './listing-list.component.html',
  styleUrls: ['./listing-list.component.scss']
})
export class ListingListComponent implements OnInit {

  @Input() listings: [];
  
  constructor() { }

  ngOnInit() {
  }

}
