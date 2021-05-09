import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Offer } from 'src/app/models/domain/offer';
import { OfferList } from 'src/app/models/domain/offer-list';
import { AuthService } from 'src/app/services/auth.service';
import { FavoritesService } from 'src/app/services/favorites.service';
import { Notifier } from 'src/app/services/notifier.service';
import { OfferService } from 'src/app/services/offer.service';

@Component({
  selector: 'app-offer-card',
  templateUrl: './offer-card.component.html',
  styleUrls: ['./offer-card.component.scss']
})
export class OfferCardComponent implements OnInit {
  @Input() offer: Offer | OfferList;
  @Input() isEditable: boolean;
  @Input() isDetails: boolean;

  @Output() offerDeleted = new EventEmitter<string>();
  @Output() offerUnliked = new EventEmitter<string>();

  currentUserId: string;

  constructor(private offerService: OfferService, private favoritesService: FavoritesService, private notifier: Notifier, private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.currentUser.id;
  }

  public likeOffer() {
    this.favoritesService.likeOffer(this.offer.id).subscribe(res => {
      const response: any = res.body;

      if (response.isLiked) {
        this.offer.offerLikes.push(response.like);
        this.offer.likesCount++;
      } else {
        this.offer.offerLikes = this.offer.offerLikes.filter(l => l.userId !== this.currentUserId);
        this.offer.likesCount--;

        this.offerUnliked.emit(this.offer.id);
      }

      this.notifier.push(response.isLiked ? 'Offer added to favorites' : 'Offer removed from favorites');
    }, error => this.notifier.push(error, 'error'));
  }

  public deleteOffer() {
    this.offerService.deleteOffer(this.offer.id).subscribe(() => {
      this.notifier.push('Offer has been deleted');
    }, error => this.notifier.push(error, 'error'),
      () => this.offerDeleted.emit(this.offer.id));
  }

  public isFavorite = () => this.offer.offerLikes.some(l => l.userId === this.currentUserId);
}
