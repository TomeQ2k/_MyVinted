import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UserAuth } from '../models/domain/user-auth';
import { Listener } from './listener.service';
import { ExternalAuth } from '../models/helpers/external-auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly authApiUrl = environment.apiUrl + 'auth/';

  jwtHelper = new JwtHelperService();

  currentUser: UserAuth;
  decodedToken: any;

  private signedIn = new BehaviorSubject(this.isSignedIn())
  currentSignedIn = this.signedIn.asObservable();

  constructor(private httpClient: HttpClient, private listener: Listener) { }

  public signIn(request: any) {
    return this.httpClient.post(this.authApiUrl + 'signIn', request, { observe: 'response' })
      .pipe(
        map((res: any) => {
          const response = res.body;

          if (response.isSucceeded) {
            this.initUserData(response);
          }
        })
      );
  }

  public signInWithExternalProvider(externalAuth: ExternalAuth) {
    return this.httpClient.post(this.authApiUrl + 'signIn/external', externalAuth, { observe: 'response' })
      .pipe(
        map((res: any) => {
          const response = res.body;

          if (response.isSucceeded) {
            this.initUserData(response);
          }
        })
      );
  }

  public signUp(request: any) {
    return this.httpClient.post(this.authApiUrl + 'signUp', request);
  }

  public signOut() {
    localStorage.clear();

    this.decodedToken = null;
    this.currentUser = null;

    this.listener.changeCurrentUser(this.currentUser);

    this.isSignedInEmit();
  }

  public confirmAccount(email: string, token: string) {
    return this.httpClient.get<any>(this.authApiUrl + 'signUp/confirm', { params: { email, token } });
  }

  public resetPassword(email: string, newPassword: string, token: string) {
    return this.httpClient.get<any>(this.authApiUrl + 'resetPassword', { params: { email, newPassword, token } });
  }

  public sendResetPasswordCallback(request: any) {
    return this.httpClient.post(this.authApiUrl + 'resetPassword/send', request);
  }

  public checkPermissions(roles: string[]) {
    let isPermitted = false;

    const userRoles = this.decodedToken?.role as string[];

    if (!userRoles) {
      return false;
    }

    for (const role of roles) {
      if (userRoles.includes(role)) {
        isPermitted = true;
        break;
      }
    }

    return isPermitted;
  }

  public isSignedIn() {
    return !!this.decodedToken;
  }

  public isSignedInEmit() {
    this.signedIn.next(this.isSignedIn());
  }

  private initUserData(response: any) {
    localStorage.setItem('token', response.token);
    this.decodedToken = this.jwtHelper.decodeToken(response.token);

    this.currentUser = response.user;
    this.listener.changeCurrentUser(this.currentUser);

    this.isSignedInEmit();
  }
}
