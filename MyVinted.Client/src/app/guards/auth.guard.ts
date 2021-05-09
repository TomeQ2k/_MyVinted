import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const isSignedIn = this.authService.isSignedIn();
    const routeRoles = next.firstChild?.data?.roles as string[];

    if (routeRoles && isSignedIn) {
      const isPermitted = this.authService.checkPermissions(routeRoles);

      if (isPermitted) {
        return true;
      }

      this.router.navigate(['']);
    }

    if (isSignedIn) {
      return true;
    }

    this.router.navigate(['login'], { queryParams: { returnTo: state.url } });

    return false;
  }

}
