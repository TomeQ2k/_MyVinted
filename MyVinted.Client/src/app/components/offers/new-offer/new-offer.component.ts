import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { appendPhotos, removePhoto } from 'src/app/helpers/photos-manager';
import { Category } from 'src/app/models/domain/category';
import { FileModel } from 'src/app/models/helpers/file-model';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { OfferService } from 'src/app/services/offer.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-new-offer',
  templateUrl: './new-offer.component.html',
  styleUrls: ['./new-offer.component.scss']
})
export class NewOfferComponent implements OnInit, Validatable {
  @ViewChild('fileInput') fileInput: ElementRef;

  offerForm: FormGroup;

  constants = constants;

  categories: Category[];
  photoModels: FileModel[] = [];

  constructor(private offerService: OfferService, private notifier: Notifier, private router: Router, private formBuilder: FormBuilder,
    private formHelper: FormHelper, private route: ActivatedRoute, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => this.categories = data.categoriesResponse.categories);
    this.listener.changeCurrentNavbarFormVisible(false);
    this.createOfferForm();
  }

  public addOffer() {
    if (this.offerForm.valid) {
      const photos: File[] = this.photoModels.map(photo => photo.file);
      const request = { ...Object.assign({}, this.offerForm.value), photos: photos };

      this.offerService.addOffer(request).subscribe(() => {
        this.notifier.push('Offer has been added', 'success');
        this.router.navigate(['myOffers']);
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['newOffer']);
      }, () => this.formHelper.resetForm(this.offerForm));
    }
  }

  public appendPhotos(files: File[]) {
    if (files.length + this.photoModels.length <= constants.maxFilesCount) {
      this.photoModels = appendPhotos(this.photoModels, files);
    } else {
      this.notifier.push(`Maximum files count is: ${constants.maxFilesCount}`, 'warning');
    }
  }

  public removePhoto(id: Guid) {
    this.photoModels = removePhoto(this.photoModels, id);
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.offerForm);

  private createOfferForm() {
    this.offerForm = this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(constants.maxTitleLength)]],
      price: [1, [Validators.required, Validators.min(1), Validators.max(constants.maxPrice)]],
      description: ['', [Validators.required, Validators.maxLength(constants.maxDescriptionLength)]],
      allowBidding: [false, [Validators.required]],
      categoryId: [null, [Validators.required]]
    });
  }
}
