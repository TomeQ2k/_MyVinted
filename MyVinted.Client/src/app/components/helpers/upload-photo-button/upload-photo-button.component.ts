import { Component, EventEmitter, Input, Output } from '@angular/core';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-upload-photo-button',
  templateUrl: './upload-photo-button.component.html',
  styleUrls: ['./upload-photo-button.component.scss']
})
export class UploadPhotoButtonComponent {
  @Input() currentCount: number;
  @Input() multiple: boolean = false;

  @Output() photosUploaded = new EventEmitter<File[]>();

  constants = constants;

  constructor() { }
}
