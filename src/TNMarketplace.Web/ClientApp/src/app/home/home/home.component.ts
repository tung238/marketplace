import { Component, OnInit } from '@angular/core';
import { AppService } from '@app/app.service';

@Component({
  selector: 'appc-home-component',
  templateUrl: './home.component.html',
  styleUrls: ["home.component.scss"]
})

export class HomeComponent implements OnInit {
  rootCategories: any[]
  ngOnInit(): void {
    this.appService.getAppData().then(data=>{
      this.rootCategories = data.categoriesTree.map(c=>{
        if (c.iconClass == null){
          c.iconClass = 'fa fa-at';
        }
        return c;
      });
    })
  }
  constructor(
    private appService: AppService
  ){

  }
  array = [1, 2, 3];
  public imageSources: string[] = [
    '/assets/images/banner.jpg',
    '/assets/images/banner.jpg',
    '/assets/images/banner.jpg'
  ];


  // public config: ICarouselConfig = {
  //   verifyBeforeLoad: true,
  //   log: false,
  //   animation: true,
  //   animationType: AnimationConfig.SLIDE,
  //   autoplay: true,
  //   autoplayDelay: 2000,
  //   stopAutoplayMinWidth: 768
  // };
}
