import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'appc-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {
  isCollapsed = false;
  constructor(    private router: Router,
  ) { }

  ngOnInit() {
    this.router.events.subscribe((event: NavigationStart) => {
      this.isCollapsed = true;
    });
  }
  public toggleNav() {
    this.isCollapsed = !this.isCollapsed;
  }

}
