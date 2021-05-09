import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { Opinion } from 'src/app/models/domain/opinion';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { Notifier } from 'src/app/services/notifier.service';
import { OpinionService } from 'src/app/services/opinion.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-opinion-form',
  templateUrl: './opinion-form.component.html',
  styleUrls: ['./opinion-form.component.scss']
})
export class OpinionFormComponent implements OnInit, Validatable {
  @Input() userId: string;

  @Output() opinionAdded = new EventEmitter<{ opinion: Opinion, newRating: number }>();

  opinionForm: FormGroup;

  rating: number = 5;
  stars: number[] = [1, 2, 3, 4, 5];

  constants = constants;

  constructor(private opinionService: OpinionService, private notifier: Notifier, private formBuilder: FormBuilder, private formHelper: FormHelper) { }

  ngOnInit(): void {
    this.createOpinionForm();
  }

  public addOffer() {
    if (this.opinionForm.valid) {
      const request = { ...this.opinionForm.value, rating: this.rating, userId: this.userId };

      this.opinionService.addOpinion(request).subscribe(res => {
        const response: any = res.body;

        this.notifier.push('Opinion added');
        this.opinionAdded.emit({ opinion: response.opinion as Opinion, newRating: response.newRating });
      }, error => this.notifier.push(error, 'error'),
        () => this.formHelper.resetForm(this.opinionForm));
    }
  }

  public setRating(rating: number) {
    this.rating = rating;
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.opinionForm);

  private createOpinionForm() {
    this.opinionForm = this.formBuilder.group({
      text: ['', [Validators.required, Validators.maxLength(this.constants.maxDescriptionLength)]]
    });
  }
}
