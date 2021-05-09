import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Align } from 'src/app/enums/align.enum';
import { Category } from 'src/app/models/domain/category';
import { OfferList } from 'src/app/models/domain/offer-list';
import { Pagination } from 'src/app/models/helpers/pagination';
import { OffersRequest } from 'src/app/resolvers/requests/offers-request';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { OfferService } from 'src/app/services/offer.service';

@Component({
  selector: 'app-my-offers',
  templateUrl: './my-offers.component.html',
  styleUrls: ['./my-offers.component.scss']
})
export class MyOffersComponent implements OnInit {
  offers: OfferList[];
  categories: Category[];
  pagination: Pagination;

  private offersRequest: OffersRequest;

  private firstLoaded = true;

  align = Align;

  constructor(private offerService: OfferService, private route: ActivatedRoute, private listener: Listener, private notifier: Notifier) { }

  ngOnInit(): void {
    this.subscribeData();

    this.firstLoaded = false;
  }

  public onOfferDeleted(offerId: string) {
    this.offers = this.offers.filter(o => o.id !== offerId);
  }

  public onPageChanged(index: number) {
    this.listener.changeCurrentOffersRequest({ ...this.offersRequest, pageNumber: index });
  }

  private getMyOffers(request: OffersRequest) {
    if (!this.firstLoaded) {
      this.offerService.getMyOffers(request).subscribe(response => {
        this.offers = response.result.offers;
        this.pagination = response.pagination;
      }, error => this.notifier.push(error, 'error'));
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.offers = data.myOffersResponse.result.offers;
      this.categories = data.categoriesResponse.categories;
      this.pagination = data.myOffersResponse.pagination;

      this.listener.changeCurrentCategories(this.categories);
      this.listener.changeCurrentNavbarFormVisible(true);

      this.listener.currentOffersRequest.subscribe(request => {
        this.offersRequest = request;
        this.getMyOffers(request);
      });
    });
  }
}
