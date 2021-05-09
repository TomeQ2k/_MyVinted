import { FormGroup } from "@angular/forms";
import { ValidationErrorTuple } from "src/app/models/helpers/validation-error-tuple";

export interface Validatable {
  getValidationErrorTuple(control: string, formGroup?: FormGroup): ValidationErrorTuple;
}
