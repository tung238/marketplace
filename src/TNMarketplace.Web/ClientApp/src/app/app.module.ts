import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER, LOCALE_ID } from '@angular/core';
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
import { AppService } from './app.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgZorroAntdModule, NZ_I18N, en_US } from 'ng-zorro-antd';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { NotFoundComponent } from './not-found/not-found.component';
import { Configuration, ApiModule } from './api';
import localeVi from '@angular/common/locales/vi';
import { AuthGuard } from './auth.guard';
import { DefaultLayoutComponent } from './components/default-layout/default-layout.component';
import { AdminLayoutComponent } from './components/admin-layout/admin-layout.component';
import { ProfileService } from './account/+profile/profile.service';

registerLocaleData(localeVi);
registerLocaleData(en);

export function appServiceFactory(appService: AppService): Function {
  return () => appService.getAppData();
}
@NgModule({
  declarations: [
    // Components
    AppComponent,
    CookieConsentComponent,
    FooterComponent,
    HeaderComponent,
    ModalComponent,
    PrivacyComponent,
    NotFoundComponent,
    DefaultLayoutComponent,
    AdminLayoutComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    PrebootModule.withConfig({ appRoot: 'appc-root' }),
    BrowserAnimationsModule,
    BrowserTransferStateModule,
    FormsModule,
    ApiModule,
    HttpClientModule,
    NgZorroAntdModule,
    CoreModule.forRoot(),
    AppSharedModule,
    SimpleNotificationsModule.forRoot(),
    OAuthModule.forRoot(),
    RouterModule.forRoot([
      { path: 'admin', component: AdminLayoutComponent, loadChildren: './admin/admin.module#AdminModule' },
      {
        path: '',
        component: DefaultLayoutComponent,
        children: [
          {
            path: '',
            loadChildren: './home/home.module#HomeModule'
          },
          { path: 'dang-nhap', loadChildren: './account/+login/login.module#LoginModule' },
          { path: 'dang-ky', loadChildren: './account/+register/register.module#RegisterModule' },
          { path: 'dang-tin', loadChildren: './listing-add/listing-add.module#ListingAddModule', canActivate: [AuthGuard] },
          { path: 'dang-tin/:id', loadChildren: './listing-add/listing-add.module#ListingAddModule', canActivate: [AuthGuard] },
          { path: 'createaccount', loadChildren: './account/+create/create.module#CreateAccountModule' },
          { path: 'tai-khoan', loadChildren: './account/+profile/profile.module#ProfileModule' },
          { path: 'signalr', loadChildren: './+signalr/signalr.module#SignalrModule' },
          { path: 'privacy', component: PrivacyComponent },
          { path: 'khong-tim-thay', component: NotFoundComponent },
          { matcher: listings, loadChildren: './listings/listings.module#ListingsModule', runGuardsAndResolvers: 'always' },
          { matcher: listingItem, loadChildren: './listing-item/listing-item.module#ListingItemModule', runGuardsAndResolvers: 'always' }
        ]
      },
    ], { initialNavigation: 'enabled', onSameUrlNavigation: 'reload' }),
    ServiceWorkerModule.register('/ngsw-worker.js', { enabled: environment.production }),
    

  ],
  providers: [
    AppService, 
    ProfileService,
    AuthGuard,
    { provide: LOCALE_ID, useValue: "vi-VN" },
    { provide: APP_INITIALIZER, useFactory: appServiceFactory, deps: [AppService], multi: true },
    { provide: NZ_I18N, useValue: en_US },
    { provide: Configuration, useFactory: (authService: OAuthService) => new Configuration({ basePath: environment.apiLocation, accessToken: authService.getAccessToken(), apiKeys: { "Authorization": "Bearer" } }), deps: [OAuthService], multi: false }
  ],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule {

}

export function listings(url: UrlSegment[]) {
  if (url.length == 0) {
    return null
  }
  return !url[url.length - 1].path.endsWith('.html') ? ({ consumed: url }) : null;
}

export function listingItem(url: UrlSegment[]) {
  if (url.length == 0) {
    return null
  }
  return url[url.length - 1].path.endsWith('.html') ? { consumed: url } : null;
  // return url.length === 1 && url[0].path.endsWith('.html') ? ({consumed: url}) : null;
}
