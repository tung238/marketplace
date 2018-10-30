import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, UrlSegment } from '@angular/router';
import { ServiceWorkerModule } from '@angular/service-worker';
import { OAuthModule, OAuthService } from 'angular-oauth2-oidc';
import { PrebootModule } from 'preboot';

import { environment } from '../environments/environment';

import { CoreModule } from './core';
import { AppSharedModule } from './appshared';
import { SimpleNotificationsModule } from './simple-notifications';

// Components
import { AppComponent } from './app.component';
import { CookieConsentComponent, FooterComponent, HeaderComponent, ModalComponent, PrivacyComponent } from './components';
import { HomeComponent } from './home/home.component';
import { AppService } from './app.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgZorroAntdModule, NZ_I18N, en_US } from 'ng-zorro-antd';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { NotFoundComponent } from './not-found/not-found.component';
import { Configuration, ApiModule } from './api';

registerLocaleData(en);
export function appServiceFactory(appService: AppService): Function {
  return () => appService.getAppData();
}
@NgModule({
  declarations: [
    // Components
    AppComponent,
    HomeComponent,
    CookieConsentComponent,
    FooterComponent,
    HeaderComponent,
    ModalComponent,
    PrivacyComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    PrebootModule.withConfig({ appRoot: 'appc-root' }),
    BrowserAnimationsModule,
    BrowserTransferStateModule,
    CoreModule.forRoot(),
    AppSharedModule,
    SimpleNotificationsModule.forRoot(),
    OAuthModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', data: { state: 'home' } },
      { path: 'dang-nhap', loadChildren: './account/+login/login.module#LoginModule' },
      { path: 'dang-ky', loadChildren: './account/+register/register.module#RegisterModule' },
      { path: 'createaccount', loadChildren: './account/+create/create.module#CreateAccountModule' },
      { path: 'thong-tin', loadChildren: './account/+profile/profile.module#ProfileModule' },
      { path: 'signalr', loadChildren: './+signalr/signalr.module#SignalrModule' },
      { path: 'privacy', component: PrivacyComponent },
      { path: 'not-found', component: NotFoundComponent},
      { matcher: listings, loadChildren: './listings/listings.module#ListingsModule' },
      { matcher: listingItem, loadChildren: './listing-item/listing-item.module#ListingItemModule'}
    ], { initialNavigation: 'enabled' }),
    ServiceWorkerModule.register('/ngsw-worker.js', { enabled: environment.production }),
    FormsModule,
    ApiModule,
    HttpClientModule,
    NgZorroAntdModule
  ],
  providers: [
    AppService,
    { provide: APP_INITIALIZER, useFactory: appServiceFactory, deps: [AppService], multi: true },
    { provide: NZ_I18N, useValue: en_US },
    { provide: Configuration, useFactory: (authService: OAuthService) => new Configuration({ basePath: environment.apiLocation, accessToken: authService.getAccessToken(), apiKeys: {"Authorization": "Bearer"} }), deps: [OAuthService], multi: false }
  ],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule { 
  
}

export function listings(url: UrlSegment[]) {
  return !url[url.length - 1].path.endsWith('.html') ? ({consumed: url}) : null;
}

export function listingItem(url: UrlSegment[]) {
  return url[url.length - 1].path.endsWith('.html') ? {consumed: url} : null;
  // return url.length === 1 && url[0].path.endsWith('.html') ? ({consumed: url}) : null;
}
