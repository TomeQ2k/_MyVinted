import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserProfile } from 'src/app/models/domain/user-profile';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { AccountService } from 'src/app/services/account.service';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-change-username',
  templateUrl: './change-username.component.html',
  styleUrls: ['./change-username.component.scss']
})
export class ChangeUsernameComponent implements OnInit {
  @Input() currentUser: UserProfile;

  changeUsernameForm: FormGroup;

  constants = constants;

  constructor(private accountService: AccountService, private notifier: Notifier, private formBuilder: FormBuilder, private router: Router, private authService: AuthService, private listener: Listener) { }

  ngOnInit(): void {
    this.createChangeUsernameForm();
  }

  public changeUsername() {
    if (this.changeUsernameForm.valid) {
      const request = Object.assign({}, this.changeUsernameForm.value);

      this.accountService.changeUsername(request).subscribe(() => {
        const username = request.newUsername;
        this.notifier.push('Username has been changed');

        this.currentUser.userName = username;
        this.authService.currentUser.userName = username;
        this.listener.changeCurrentUser(this.authService.currentUser);
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['account']);
      }, () => this.createChangeUsernameForm());
    }
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.changeUsernameForm);

  private createChangeUsernameForm() {
    this.changeUsernameForm = this.formBuilder.group({
      newUsername: ['', [Validators.required, Validators.minLength(this.constants.minUsernameLength), Validators.maxLength(this.constants.maxUsernameLength),
      Validators.pattern(/^\S+$/)]]
    });
  }
}
