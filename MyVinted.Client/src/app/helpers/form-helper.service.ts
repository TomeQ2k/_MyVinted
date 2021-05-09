import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormHelper {

  public resetForm(formGroup: FormGroup) {
    const controls = formGroup.controls;

    formGroup.reset();

    for (const key in controls) {
      controls[key].setErrors(null);
    }
  }

  public getErrorMessage(formGroup: FormGroup, field: string) {
    if (formGroup.get(field).hasError('required')) {
      return 'Field is required';
    }
    if (formGroup.get(field).hasError('minlength')) {
      return `Field must exceed ${formGroup.get(field).errors.minlength.requiredLength} characters`;
    }
    if (formGroup.get(field).hasError('maxlength')) {
      return `Field cannot exceed ${formGroup.get(field).errors.maxLength.requiredLength} characters`;
    }
    if (formGroup.get(field).hasError('min')) {
      return `Minimum allowed value is: ${formGroup.get(field).errors.min.min}`;
    }
    if (formGroup.get(field).hasError('max')) {
      return `Maximum allowed value is: ${formGroup.get(field).errors.max.max}`;
    }
    if (formGroup.get(field).hasError('email')) {
      return 'Invalid email address format';
    }
    if (formGroup.get(field).hasError('pattern')) {
      return 'Invalid format';
    }
    if (formGroup.get(field).hasError('whitespaces')) {
      return 'Whitespaces are not allowed';
    }
    if (formGroup.get(field).hasError('passwordMismatch')) {
      return 'Passwords don\'t match';
    }
  }
}
