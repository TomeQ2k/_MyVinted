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
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, Validatable {
  registerForm: FormGroup;

  constants = constants;

  constructor(private authService: AuthService, private formBuilder: FormBuilder, private formHelper: FormHelper, private notifier: Notifier,
    private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  public register() {
    if (this.registerForm.valid) {
      const request = Object.assign({}, this.registerForm.value);

      this.authService.signUp(request).subscribe(() => {
        this.notifier.push(`Account confirmation token was sent to: ${request.email}`);
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['register']);
      }, () => this.formHelper.resetForm(this.registerForm));
    }
  }

  public getValidationErrorTuple = (control: string) => new ValidationErrorTuple(control, this.registerForm);

  private createRegisterForm() {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      username: ['', [Validators.required, Validators.minLength(this.constants.minUsernameLength), Validators.maxLength(this.constants.maxUsernameLength),
        Validators.pattern(/^\S+$/)]],
      password: ['', [Validators.required, Validators.minLength(this.constants.minPasswordLength), Validators.maxLength(this.constants.maxPasswordLength),
        Validators.pattern(/^\S+$/)]],
      confirmPassword: ['', [Validators.required]]
    }, {
      validators: [
        this.passwordMatchValidator
      ]
    });
  }

  private passwordMatchValidator(formGroup: FormGroup) {
    const password: string = formGroup.get('password').value;
    const confirmPassword: string = formGroup.get('confirmPassword').value;

    if (password !== confirmPassword) {
      formGroup.get('confirmPassword').setErrors({ passwordMismatch: true });
    }
  }
}
