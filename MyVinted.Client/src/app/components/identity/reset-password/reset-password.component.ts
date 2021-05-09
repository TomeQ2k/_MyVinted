import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit, Validatable {
  resetPasswordForm: FormGroup;

  constants = constants;

  constructor(private authService: AuthService, private notifier: Notifier, private formBuilder: FormBuilder, private router: Router,
    private formHelper: FormHelper) { }

  ngOnInit(): void {
    this.createResetPasswordForm();
  }

  public sendResetPasswordCallback() {
    if (this.resetPasswordForm.valid) {
      const request = Object.assign({}, this.resetPasswordForm.value);

      this.authService.sendResetPasswordCallback(request).subscribe(() => {
        this.notifier.push(`Reset password token was sent to: ${request.email}`);
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['resetPassword']);
      }, () => this.formHelper.resetForm(this.resetPasswordForm));
    }
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.resetPasswordForm);

  private createResetPasswordForm() {
    this.resetPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      newPassword: ['', [Validators.required, Validators.minLength(constants.minPasswordLength), Validators.maxLength(constants.maxPasswordLength),
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
