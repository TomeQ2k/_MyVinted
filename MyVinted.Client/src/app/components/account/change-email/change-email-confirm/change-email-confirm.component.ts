import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-change-email-confirm',
  templateUrl: './change-email-confirm.component.html',
  styleUrls: ['./change-email-confirm.component.scss']
})
export class ChangeEmailConfirmComponent implements OnInit {
  private newEmail: string;
  private token: string;

  constructor(private accountService: AccountService, private route: ActivatedRoute, private router: Router, private notifier: Notifier) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.newEmail = params.newEmail;
      this.token = params.token;
    });

    this.changeEmail();
  }

  private changeEmail() {
    if (this.newEmail && this.token) {
      this.accountService.changeEmail(this.newEmail, this.token).subscribe(() => {
        this.notifier.push('Email address has been changed');
        this.router.navigate(['account']);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }
}
