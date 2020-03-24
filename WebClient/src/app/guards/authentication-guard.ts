import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { AuthenticationService } from '../services/authentication/authentication.service';

@Injectable()
export class AuthenticationGuard implements CanActivate {
  constructor(
    private authService: AuthenticationService,
    private router: Router) { }

  canActivate(): Observable<boolean> {
    return this.authService.validateToken(
      () => {
        return true;
      },
      () => {
        this.router.navigate(['/home']);
        return false;
      });
  }
}
