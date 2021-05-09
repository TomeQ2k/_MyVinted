import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Filterable } from 'src/app/helpers/interfaces/filterable';
import { OffersRequest } from 'src/app/resolvers/requests/offers-request';
import { Listener } from 'src/app/services/listener.service';

@Component({
  selector: 'app-offers-filter-form',
  templateUrl: './offers-filter-form.component.html',
  styleUrls: ['./offers-filter-form.component.scss']
})
export class OffersFilterFormComponent implements OnInit, Filterable {
  filterForm: FormGroup;

  private offersRequest: OffersRequest;

  constructor(private formBuilder: FormBuilder, private listener: Listener) { }

  ngOnInit(): void {
    this.createFilterForm();
    this.listener.currentOffersRequest.subscribe(request => this.offersRequest = request);
  }

  public emitFilters() {
    this.offersRequest = Object.assign(this.offersRequest, this.filterForm.value);
    this.listener.changeCurrentOffersRequest(this.offersRequest);
  }

  public clearFilters() {
    this.offersRequest = new OffersRequest();
    this.createFilterForm();

    this.listener.changeCurrentOffersRequest(this.offersRequest);
    this.listener.clearCurrentNavbarForm();
  }

  private createFilterForm() {
    this.filterForm = this.formBuilder.group({
      sortType: [0],
      onlyVerified: [false],
      boughtOfferStatus: [1]
    });
  }
}
