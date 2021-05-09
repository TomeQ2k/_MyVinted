import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Align } from 'src/app/enums/align.enum';
import { Order } from 'src/app/models/domain/order';
import { Pagination } from 'src/app/models/helpers/pagination';
import { OrdersRequest } from 'src/app/resolvers/requests/orders-request';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-admin-orders',
  templateUrl: './admin-orders.component.html',
  styleUrls: ['./admin-orders.component.scss']
})
export class AdminOrdersComponent implements OnInit {
  orders: Order[];
  pagination: Pagination;

  filterForm: FormGroup;

  private ordersRequest: OrdersRequest = new OrdersRequest();

  align = Align;

  constructor(private orderService: OrderService, private route: ActivatedRoute, private formBuilder: FormBuilder,
    private notifier: Notifier, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.orders = data.ordersResponse.result.orders;
      this.pagination = data.ordersResponse.pagination;
      this.listener.changeCurrentFilteredOrders(this.orders);
    });

    this.createFilterForm();
    this.listener.changeCurrentNavbarFormVisible(false);
  }

  public onFilterChanged() {
    this.orderService.getAllOrders(Object.assign(this.ordersRequest, this.filterForm.value)).subscribe(response => {
      this.orders = response.result.orders;
      this.pagination = response.pagination;
      this.listener.changeCurrentFilteredOrders(this.orders);
    }, error => this.notifier.push(error, 'error'));
  }

  public onPageChanged(index: number) {
    this.ordersRequest.pageNumber = index;
    this.onFilterChanged();
  }

  public clearFilters() {
    this.createFilterForm();
    this.ordersRequest = new OrdersRequest();
    this.onFilterChanged();
  }

  private createFilterForm() {
    this.filterForm = this.formBuilder.group({
      sortType: [0],
      validatedStatus: [0],
      login: ['']
    });
  }
}
