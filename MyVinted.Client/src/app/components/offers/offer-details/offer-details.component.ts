import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Offer } from 'src/app/models/domain/offer';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.scss']
})
export class OfferDetailsComponent implements OnInit {
  offer: Offer;

  photos: { id: number, url: string }[];

  private indexCounter: number = 0;

  currentUserId: string;

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.offer = data.offerResponse.offer;
      this.photos = this.mapPhotos();
    });

    this.currentUserId = this.authService.currentUser.id;
    this.listener.changeCurrentNavbarFormVisible(false);
  }

  public onOfferDeleted() {
    this.router.navigate(['myOffers']);
  }

  private mapPhotos = (): { id: number, url: string }[] => {
    const photos: { id: number, url: string }[] = [];
    this.offer.offerPhotos.map(p => photos.push({ id: this.indexCounter++, url: p.url }));

    return photos;
  };
}
