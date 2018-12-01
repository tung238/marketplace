import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { Title, Meta } from '@angular/platform-browser';

import { Params, ActivatedRoute, Router, NavigationStart } from '@angular/router';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';

// import { routerTransition } from './router.animations';
import { ExternalLoginStatus } from './app.models';
import { AppService } from './app.service';

@Component({
  selector: 'appc-root',
  // animations: [routerTransition],
  styleUrls: ['./app.component.scss'],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  
  constructor(
    private router: Router,
    private title: Title,
    private meta: Meta,
    private appService: AppService,
    @Inject('BASE_URL') private baseUrl: string,
    @Inject(PLATFORM_ID) private platformId: string,
    private activatedRoute: ActivatedRoute,
    private oauthService: OAuthService,
  ) {

    if (isPlatformBrowser(this.platformId)) {
      this.configureOidc();
    }
  }

  public ngOnInit() {
    this.updateTitleAndMeta();
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      const param = params['externalLoginStatus'];
      if (param) {
        const status = <ExternalLoginStatus>+param;
        switch (status) {
          case ExternalLoginStatus.CreateAccount:
            this.router.navigate(['createaccount']);
            break;
          default:
            break;
        }
      }
    });
  }

  public getState(outlet: any) {
    return outlet.activatedRouteData.state;
  }

  private configureOidc() {
    this.oauthService.configure(authConfig(this.baseUrl));
    this.oauthService.setStorage(localStorage);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }

  private updateTitleAndMeta() {
    this.title.setTitle("Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com");
    this.meta.addTags([
      { name: 'description', content: "Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com" },
      { property: 'keywords', content: "Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com" },
      { property: 'og:description', content: "Mua bán, rao vặt, mua bán nhà đất, bán xe hơi : moichao.com" }
    ]);
  }
}
