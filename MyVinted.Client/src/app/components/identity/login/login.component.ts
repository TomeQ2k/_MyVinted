import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SocialUser } from 'angularx-social-login';
import { ExternalProvider } from 'src/app/enums/external-provider.enum';
import { ExternalAuth } from 'src/app/models/helpers/external-auth';
import { AuthService } from 'src/app/services/auth.service';
import { ClientExternalAuthService } from 'src/app/services/client-external-auth.service';
import { Messenger } from 'src/app/services/messenger.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  externalProvider = ExternalProvider;

  constructor(private authService: AuthService, private router: Router, private formBuilder: FormBuilder, private notifier: Notifier,
    private clientExternalAuthService: ClientExternalAuthService, private notificationService: NotificationService, private messenger: Messenger) { }

  ngOnInit(): void {
    this.createLoginForm();
  }

  public login() {
    const request = Object.assign({}, this.loginForm.value);

    this.authService.signIn(request).subscribe(() => {
      this.router.navigate(['']);
      this.notifier.push('Signed in');
    }, error => {
      this.router.navigate(['login']);
      this.notifier.push(error, 'error');
    }, () => this.countUnread());
  }

  public loginWithExternalProvider(provider: ExternalProvider) {
    this.clientExternalAuthService.signInWithExternalProvider(provider)
      .then(res => {
        const user: SocialUser = { ...res };
        const externalAuth: ExternalAuth = {
          provider: user.provider,
          idToken: user.idToken ? user.idToken : user.authToken
        };
        this.authService.signInWithExternalProvider(externalAuth).subscribe(() => {
          this.notifier.push('Signed in');
          this.router.navigate(['']);
        }, error => {
          this.notifier.push(error, 'error');
          this.clientExternalAuthService.signOutExternal();
        }, () => this.countUnread());
      }, error => this.notifier.push(error, 'error'));
  }

  private createLoginForm() {
    this.loginForm = this.formBuilder.group({
      email: [''],
      password: ['']
    });
  }

  private countUnread() {
    this.notificationService.countUnreadNotifications();
    this.messenger.countUnreadMessages();
  }
}
