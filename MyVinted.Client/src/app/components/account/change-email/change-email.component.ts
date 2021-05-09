import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { UserProfile } from 'src/app/models/domain/user-profile';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { AccountService } from 'src/app/services/account.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-change-email',
  templateUrl: './change-email.component.html',
  styleUrls: ['./change-email.component.scss']
})
export class ChangeEmailComponent implements OnInit, Validatable {
  @Input() currentUser: UserProfile;

  changeEmailForm: FormGroup;

  constructor(private accountService: AccountService, private notifier: Notifier, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.createChangeEmailForm();
  }

  public sendChangeEmailCallback() {
    if (this.changeEmailForm.valid) {
      const request = Object.assign({}, this.changeEmailForm.value);

      this.accountService.sendChangeEmailCallback(request).subscribe(() => {
        this.notifier.push(`Change email token was sent to: ${request.newEmail}`);
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['account']);
      }, () => this.createChangeEmailForm());
    }
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.changeEmailForm);

  private createChangeEmailForm() {
    this.changeEmailForm = this.formBuilder.group({
      newEmail: ['', [Validators.required, Validators.email]]
    });
  }
}
