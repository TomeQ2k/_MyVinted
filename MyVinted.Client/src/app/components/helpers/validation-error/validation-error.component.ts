import { Component, Input, OnInit } from '@angular/core';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';

@Component({
  selector: 'app-validation-error',
  templateUrl: './validation-error.component.html',
  styleUrls: ['./validation-error.component.scss']
})
export class ValidationErrorComponent implements OnInit {
  @Input() errorTuple: ValidationErrorTuple;

  constructor(public formHelper: FormHelper) { }

  ngOnInit(): void {
  }
}
