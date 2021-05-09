import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-confirm-account',
  templateUrl: './confirm-account.component.html',
  styleUrls: ['./confirm-account.component.scss']
})
export class ConfirmAccountComponent implements OnInit {
  private email: string;
  private token: string;

  constructor(private authService: AuthService, private route: ActivatedRoute, private router: Router, private notifier: Notifier) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.email = params.email;
      this.token = params.token;
    });

    this.confirmAccount();
  }

  private confirmAccount() {
    if (this.email && this.token) {
      this.authService.confirmAccount(this.email, this.token).subscribe(response => {
        this.notifier.push('Account has been confirmed', 'success');
        this.router.navigate(['login']);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }
}
