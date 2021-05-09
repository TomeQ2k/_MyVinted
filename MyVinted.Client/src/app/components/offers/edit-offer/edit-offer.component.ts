import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { appendPhotos, removePhoto } from 'src/app/helpers/photos-manager';
import { Category } from 'src/app/models/domain/category';
import { OfferUpdate } from 'src/app/models/domain/offer-update';
import { FileModel } from 'src/app/models/helpers/file-model';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { OfferService } from 'src/app/services/offer.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-edit-offer',
  templateUrl: './edit-offer.component.html',
  styleUrls: ['./edit-offer.component.scss']
})
export class EditOfferComponent implements OnInit, Validatable {
  @ViewChild('fileInput') fileInput: ElementRef;

  offerForm: FormGroup;

  constants = constants;

  offerToUpdate: OfferUpdate;
  categories: Category[];
  photoModels: FileModel[] = [];

  constructor(private offerService: OfferService, private notifier: Notifier, private route: ActivatedRoute, private router: Router,
    private formBuilder: FormBuilder, private authService: AuthService, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.offerToUpdate = data.offerUpdateResponse.offerToUpdate;
      this.categories = data.categoriesResponse.categories;
    });

    this.listener.changeCurrentNavbarFormVisible(false);

    if (this.authService.currentUser.id !== this.offerToUpdate.ownerId) {
      this.router.navigate(['']);
      this.notifier.push('You are not allowed to perform this action', 'error');
    }

    this.createOfferForm();
  }

  public updateOffer() {
    if (this.offerForm.valid) {
      const photos: File[] = this.photoModels.map(photo => photo.file);
      const request = { ...Object.assign({}, this.offerForm.value), photos: photos };

      this.offerService.updateOffer(request).subscribe(() => {
        this.notifier.push('Offer has been updated', 'success');
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/editOffer/', this.offerToUpdate.id]);
      }, () => this.router.navigate(['/offers/', this.offerToUpdate.id]));
    }
  }

  public deleteOfferPhoto(photoId: string) {
    if (confirm('Are you sure you want to delete this photo permamently?')) {
      this.offerService.deleteOfferPhoto(photoId, this.offerToUpdate.id).subscribe(() => {
        this.notifier.push('Photo has been deleted');
        this.offerToUpdate.offerPhotos = this.offerToUpdate.offerPhotos.filter(p => p.id !== photoId);
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/editOffer/', this.offerToUpdate.id]);
      });
    }
  }

  public appendPhotos(files: File[]) {
    if (files.length + this.photoModels.length + this.offerToUpdate.offerPhotos.length <= constants.maxFilesCount) {
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
      offerId: [this.offerToUpdate.id],
      title: [this.offerToUpdate.title, [Validators.required, Validators.maxLength(constants.maxTitleLength)]],
      price: [this.offerToUpdate.price.toFixed(2), [Validators.required, Validators.min(1), Validators.max(constants.maxPrice)]],
      description: [this.offerToUpdate.description, [Validators.required, Validators.maxLength(constants.maxDescriptionLength)]],
      allowBidding: [this.offerToUpdate.allowBidding, [Validators.required]],
      categoryId: [this.offerToUpdate.categoryId, [Validators.required]]
    });
  }
}
