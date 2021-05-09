import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { UserProfile } from 'src/app/models/domain/user-profile';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { AccountService } from 'src/app/services/account.service';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-change-phone',
  templateUrl: './change-phone.component.html',
  styleUrls: ['./change-phone.component.scss']
})
export class ChangePhoneComponent implements OnInit, Validatable {
  @Input() currentUser: UserProfile;

  changePhoneForm: FormGroup;

  constants = constants;

  constructor(private accountService: AccountService, private notifier: Notifier, private formBuilder: FormBuilder, private router: Router, private authService: AuthService, private listener: Listener) { }

  ngOnInit(): void {
    this.createChangePhoneForm();
  }

  public changePhoneNumber() {
    if (this.changePhoneForm.valid) {
      const request = Object.assign({}, this.changePhoneForm.value);

      this.accountService.changePhoneNumber(request).subscribe(() => {
        const phoneNumber = request.newPhoneNumber;
        this.notifier.push('Phone number has been changed');

        this.currentUser.phoneNumber = phoneNumber;
        this.authService.currentUser.phoneNumber = phoneNumber;
        this.listener.changeCurrentUser(this.authService.currentUser);
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['account']);
      }, () => this.createChangePhoneForm());
    }
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.changePhoneForm);

  private createChangePhoneForm() {
    this.changePhoneForm = this.formBuilder.group({
      newPhoneNumber: ['', [Validators.required, Validators.maxLength(this.constants.maxPhoneNumberLength),
      Validators.pattern(/^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$/)]]
    });
  }
}
