import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-photos-gallery',
  templateUrl: './photos-gallery.component.html',
  styleUrls: ['./photos-gallery.component.scss']
})
export class PhotosGalleryComponent {
  @Input() photos: { id: number, url: string }[];
}
