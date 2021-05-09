import { Injectable } from '@angular/core';
import { SocialAuthService, SocialAuthServiceConfig } from "angularx-social-login";
import { GoogleLoginProvider, FacebookLoginProvider } from "angularx-social-login";
import { facebookCredentials, googleCredentials } from 'src/environments/environment';
import { ExternalProvider } from '../enums/external-provider.enum';

@Injectable({
  providedIn: 'root'
})
export class ClientExternalAuthService {

  constructor(private externalAuthService: SocialAuthService) { }

  public signInWithExternalProvider(provider: ExternalProvider) {
    let providerId: string;

    switch (provider) {
      case ExternalProvider.GOOGLE:
        providerId = GoogleLoginProvider.PROVIDER_ID;
        break;
      case ExternalProvider.FACEBOOK:
        providerId = FacebookLoginProvider.PROVIDER_ID;
        break;
    }

    return this.externalAuthService.signIn(providerId);
  }

  public signOutExternal() {
    this.externalAuthService.signOut();
  }
}

export const ExternalAuthServiceProvider = {
  provide: 'SocialAuthServiceConfig',
  useValue: {
    autoLogin: false,
    providers: [
      {
        id: GoogleLoginProvider.PROVIDER_ID,
        provider: new GoogleLoginProvider(
          googleCredentials.clientId
        )
      },
      {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider(
          facebookCredentials.appId
        )
      }
    ],
  } as SocialAuthServiceConfig
};

