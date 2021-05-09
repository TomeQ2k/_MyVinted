import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Align } from 'src/app/enums/align.enum';
import { Category } from 'src/app/models/domain/category';
import { OfferList } from 'src/app/models/domain/offer-list';
import { Pagination } from 'src/app/models/helpers/pagination';
import { OffersRequest } from 'src/app/resolvers/requests/offers-request';
import { AuthService } from 'src/app/services/auth.service';
import { FavoritesService } from 'src/app/services/favorites.service';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements OnInit {
  favoritesOffers: OfferList[];
  categories: Category[];
  pagination: Pagination;

  private offersRequest: OffersRequest;

  private firstLoaded = true;

  currentUserId: string;

  align = Align;

  constructor(private favoritesService: FavoritesService, private route: ActivatedRoute, private listener: Listener, private notifier: Notifier,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.subscribeData();

    this.firstLoaded = false;
  }

  public onOfferDeleted(offerId: string) {
    this.favoritesOffers = this.favoritesOffers.filter(o => o.id !== offerId);
  }

  public onOfferUnliked(offerId: string) {
    this.favoritesOffers = this.favoritesOffers.filter(o => o.id !== offerId);
  }

  public onPageChanged(index: number) {
    this.listener.changeCurrentOffersRequest({ ...this.offersRequest, pageNumber: index });
  }

  private getFavoritesOffers(request: OffersRequest) {
    if (!this.firstLoaded) {
      this.favoritesService.getFavoritesOffers(request).subscribe(response => {
        this.favoritesOffers = response.result.favoritesOffers;
        this.pagination = response.pagination;
      }, error => this.notifier.push(error, 'error'));
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.favoritesOffers = data.favoritesResponse.result.favoritesOffers;
      this.categories = data.categoriesResponse.categories;
      this.pagination = data.favoritesResponse.pagination;

      this.listener.changeCurrentCategories(this.categories);
      this.listener.changeCurrentNavbarFormVisible(true);

      this.listener.currentOffersRequest.subscribe(request => {
        this.offersRequest = request;
        this.getFavoritesOffers(request);
      });
    });

    this.currentUserId = this.authService.currentUser.id;
  }
}
