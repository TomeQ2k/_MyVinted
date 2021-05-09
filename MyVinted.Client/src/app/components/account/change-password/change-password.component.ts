import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { AccountService } from 'src/app/services/account.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit, Validatable {
  changePasswordForm: FormGroup;

  constants = constants;

  constructor(private accountService: AccountService, private notifier: Notifier, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.createChangePasswordForm();
  }

  public changePassword() {
    if (this.changePasswordForm.valid) {
      const request = Object.assign({}, this.changePasswordForm.value);

      this.accountService.changePassword(request).subscribe(() => {
        this.notifier.push('Password has been changed');
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['account']);
      }, () => this.createChangePasswordForm());
    }
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.changePasswordForm);

  private createChangePasswordForm() {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: ['', [Validators.required]],
      newPassword: ['', [Validators.required, Validators.minLength(this.constants.minPasswordLength), Validators.maxLength(this.constants.maxPasswordLength),
      Validators.pattern(/^\S+$/)]],
      confirmPassword: ['', [Validators.required]]
    }, {
      validators: [
        this.passwordMatchValidator
      ]
    });
  }

  private passwordMatchValidator(formGroup: FormGroup) {
    const password: string = formGroup.get('newPassword').value;
    const confirmPassword: string = formGroup.get('confirmPassword').value;

    if (password !== confirmPassword) {
      formGroup.get('confirmPassword').setErrors({ passwordMismatch: true });
    }
  }
}
