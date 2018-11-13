import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { AccountService } from './core';

/**
 * Decides if a route can be activated.
 */
@Injectable() export class AuthGuard implements CanActivate  {

    constructor(private accountService: AccountService,
        private router: Router) { }

    canActivate(): boolean {
        if (!this.accountService.isLoggedIn) {
          this.router.navigate(['/dang-nhap']);
          return false;
        }
        return true;
      }

}
