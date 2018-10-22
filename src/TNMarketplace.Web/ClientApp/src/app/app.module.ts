import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { ServiceWorkerModule } from '@angular/service-worker';
import { OAuthModule } from 'angular-oauth2-oidc';
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
import { CategoryModule } from './category/category.module';
import { ProductModule } from './product/product.module';

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
    PrivacyComponent
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
      { path: 'abc', loadChildren: './product/product.module#ProductModule' },

      { path: 'createaccount', loadChildren: './account/+create/create.module#CreateAccountModule' },
      { path: 'thong-tin', loadChildren: './account/+profile/profile.module#ProfileModule' },
      { path: 'signalr', loadChildren: './+signalr/signalr.module#SignalrModule' },
      { path: 'privacy', component: PrivacyComponent },
    ], { initialNavigation: 'enabled' }),
    ServiceWorkerModule.register('/ngsw-worker.js', { enabled: environment.production }),
    FormsModule,
    HttpClientModule,
    NgZorroAntdModule,
    CategoryModule,
    ProductModule,
  ],
  providers: [
    AppService,
    { provide: APP_INITIALIZER, useFactory: appServiceFactory, deps: [AppService], multi: true },
    { provide: NZ_I18N, useValue: en_US }
  ],
  exports: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
