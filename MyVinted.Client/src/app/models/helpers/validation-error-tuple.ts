import { FormGroup } from "@angular/forms";

export class ValidationErrorTuple {
  control: string;
  formGroup: FormGroup;

  constructor(control: string, formGroup: FormGroup) {
    this.control = control;
    this.formGroup = formGroup;
  }
}
