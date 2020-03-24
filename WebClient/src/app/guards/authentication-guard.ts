import { CanActivate, UrlTree, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { AuthenticationService } from '../services/authentication/authentication.service';

@Injectable()
export class AuthenticationGuard implements CanActivate {
  constructor(
    private authService: AuthenticationService,
    private router: Router) { }

  canActivate(): Observable<boolean | UrlTree> {
    if (this.authService.IsAuthenticated) {
      return of(true);
    }

    this.router.navigate(['/home']);
    return of(false);
  }
}
