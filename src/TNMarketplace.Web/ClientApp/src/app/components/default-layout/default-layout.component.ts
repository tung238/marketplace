import { Component, OnInit } from '@angular/core';
import { NavigationStart, ActivatedRoute, Router } from '@angular/router';
import { AppService } from '@app/app.service';

@Component({
  selector: 'appc-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss']
})
export class DefaultLayoutComponent implements OnInit {
  public notificationOptions = {
    position: ['top', 'right'],
    timeOut: 5000,
    lastOnBottom: true
  };
  isCollapsed = true;
  rootCategories: any[]
  constructor(    private router: Router,
    private appService: AppService
  ) { }

  ngOnInit() {
    this.router.events.subscribe((event: NavigationStart) => {
      this.isCollapsed = true;
    });
    this.appService.getAppData().then(data=>{
      this.rootCategories = data.categoriesTree.map(c=>{
        if (c.iconClass == null){
          c.iconClass = 'fa fa-at';
        }
        return c;
      });
    })
  }
  public toggleNav() {
    this.isCollapsed = !this.isCollapsed;
  }

}
