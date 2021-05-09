import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-reset-password-confirm',
  templateUrl: './reset-password-confirm.component.html',
  styleUrls: ['./reset-password-confirm.component.scss']
})
export class ResetPasswordConfirmComponent implements OnInit {
  private email: string;
  private newPassword: string;
  private token: string;

  constructor(private authService: AuthService, private route: ActivatedRoute, private router: Router, private notifier: Notifier) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.email = params.email;
      this.newPassword = params.newPassword;
      this.token = params.token;
    });

    this.resetPassword();
  }

  private resetPassword() {
    if (this.email && this.newPassword && this.token) {
      this.authService.resetPassword(this.email, this.newPassword, this.token).subscribe(response => {
        this.notifier.push('Password has been changed', 'success');
        this.router.navigate(['login']);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }
}
